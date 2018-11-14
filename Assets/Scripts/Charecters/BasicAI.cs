using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAI : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    private Transform currentTarget;

    public static float speed = 5f;
    public static float stopDistance = 1f;

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

    public static ReaperCountdown reaper;


    private void Start()
    {
        // if you want points to be gathered it does that
        if (gatherPoints)
        {
            // if you want points to be gathered it does that
            if (gatherPoints)
            {
                GenericFunctions.GatherComponetFromSceneByTag<Transform>(ref patrolPoints, "PatrolPoint");

                // private IEnumerator wanderCoroutine;
                //private IEnumerator idleCoroutine;

            }

            
        }
        phantom = GameObject.FindWithTag("Player");
        phantomBox = phantom.GetComponent<BoxCollider>();
        phantomMesh = phantom.GetComponent<MeshRenderer>();
    }

    private void Update()
    {

       float startTime = Time.time;
        // if there is no target it gets one
        if (currentTarget == null && patrolPoints.Count > 0)
        {
            currentTarget = FindTarget(patrolPoints);
        }

        if (currentTarget != null)
        {
            print("moving to target: " + currentTarget.ToString());

            // Move toward the target
            Wander(this.transform, currentTarget.transform);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);

            // checks if its too close to target
            if (Vector3.Distance(this.transform.position, currentTarget.transform.position) < stopDistance)
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
        playerMovement.enabled = true;
        this.enabled = false;

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


    public void Idle(Transform mover, Transform currentTarget)
    {
        Vector3 idleSpot = new Vector3(currentTarget.position.x + Random.Range(-3f, 3f), currentTarget.position.y + Random.Range(-3f, 3f));
        Vector3.RotateTowards(mover.transform.position, idleSpot, 2f, 1f);
        mover.position = Vector3.MoveTowards(mover.transform.position, idleSpot, 3f);


    }

    public IEnumerator Idle(Transform mover)
    {
        mover.rotation = new Quaternion(0, 0, Random.Range(-180, 180), 1);

        //mover.transform.position += transform.position * 2f;

        yield return new WaitForSecondsRealtime(Random.Range(100f, 150f));
    }




    //does the wander thingy
    public void Wander(Transform mover, Transform target)
    {
        //sets the target to a random spot within a certain distance from the target point
        if (Vector3.Distance(mover.position, target.position) > 1f)
        {
            Vector3 newTarget = new Vector3(Random.Range(target.position.x - .5f, target.position.x + .5f),
                Random.Range(target.position.y - .5f, target.position.y + .5f), 0);

            //float angleToTurn = Vector3.Angle(mover.position, target.position);
            //Vector3 wanderTarget = Vector3.RotateTowards(mover.position, newTarget, Random.Range(-angleToTurn, angleToTurn), 1f);
            
            //move towards spot
            mover.transform.position = Vector3.MoveTowards(mover.position, newTarget, speed * Time.deltaTime);
        }
        //yield return new WaitForSecondsRealtime(Random.Range(5f, 15f));

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
