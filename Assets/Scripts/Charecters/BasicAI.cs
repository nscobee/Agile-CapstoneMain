using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAI : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    public bool isPatrolling = true;
    
    public Camera mainCamera;

    public int NPC_ID;
    public int currentLevel = 1; 
    
    private Transform currentTarget;
  //  private float nextRound = 0.0f;
  //  public float fireRate = 7.0f;

    public float speed = 5f;
    public static float stopDistance = 1f;

    public float distanceBetweenPlayer = 9999999999999;
    public float distanceToStartAttackingPlayer = 1;
    public float distanceToForgetPlayer = 5;
    public float aggroDistance = 3f;

    public bool gatherPoints = false;
    public bool isAggressive = false;

    public BasicMovement playerMovement;

    public PhantomControls phantomControls;

    public AISpawner homeSpawner;

    public UIController UIControls;
    public PossessIcon PossessionIcon;

    
   // public GameObject fighterAbilities;
  //  public GameObject mageAbilities;


    public bool InRange = false;

    public bool playerInRangeToAttack = false;
    public float nextAIAttack = 0f;
    public float AttackCooldown = 10f;
    private bool canAttack = false;
    public bool canSpawn = true;
    public float randNum;
    [Tooltip("Percentage of how often the primary is used, out of 100")]
    public float frequencyOfPrimary = 80f;


    public float levelMultiplierHP;
    public float levelMultiplierAP;

    public string startingTag;
    [Tooltip("If starting tag is noPossess, input the correct tag from one of the following: Melee, healer, mage, dog, demon")]
    public string tagOverride;

    public bool isRetaliating;
    public bool movingToPlayer = false;
    public bool isPursuing;

    public bool possessingThisObject = false;

    public bool canPossess = true;
    public bool possessOnLowHealth = false;

    public float dirNum;

    [Header("Audio Stuffs")]   
    public AudioClip possessSound;
    public AudioClip swordAttackSound;
    public AudioClip heavySwordAttackSound;
    public AudioClip MagicAttackSound;
    public AudioClip BigMagicAttackSound;
    public AudioClip HealSound;
    public AudioClip dogAttackSound;
    public AudioClip demonSlashSound;
    private AudioSource source;

    [Header("Object References")]
    public GameObject phantom;  //Obtain info about phantom to have it persist
    public BoxCollider2D phantomBox; //to be hidden while phantom is possessing
    public SpriteRenderer phantomMesh; //same as ^^
    public Rigidbody2D phantomRigid;

    public Transform playerObjTransform;

    public static ReaperCountdown reaper;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }



    private void Start()
    {

        NPC_ID = ((int)(Random.Range(0, 100000) * 100));

        mainCamera = Camera.main;
        startingTag = this.gameObject.tag;
        if (startingTag == "NoPossess") startingTag = tagOverride;
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
        UIControls = this.gameObject.GetComponent<UIController>();
        PossessionIcon = this.gameObject.GetComponent<PossessIcon>();
        playerObjTransform = phantom.transform;

        if (possessOnLowHealth) canPossess = false;


    }

    private void Update()
    {
        randNum = Random.Range(0f, 100f);

        if (isPursuing)
            ChaseThePlayer();
       // if (!phantomControls.isPossessing)
       // {
            
            //levelMultiplierAP = 1;
            //levelMultiplierHP = 1;
       // }

        float startTime = Time.time;
        //isPatrolling = true;
       // if (isRetaliating)
      //  {
       //     isPatrolling = false;
            //ChaseThePlayer();
      //  }

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

        nextAIAttack += Time.deltaTime;
        if(nextAIAttack >= AttackCooldown)
        {
            canAttack = true;
            nextAIAttack = 0;
        }

        if (UIControls.currentHealth <= 0 && !phantomControls.isPossessing)
        {
            UIControls.Die();
        }

        if(!playerInRangeToAttack && isRetaliating && this.gameObject.tag != "Possessed" && movingToPlayer)
        {
            if(GameObject.FindGameObjectWithTag("Possessed"))
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Possessed").transform.position, speed * Time.deltaTime);
           // if((GameObject.FindGameObjectWithTag("Possessed").transform.position.x - this.gameObject.transform.position.x < 0))
           // {
          //      this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
          //  }
          //  else this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            print("I'm moving towards the possessed!");
        }
        if(GameObject.FindGameObjectWithTag("Possessed"))
        distanceBetweenPlayer = Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Possessed").transform.position);
       
       
           
        if (isRetaliating)
        {
            Vector3 heading = GameObject.FindGameObjectWithTag("Possessed").transform.position - transform.position;
            dirNum = AngleDir(transform.forward, heading, transform.up);
            print("I'm retaliating!");
                isPatrolling = false;
                retaliate();

            if (dirNum < 0)
                {
        
                    this.gameObject.transform.eulerAngles = new Vector3(0, this.gameObject.transform.eulerAngles.y + 180, 0);
     
                }
         
        }

        if (!GameObject.FindGameObjectWithTag("Possessed")) isRetaliating = false;
        if (GameObject.FindGameObjectWithTag("Possessed"))
            if (tagOverride != null && GameObject.FindGameObjectWithTag("Possessed").GetComponent<BasicAI>().tagOverride == tagOverride)
                isAggressive = false;
        if (isAggressive && distanceBetweenPlayer <= aggroDistance)
            isRetaliating = true;


    }

    // when the player possess a AI it destories the phantom and enables the player movement on the 
    public void Possess(GameObject phantom)
    {
        source.PlayOneShot(possessSound);
        //updateLevelMultiplier();
        isPatrolling = false;
        Vector3 cameraPos;
        cameraPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10);
        //mainCamera.transform.position = cameraPos;
        phantomBox.enabled = false; //hide the phantom without nuking him
        phantomMesh.enabled = false; //^^^same
        phantomRigid.Sleep();
        playerMovement.enabled = true;
        phantomControls.enabled = false;

        this.enabled = false;
        //setUI();

        //mainCamera.transform.parent = this.gameObject.transform;
        phantom.transform.parent = this.gameObject.transform;
        phantom.transform.position = this.transform.position; //reset phantom's position to currently possessed NPC

        this.gameObject.tag = "Possessed";

        phantom.transform.position = this.gameObject.transform.position;
        phantomControls.isPossessing = true;
        possessingThisObject = true;

        healOnPossess();
    }

    // this is to let the spawner know that it can send out another AI
    private void OnDestroy()
    {
       // Die();

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
    
    public Transform FindTarget(List<Transform> transformList)
    {
        int randomNumber = Random.Range(0, patrolPoints.Count);

        return transformList[randomNumber];
    }

    //public Transform FindNextTarget(List<Transform> points, Transform currentPoint)
    //{
        
    //}

    public void ChaseThePlayer()
    {
        this.transform.LookAt(playerObjTransform);
        isPatrolling = false;
        isPursuing = true;
        playerObjTransform = GameObject.FindGameObjectWithTag("Possessed").GetComponent<Transform>();
        float shooterYPos = transform.position.y;
        Vector3 customPos = playerObjTransform.transform.position;
        customPos.y = shooterYPos;
       this.transform.LookAt(customPos);
       this.transform.position = this.transform.forward * speed * Time.deltaTime;

    }//end chasePlayer

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


    public void healOnPossess()
    {
        if (!phantom.GetComponent<levelingScript>().NPC_Levels.Contains(NPC_ID))
        {
            UIControls.currentHealth = UIControls.MAXHP;
            UIControls.currentMana = UIControls.MAXMANA;
        }
    }

   

    public void resetTag()
    {
        this.gameObject.tag = startingTag;
    }

    public void retaliate()
    {
        //in theory these 3 lines of code will make the AI look at player
        

        print("Retalition!!!!");
      if(distanceBetweenPlayer <= distanceToStartAttackingPlayer)
        {
            print("I am in range to attack the player");   
            movingToPlayer = false;
            playerInRangeToAttack = true;
            //float randomNum = Random.Range(0.0f, 100.0f);
            // print(randomNum);
           
            if(canAttack && randNum <= frequencyOfPrimary )
            {
                print("Weak attack!");              
                primaryAttack();
                canAttack = false;
                
            }
            else if(canAttack)
            {
                print("strong attack!");         
                secondaryAttack();
                canAttack = false;
            }
        }
        else if(distanceBetweenPlayer > distanceToStartAttackingPlayer && distanceBetweenPlayer <= distanceToForgetPlayer)
        {
            if(startingTag == "demon")
            {
                if(canAttack && randNum > frequencyOfPrimary)
                {
                    print("strong attack!");
                    secondaryAttack();
                    canAttack = false;
                }
            }

            print("I am not in range to attack the player but I can see him");
            movingToPlayer = true;
            playerInRangeToAttack = false;        
            
        }
      else if(distanceBetweenPlayer > distanceToForgetPlayer || GameObject.FindGameObjectWithTag("Possessed") == null)
        {
            print("Player who?");
            playerInRangeToAttack = false;
            isRetaliating = false;
            movingToPlayer = false;
        }
    }

    public void primaryAttack()
    {
        if (startingTag == "Melee")
        {
            this.gameObject.GetComponent<MeleeAI>().meleeAttack(this.gameObject.GetComponent<MeleeAI>().weakAttackDamage);
            source.PlayOneShot(swordAttackSound);
        }

        if (startingTag == "mage")
        {
            this.gameObject.GetComponent<MageAI>().FireballAttack(playerObjTransform);
            source.PlayOneShot(MagicAttackSound);
        }

        if (startingTag == "healer")
        {
            this.gameObject.GetComponent<healerAI>().FireAttack(playerObjTransform);
            source.PlayOneShot(MagicAttackSound);
        }

        if (startingTag == "dog")
        {
            this.gameObject.GetComponent<MeleeAI>().meleeAttack(this.gameObject.GetComponent<MeleeAI>().weakAttackDamage);
            source.PlayOneShot(dogAttackSound);
        }

        if (startingTag == "demon")
        {
            print("demon using primary!");
            this.gameObject.GetComponent<demonAI>().meleeAttack(this.gameObject.GetComponent<demonAI>().weakAttackDamage);
            source.PlayOneShot(demonSlashSound);
        }
    }

    public void secondaryAttack()
    {
        if (startingTag == "Melee")
        {
            this.gameObject.GetComponent<MeleeAI>().meleeAttack(this.gameObject.GetComponent<MeleeAI>().strongAttackDamage);
            source.PlayOneShot(heavySwordAttackSound);
        }
        if (startingTag == "mage")
        {
            this.gameObject.GetComponent<MageAI>().AOEAttack(playerObjTransform);
            source.PlayOneShot(BigMagicAttackSound);
        }
        if (startingTag == "healer")
        {
            this.gameObject.GetComponent<healerAI>().Heal();
            source.PlayOneShot(HealSound);
        }
        if (startingTag == "dog")
        {
            this.gameObject.GetComponent<MeleeAI>().meleeAttack(this.gameObject.GetComponent<MeleeAI>().weakAttackDamage);
            source.PlayOneShot(dogAttackSound);
        }
        if (startingTag == "demon")
        {
            print("demon using secondary!");
            this.gameObject.GetComponent<demonAI>().AOEAttack(playerObjTransform);
            source.PlayOneShot(BigMagicAttackSound);
        }
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    
}
