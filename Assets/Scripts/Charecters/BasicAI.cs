using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAI : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    public bool isPatrolling = true;


    [Tooltip("Time for dialogue to show")]
    public float dialogueWait = 7f;

    public List<Transform> pointsList = new List<Transform>();

    public Camera mainCamera;


    private Transform currentTarget;
    private float nextRound = 0.0f;
    public float fireRate = 7.0f;

    public static float speed = 5f;
    public static float stopDistance = 1f;

    public bool gatherPoints = false;

    public BasicMovement playerMovement;

    public PhantomControls phantomControls;

    public AISpawner homeSpawner;

    public Slider hpSlider;
    public Slider apSlider;

    public GameObject statUI;
    
    public GameObject fighterAbilities;
    public GameObject mageAbilities;
    public float meleesHp = 75f;
    public float meleesAp = 40f;
    public float magesHp = 40f;
    public float magesAp = 75f;
    //public GameObject healerAbilities;

    public float commonHp = 25f;
    public float commonAp = 25f;

    public bool InRange = false;

    public float currentHP;
    public float currentAP;

    public float maxHP;
    public float maxAP;

    public float levelMultiplierHP;
    public float levelMultiplierAP;

    public string startingTag;
    public bool playerInRangeR;
    public bool isRetaliating;

    public bool possessingThisObject = false;

    [Header("Object References")]
    public GameObject phantom;  //Obtain info about phantom to have it persist
    public BoxCollider2D phantomBox; //to be hidden while phantom is possessing
    public SpriteRenderer phantomMesh; //same as ^^
    public Rigidbody2D phantomRigid;

    public Transform playerObjTransform;

    public static ReaperCountdown reaper;

    public GameObject healthDrop;


    private void Start()
    {

        mainCamera = Camera.main;
        startingTag = this.gameObject.tag;
        phantomControls = GameObject.Find("Phantom2.0").GetComponent<PhantomControls>();
        // if you want points to be gathered it does that
        if (gatherPoints)
        {
            GenericFunctions.GatherComponetFromSceneByTag<Transform>(ref patrolPoints, "PatrolPoint");
        }

        phantom = GameObject.Find("Phantom2.0");
        phantomBox = phantom.GetComponent<BoxCollider2D>();
        phantomMesh = phantom.GetComponent<SpriteRenderer>();
        phantomRigid = phantom.GetComponent<Rigidbody2D>();

        //needed to assign these i think?
        //hpSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        // apSlider = GameObject.FindGameObjectWithTag("ApSlider").GetComponent<Slider>();
        // mageAbilities = GameObject.Find("MageA");
        //statUI = GameObject.FindGameObjectWithTag("UI");

        currentHP = maxHP;
        currentAP = maxAP;
    }

    private void Update()
    {
       // if (!phantomControls.isPossessing)
       // {
            
            //levelMultiplierAP = 1;
            //levelMultiplierHP = 1;
       // }

        float startTime = Time.time;
        isPatrolling = true;
        if (playerInRangeR)
        {
            isPatrolling = false;
            //ChaseThePlayer();
        }

        if (this.gameObject.tag == "tutorial")
        {
            isPatrolling = false;
            StartCoroutine(FollowPoints(this.gameObject.transform, pointsList));
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
                // Move toward the target
                Wander(this.transform, currentTarget.transform);

                // checks if its too close to target
                if (Vector2.Distance(this.transform.position, currentTarget.transform.position) < stopDistance)
                {
                    currentTarget = null;
                }
            }
        }

       // if (phantomControls.isPossessing)
       // {
            /*if(possessingThisObject)
            {
                phantom.transform.position = Vector3.back; //reset phantom's position to currently possessed NPC
            }*/
            //apSlider.value = currentAP;
       // }

        if (currentHP > maxHP) currentHP = maxHP;
        if (currentAP > maxAP) currentAP = maxAP;

        if (currentHP <= 0 && !phantomControls.isPossessing)
        {
            Die();
        }

        if (this.gameObject.tag == "Melee")
        {
            playerInRangeR = this.gameObject.GetComponent<MeleeAI>().playerInRange;
        }
        if (this.gameObject.tag == "mage")
        {
            playerInRangeR = this.gameObject.GetComponent<MageAI>().playerInRange;
        }
        if (this.gameObject.tag == "healer")
        {
            playerInRangeR = this.gameObject.GetComponent<healerAI>().playerInRange;
        }

        if (isRetaliating)
        {
            retaliate();
        }

    }

    // when the player possess a AI it destories the phantom and enables the player movement on the 
    public void Possess(GameObject phantom)
    {
        //updateLevelMultiplier();
        

        Vector3 cameraPos;
        cameraPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10);
        mainCamera.transform.position = cameraPos;
        phantomBox.enabled = false; //hide the phantom without nuking him
        phantomMesh.enabled = false; //^^^same
        phantomRigid.Sleep();
        playerMovement.enabled = true;
        //phantomControls.enabled = false;

        this.enabled = false;
        //setUI();

        mainCamera.transform.parent = this.gameObject.transform;
        phantom.transform.parent = this.gameObject.transform;
        phantom.transform.position = this.transform.position; //reset phantom's position to currently possessed NPC

        this.gameObject.tag = "Player";

        phantom.transform.position = this.gameObject.transform.position;
        phantomControls.isPossessing = true;
        possessingThisObject = true;
    }

    // this is to let the spawner know that it can send out another AI
    private void OnDestroy()
    {
        Die();

        if (homeSpawner)
        {
            homeSpawner.AI.Remove(this.gameObject);
        }

    }

    //does the wander thingy
    public void Wander(Transform mover, Transform target)
    {
        //print("I've been told to wander");
        //sets the target to a random spot within a certain distance from the target point
        if (Vector2.Distance(mover.position, target.position) > 1f)
        {
            //print("I've been told to move");
            Vector2 newTarget = new Vector2(Random.Range(target.position.x - .5f, target.position.x + .5f),
                Random.Range(target.position.y - .5f, target.position.y + .5f));

            //move towards spot
            mover.transform.position = Vector2.MoveTowards(mover.position, newTarget, speed * Time.deltaTime);
        }
    }

    IEnumerator FollowPoints(Transform tutorialGhost, List<Transform> targets)
    {
        yield return new WaitForSeconds(dialogueWait);
        foreach (Transform point in targets)
        {
            if(Vector2.Distance(tutorialGhost.position, point.position) > 1f)
            {
                tutorialGhost.position = MoveTowardsObject(tutorialGhost.position, point.position, speed);
            }
        }
        
    }

    // Checks if two transform points are within a certain distance of each other
    public bool CheckToStop(Transform currentPosition, Transform targetPosition, float distance)
    {
        return Vector2.Distance(currentPosition.position, targetPosition.position) < distance;

    }

    // moves one object towards another by set speed
    public Vector2 MoveTowardsObject(Vector2 mover, Vector2 target, float speed)
    {
        return Vector2.MoveTowards(mover, target, speed * Time.deltaTime);

    }


    //public void Idle(Transform mover, Transform currentTarget)
    //{
    //    Vector2 idleSpot = new Vector3(currentTarget.position.x + Random.Range(-3f, 3f), currentTarget.position.y + Random.Range(-3f, 3f));
    //    //Vector2.RotateTowards(mover.transform.position, idleSpot, 2f, 1f);
    //    mover.position = Vector2.MoveTowards(mover.transform.position, idleSpot, 3f);


    //}

    //public IEnumerator Idle(Transform mover)
    //{
    //    mover.rotation = new Quaternion(0, 0, Random.Range(-180, 180), 1);

    //    //mover.transform.position += transform.position * 2f;

    //    yield return new WaitForSecondsRealtime(Random.Range(100f, 150f));
    //}

    public Transform FindTarget(List<Transform> transformList)
    {
        int randomNumber = Random.Range(0, patrolPoints.Count);

        return transformList[randomNumber];

    }

    //public void ChaseThePlayer()
    //{
    //    //this.transform.LookAt(playerObjTransform);
    //    //isPatrolling = false;
    //    //isPursuing = true;
    //    playerObjTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    //    float shooterYPos = transform.position.y;
    //    Vector3 customPos = playerObjTransform.transform.position;
    //    customPos.y = shooterYPos;
    //    this.transform.LookAt(customPos);
    //    this.transform.position += this.transform.forward * speed * Time.deltaTime;

    //}//end chasePlayer

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("something is hitting me (the ai): " + other.name);
        if (other.name == "Phantom2.0" && !phantomControls.isPossessing)
        {
            InRange = true;
        }

        if (other.tag == "bullet")
        {
            print("I (the ai) Got hit by fireball");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Phantom2.0")
        {
            InRange = false;
        }
    }

    /* private void setInactive()
     {
         abilityImage.SetActive(false);
         fighterAbilities.SetActive(false);
         mageAbilities.SetActive(false);
         commonAbilities.SetActive(false);
         healerAbilities.SetActive(false);
     }*/

    private void updateLevelMultiplier() //Is called when the player possesses an NPC, will set values to current level
    {
        levelMultiplierAP *= phantomControls.currentLevel;
        levelMultiplierHP *= phantomControls.currentLevel;

        commonAp *= levelMultiplierAP;
        commonHp *= levelMultiplierHP;

    }

    //public void ReceiveDamage(float damage)
    //{
    //    print("taking damage");
    //    currentHP -= damage;
    //    isRetaliating = true;

    //}

    public void setStats(float hp, float ap)
    {
        maxAP = ap;
        maxHP = hp;
        currentHP = maxHP;
        currentAP = maxAP;
        
    }

    public void healOnPossess()
    {
        currentHP = maxHP;
        currentAP = maxAP;
    }

    public void setUI()
    {
        //Instantiate(statUI);
        statUI.SetActive(true);
        hpSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        apSlider = GameObject.FindGameObjectWithTag("ApSlider").GetComponent<Slider>();
        if(this.gameObject.tag == "mage")
        {
            if (fighterAbilities.activeInHierarchy)
            {
                fighterAbilities.SetActive(false);
            }
            mageAbilities.SetActive(true);
            setStats(magesHp, magesAp);
        }
        else if(this.gameObject.tag == "Melee")
        {
            if (mageAbilities.activeInHierarchy)
            {
                mageAbilities.SetActive(false);
            }
            fighterAbilities.SetActive(true);
            setStats(meleesHp, meleesAp);
        }
        else
        {
            if (mageAbilities.activeInHierarchy || fighterAbilities.activeInHierarchy)
            {
                mageAbilities.SetActive(false);
                fighterAbilities.SetActive(false);
            }
            setStats(commonHp, commonAp);
        }
        hpSlider.maxValue = maxHP;
        apSlider.maxValue = maxAP;
        hpSlider.value = maxHP;
        apSlider.value = maxAP;
    }

    public void DeleteUI()
    {
        Destroy(statUI);
    }

    public void resetTag()
    {
        this.gameObject.tag = startingTag;
    }

    public void retaliate()
    {
        print("Starting retaliation");
        if (playerInRangeR)
        {
            print("Player in range");
            float moveSelect = Random.Range(0, 100);
            if (moveSelect < 70)
            {
                primaryAttack();
            }
            else secondaryAttack();

        }
        else
        {
            print("Player not in range");
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");


            this.transform.LookAt(targets[0].transform);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

            transform.position = Vector2.MoveTowards(transform.position, targets[0].transform.position, 5f * Time.deltaTime);
            //move towards
        }
    }

    public void primaryAttack()
    {
        if (this.gameObject.tag == "Melee")
        {
            this.gameObject.GetComponent<MeleeAI>().meleeAttack(this.gameObject.GetComponent<MeleeAI>().weakAttackDamage);
        }

        if (this.gameObject.tag == "mage")
        {
            this.gameObject.GetComponent<MageAI>().FireAttack();
        }

        if (this.gameObject.tag == "healer")
        {
            this.gameObject.GetComponent<healerAI>().FireAttack();
        }
    }

    public void secondaryAttack()
    {
        if (this.gameObject.tag == "Melee")
        {
            this.gameObject.GetComponent<MeleeAI>().meleeAttack(this.gameObject.GetComponent<MeleeAI>().strongAttackDamage);
        }
        if (this.gameObject.tag == "mage")
        {
            //this.gameObject.GetComponent<MageAI>().FireballAttack();
        }
        if (this.gameObject.tag == "healer")
        {
            this.gameObject.GetComponent<healerAI>().Heal();
        }
    }

    private void Die()
    {
        if (Random.Range(0, 100) > 50)
        {
            //   GameObject healthpickup = Instantiate(healthDrop, this.transform.position, Quaternion.identity);
            // Destroy(healthpickup, 10f);
        }

        //Drop stuff for player? exp/items
        Destroy(this.gameObject);
    }
}
