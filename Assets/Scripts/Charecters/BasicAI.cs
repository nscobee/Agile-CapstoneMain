using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAI : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    public bool isPatrolling = true;

    private Transform currentTarget;
    private float nextRound = 0.0f;
    public float fireRate = 7.0f;

    public static float speed = 5f;
    public static float stopDistance = 1f;

    public bool gatherPoints = false;

    public BasicMovement playerMovement;

    public PhantomControls phantomControls;

    public AISpawner homeSpawner;

    public PhantomControls phantomControls;

    public Slider hpSlider;
    public Slider apSlider;
    public GameObject abilityImage;
    public GameObject fighterAbilities;
    public GameObject mageAbilities;
    public GameObject commonAbilities;
    public GameObject healerAbilities;

    public float commonHp = 25f;
    public float commonAp = 25f;

    public float fighterHp = 90f;
    public float fighterAp = 40f;

    public float mageHp = 40f;
    public float mageAp = 90f;

    public bool playerInRange = false;
    public float healerHp = 45f;
    public float healerAp = 75f;

    public float currentHP;
    public float currentAP;

    public float maxHP;
    public float maxAP;

    public float levelMultiplierHP;
    public float levelMultiplierAP;

    public bool isPosessingMage = false;
    public bool isPosessingFighter = false;
    public bool isPosessingCommon = false;

    public GameObject phantom;  //Obtain info about phantom to have it persist
    public BoxCollider2D phantomBox; //to be hidden while phantom is possessing
    public SpriteRenderer phantomMesh; //same as ^^

    public Transform playerObjTransform;

    public static ReaperCountdown reaper;

    public GameObject healthDrop;


    private void Start()
    {
        phantomControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PhantomControls>();
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
        phantomBox = phantom.GetComponent<BoxCollider2D>();
        phantomMesh = phantom.GetComponent<SpriteRenderer>();

        //needed to assign these i think?
        phantomControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PhantomControls>();
        hpSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        apSlider = GameObject.FindGameObjectWithTag("ApSlider").GetComponent<Slider>();
        mageAbilities = GameObject.Find("MageA");
        phantomBox = phantom.GetComponent<BoxCollider>();
        phantomMesh = phantom.GetComponent<MeshRenderer>();

        currentHP = maxHP;
        currentAP = maxAP;
        
    }

    private void Update()
    {
    if(!phantomControls.isPossessing)
        {
            levelMultiplierAP = 1;
            levelMultiplierHP = 1;
        }

        float startTime = Time.time;
        //isPatrolling = true;
       /* if (playerInRange)
        {
            isPatrolling = false;
            ChaseThePlayer();
        }*/

        //check if posessing a character, then what character

        if (currentTarget == null && patrolPoints.Count > 0)

        if (isPosessingMage)
        {
            hpSlider.value = mageHp;
            apSlider.value = mageAp;
        }
        if (isPosessingFighter)
        {
            hpSlider.value = fighterHp;
            apSlider.value = fighterAp;
        }
        if (isPosessingCommon)
        {
            hpSlider.value = commonHp;
            apSlider.value = commonAp;
        }
        

        if (isPatrolling)
        {
            // if there is no target it gets one
            if (currentTarget == null && patrolPoints.Count > 0)
            {
                currentTarget = FindTarget(patrolPoints);
            }

            if (currentTarget != null)
            {
                // print("moving to target: " + currentTarget.ToString());

                // Move toward the target
                Wander(this.transform, currentTarget.transform);
                //this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);

                // checks if its too close to target
                if (Vector2.Distance(this.transform.position, currentTarget.transform.position) < stopDistance)
                {
                    currentTarget = null;
                }
            }
        }

        if(phantomControls.isPossessing)
        {
            hpSlider.value = currentHP;
            apSlider.value = currentAP;
        }

        
        
        if (currentHP > maxHP) currentHP = maxHP;
        if (currentAP > maxAP) currentAP = maxAP;

        if(currentHP <= 0 && !phantomControls.isPossessing)
        {
            Die();
        }

    }

    // when the player possess a AI it destories the phantom and enables the player movement on the 
    public void Possess(GameObject phantom)
    {

        updateLevelMultiplier();

        phantomBox.enabled = false; //hide the phantom without nuking him
        phantomMesh.enabled = false; //^^^same
        playerMovement.enabled = true;

        this.enabled = false;
        phantomControls.isPossessing = true;

        if (gameObject.tag == "fighter")
        {
            hpSlider.value = fighterHp;
            apSlider.value = fighterAp;
            maxHP = fighterHp;
            maxAP = fighterAp;

            setInactive();
            fighterAbilities.SetActive (true);
            
            isPosessingMage = false;
            isPosessingCommon = false;
            isPosessingFighter = true;

           
        }
        else if (gameObject.tag == "mage")
        {
            //hpSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
            //apSlider = GameObject.FindGameObjectWithTag("ApSlider").GetComponent<Slider>();
            
            hpSlider.value = mageHp;
            apSlider.value = mageAp;
            maxHP = mageHp;
            maxAP = mageAp;

            setInactive();
            mageAbilities.SetActive(true);

            isPosessingMage = true;
            isPosessingCommon = false;
            isPosessingFighter = false;
 
        }
        else if(gameObject.tag == "healer")
        {
            hpSlider.value = healerHp;
            apSlider.value = healerAp;
            maxHP = healerHp;
            maxAP = healerAp;

            setInactive();
            healerAbilities.SetActive(true);
        }
        else
        {
            hpSlider.value = commonHp;
            apSlider.value = commonAp;
            maxHP = commonHp;
            maxAP = commonAp;

            setInactive();

            //commonAbilities.SetActive(true);
            //phantomControls.isPossessing = false;

            isPosessingMage = false;
            isPosessingFighter = false;
            isPosessingCommon = true;

        }

        phantom.transform.position = this.transform.position; //reset phantom's position to currently possessed NPC

    }

    private void setInactive()
    {
        abilityImage.SetActive(false);
        fighterAbilities.SetActive(false);
        mageAbilities.SetActive(false);
        commonAbilities.SetActive(false);
    }

    #region automove?
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
            // mover.LookAt(newTarget);
            mover.transform.position = Vector3.MoveTowards(mover.position, newTarget, speed * Time.deltaTime);
        }
        //yield return new WaitForSecondsRealtime(Random.Range(5f, 15f));

    }
    
    #endregion

    // this is to let the spawner know that it can send out another AI
    private void OnDestroy()
    {
        Die();
        isPosessingMage = false;
        isPosessingFighter = false;
        isPosessingCommon = true;

        if (homeSpawner)
        {
            homeSpawner.AI.Remove(this.gameObject);
        }
    }


    public void ChaseThePlayer()
    {
        //this.transform.LookAt(playerObjTransform);
        //isPatrolling = false;
        //isPursuing = true;
        playerObjTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float shooterYPos = transform.position.y;
        Vector3 customPos = playerObjTransform.transform.position;
        customPos.y = shooterYPos;
        this.transform.LookAt(customPos);
        this.transform.position += this.transform.forward * speed * Time.deltaTime;

    }//end chasePlayer
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("something is hitting me (the ai): " + other.name);
        if (other.tag == "Player" && !phantomControls.isPossessing)
        {
            playerInRange = true;
        }

        if (other.tag == "bullet")
        {
            print("I (the ai) Got hit by fireball");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }

    private void setInactive()
    {
        abilityImage.SetActive(false);
        fighterAbilities.SetActive(false);
        mageAbilities.SetActive(false);
        commonAbilities.SetActive(false);
        healerAbilities.SetActive(false);
    }
    
    private void updateLevelMultiplier() //Is called when the player possesses an NPC, will set values to current level
    {
        levelMultiplierAP *= phantomControls.currentLevel;
        levelMultiplierHP *= phantomControls.currentLevel;

        mageHp *= levelMultiplierHP;
        mageAp *= levelMultiplierAP;

        commonAp *= levelMultiplierAP;
        commonHp *= levelMultiplierHP;

        fighterAp *= levelMultiplierAP;
        fighterHp *= levelMultiplierHP;

        healerAp *= levelMultiplierAP;
        healerHp *= levelMultiplierHP;
    }

    public void ReceiveDamage(float damage)
    {
        currentHP -= damage;

    }

    private void Die()
    {

        if (Random.Range(0, 100) > 50)
        {
            GameObject healthpickup = Instantiate(healthDrop, this.transform.position, Quaternion.identity);
            Destroy(healthpickup, 10f);
        }

        //Drop stuff for player? exp/items
        Destroy(this.gameObject);

    }

}
