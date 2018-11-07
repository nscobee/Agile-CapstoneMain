using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAI : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    private Transform currentTarget;

    public static int speed = 5;
    public static int stopDistance = 1;

    public bool gatherPoints = false;

    public BasicMovement playerMovement;

    public AISpawner homeSpawner;

    public Slider hpSlider;
    public Slider apSlider;
    public GameObject abilityImage;
    public GameObject fighterAbilities;
    public GameObject mageAbilities;
    public GameObject commonAbilities;

    public float commonHp = 25f;
    public float commonAp = 25f;

    public float fighterHp = 90f;
    public float fighterAp = 40f;

    public float mageHp = 40f;
    public float mageAp = 90f;


    public GameObject phantom;  //Obtain info about phantom to have it persist
    public BoxCollider phantomBox; //to be hidden while phantom is possessing
    public MeshRenderer phantomMesh; //same as ^^


    private void Start()
    {
        // if you want points to be gathered it does that
        if (gatherPoints)
        {
            GenericFunctions.GatherComponetFromSceneByTag<Transform>(ref patrolPoints, "PatrolPoint");


        }

        phantom = GameObject.FindWithTag("Player");
        phantomBox = phantom.GetComponent<BoxCollider>();
        phantomMesh = phantom.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // if there is no target it gets one
        if (currentTarget == null)
        {
            if (patrolPoints.Count > 0)
            {
                currentTarget = FindTarget(patrolPoints);

            }

        }

        if (currentTarget != null)
        {
            // Move toward the target
            this.transform.position = MoveTowardsObject(this.transform.position, currentTarget.transform.position, speed);

            // checks if its too close to target
            if (CheckToStop(this.transform, currentTarget.transform, stopDistance))
            {
                currentTarget = null;

            }
        }
    }

    // when the player possess a AI it destories the phantom and enables the player movement on the 
    public void Possess(GameObject phantom)
    {
        phantomBox.enabled = false; //hide the phantom without nuking him
        phantomMesh.enabled = false; //^^^same
        
        if (gameObject.tag == "fighter")
        {
            hpSlider.value = fighterHp;
            apSlider.value = fighterAp;

            abilityImage.SetActive (false);
            fighterAbilities.SetActive (true);
            mageAbilities.SetActive(false);
            commonAbilities.SetActive(false);

        }
        else if(gameObject.tag == "mage")
        {
            hpSlider.value = mageHp;
            apSlider.value = mageAp;

            abilityImage.SetActive(false);
            fighterAbilities.SetActive(false);
            mageAbilities.SetActive(true);
            commonAbilities.SetActive(false);
        }
        else
        {
            hpSlider.value = commonHp;
            apSlider.value = commonAp;

            abilityImage.SetActive(false);
            fighterAbilities.SetActive(false);
            mageAbilities.SetActive(false);
            commonAbilities.SetActive(true);
        }
        phantom.transform.position = this.transform.position; //reset phantom's position to currently possessed NPC


        playerMovement.enabled = true;
        this.enabled = false;

    }

    // this will be called from update if the AI has no target and will get a target from the given List
    public Transform FindTarget(List<Transform> transformList)
    {
        int randomNumber = Random.Range(0, patrolPoints.Count);

        return transformList[randomNumber];

    }

    // Checks if two transform points are within a certain distance of each other
    public bool CheckToStop(Transform currentPosition, Transform targetPosition, float distance)
    {
        return Vector3.Distance(currentPosition.position, targetPosition.position) < distance;

    }

    // moves one object towards another by set speed
    public Vector3 MoveTowardsObject(Vector3 mover, Vector3 target, float speed)
    {
        return Vector3.MoveTowards(mover, target, speed * Time.deltaTime);

    }

    // this is to let the spawner know that it can send out another AI
    private void OnDestroy()
    {
        if (homeSpawner)
        {
            homeSpawner.AI.Remove(this.gameObject);

        }
    }

}
