using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class necromancerAI : MonoBehaviour
{
    public GameObject player;
    public GameObject playerVassal;
    public Camera mainCamera;
    public UIController UI;

    [Header("Health and Damage Stat Stuffs")]
    public float currentHealth;
    public float MAXHEALTH;
    private float nextAttack = 0;
    [Tooltip("How frequently attacks are fired off in seconds")]
    public float rateOfAttack = 8;
    [Header("Percentage chance of attacks firing off: 0 - lower bound is fireball attack, lower - upper is pillars of fire, upper - 100 is minion summon")]
    public float lowerBound = 20;
    public float upperBound = 60;
    //damage related stuffs goes here

    [Header("First Round of Summons")]
    public GameObject summonsMage;
    public Transform summonsMageLocation;
    public GameObject summonsMelee;
    public Transform summonsMeleeLocation;
    public GameObject summonsHealer;
    public Transform summonsHealerLocation;

    [Header("Second Round of Summons")]
    public GameObject demonOne;
    public Transform demonOneLocation;
    public GameObject demonTwo;
    public Transform demonTwoLocation;
    public GameObject demonThree;
    public Transform demonThreeLocation;

    [Header("Summons Not Dead Vars")]
    public bool canTakeDamage = false;
    public bool canAttack = false;
    public bool firstWaveSummoned = false;
    public int firstWaveSummonsKilled = 0;
    public bool secondWaveSummoned = false;
    public int secondWaveSummonsKilled = 0;
    public bool isDoneMonologuing = false;
    public bool isMonologuing = false;

    [Header("Text Stuffs")]
    public GameObject TextPanel;
    public Text speechText;
    [Tooltip("How long it takes for the text string to 'type' itself in seconds")]
    public float timeLapse = 2;
    private bool typing = false;
    [Tooltip("Text that the necro says in his speech bubbles, set to whatever you want his first words to be:")]
    public string text = "";
    private float nextRead;
    [Tooltip("how long (in seconds) we are forcing the player to read the dialogue")]
    public float checkForNextRead = 4;
    bool readPart1 = false;
    bool readPart2 = false;
    bool readPart3 = false;
    bool readPart4 = false;

    [Header("Fireball attack vars")]
    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireballDamageAmount;

    [Header("List of Positions for fire pillar attack, and other stuff")]
    public GameObject[] pillarLocationGroup1;
    public GameObject[] pillarLocationGroup2;
    public GameObject[] pillarLocationGroup3;
    public GameObject[] pillarLocationGroup4;
    [Tooltip("Do Not use Fireball from mage attacks for this game object, it breaks it")]
    public GameObject pillarOfFire;
    [Tooltip("How long a pillar will stay in game")]
    public float durationPillarsLastInSeconds;

    [Header("Various Summonable Minions")]
    public Transform summonLocation;
    public GameObject undeadKnight;
    public GameObject deathMage;
    public GameObject darkHealer;
    public GameObject demonSummon;

    [Header("Audio Stuffs")]
    public AudioSource musicSource;
    public AudioClip dialogueAmbience;
    public AudioClip FireBallSound;
    public AudioClip evilLaugh;
    public AudioClip summonMinionSound;
    private AudioSource source;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(dialogueAmbience);
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UI = this.gameObject.GetComponent<UIController>();
        mainCamera = Camera.main;
        musicSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SingleFireballAttack(); //test only comment out when done testing: tests single fireball attack
        if (Input.GetKeyDown(KeyCode.P)) PillarsOfFireAttack(); //test only comment out when done testing: tests fire pillar Attack
        
        if(Input.anyKeyDown) //only way i could get the monologuing to work without breaking lots of other stuffs
        {
            if (!readPart1)
            {
                readPart1 = true;
                nextRead = Time.time + checkForNextRead;
                text = "You're.... pitifull new form cannot fool me, for I can see all...";
                typing = false;
            }
            else if (!readPart2 && readPart1 && Time.time > nextRead)
            {
                readPart2 = true;
                nextRead = Time.time + checkForNextRead;
                text = "You seek vengeance.... Or are you fullfilling your duty as hero?...";
                typing = false;
            }
            else if (!readPart3 && readPart2 && Time.time > nextRead)
            {
                readPart3 = true;
                nextRead = Time.time + checkForNextRead;
                text = "It matters not.... in the end.... we are destined to fight...";
                typing = false;
            }
            else if (!readPart4 && readPart3 && Time.time > nextRead)
            {
                readPart4 = true;
                nextRead = Time.time + checkForNextRead;
                typing = false;
            }
        }


        if (player.transform.parent != null)
            playerVassal = player.transform.parent.gameObject;
        else playerVassal = null;

        currentHealth = UI.currentHealth;

        if (canAttack && Time.time > nextAttack)
        {
            float randNumber = Random.Range(0, 100); //attacks fire off randomly based on lower and upper bounds
            if(randNumber < lowerBound)
            {
                SingleFireballAttack();
                nextAttack = Time.time + rateOfAttack;
            }
            if(randNumber >=lowerBound && randNumber < upperBound)
            {
                PillarsOfFireAttack();
                nextAttack = Time.time + rateOfAttack;
            }
            if(randNumber >= upperBound)
            {
                summonMinion();
                nextAttack = Time.time + rateOfAttack;
            }
        }

        if (!canTakeDamage) //is immortal until said otherwise
        {
            currentHealth = MAXHEALTH;
        }


        if(!isDoneMonologuing) //if text isn't done cycling, and the monologue hasn't started, start monologuing
        {
            Monologuing();
        }
        else if(isDoneMonologuing && !firstWaveSummoned) //if hes done talking, do stuff (like summon the first wave of monsters)
        {
            player.GetComponent<PhantomControls>().enabled = true;
            if(playerVassal) playerVassal.GetComponent<BasicMovement>().enabled = true;
            source.Stop();
            musicSource.Play();
            UI.uiObj.SetActive(true);
            TextPanel.SetActive(false);
            firstWaveSummoned = true; //summons the first wave of minions to their set locations
            Instantiate(summonsHealer, summonsHealerLocation.position, Quaternion.identity, null);
            Instantiate(summonsMage, summonsMageLocation.position, Quaternion.identity, null);
            Instantiate(summonsMelee, summonsMeleeLocation.position, Quaternion.identity, null);
            
        }

        if(firstWaveSummoned && firstWaveSummonsKilled == 3 && !secondWaveSummoned) //if the first wave minions are murdered in not so cold blood, summon wave 2
        {
            secondWaveSummoned = true;
            Instantiate(demonOne, demonOneLocation.position, Quaternion.identity, null);
            Instantiate(demonTwo, demonTwoLocation.position, Quaternion.identity, null);
            Instantiate(demonThree, demonThreeLocation.position, Quaternion.identity, null);
        }

        if (currentHealth <= 0) Die();

    }

    private void Monologuing()
    {

        mainCamera.GetComponent<cameraScript>().target = this.gameObject;
        isMonologuing = true;
        player.GetComponent<PhantomControls>().enabled = false;
        if (playerVassal) playerVassal.GetComponent<BasicMovement>().enabled = false;
        //insert code for the boss monologuing here
        TextPanel.SetActive(true);
        if(!typing)
        StartCoroutine(BuildText());
      
        if (readPart1)
        {
            
            if(!typing)
            StartCoroutine(BuildText());

        }
        if (readPart2)
        {
            
            if(!typing)
            StartCoroutine(BuildText());

        }
        if (readPart3)
        {
            
            if(!typing)
            StartCoroutine(BuildText());

        }
        if (readPart4)
        {
            isMonologuing = false;
            isDoneMonologuing = true;
            mainCamera.GetComponent<cameraScript>().target = player; //set camera back to player, probably should make it pan over time but thats for later
        }


    }

    private void SingleFireballAttack() //throws a single fireball at the player
    {            
            
        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

        projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
        projectileBullet.GetComponent<Projectile>().setTarget(player.transform.position);
        source.PlayOneShot(FireBallSound);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
        
    }

    private void PillarsOfFireAttack() //Summons pillars of fire at one of 4 set groups of locations
    {
        source.PlayOneShot(evilLaugh);
        StartCoroutine(DelaySpawnOfPillars());
    }

    private void summonMinion() //Summons random minion
    {
        int randNum = Random.Range(0, 4);
        switch (randNum)
        {
            case 0:
                Instantiate(undeadKnight, summonLocation.position, Quaternion.identity, this.gameObject.transform);
                break;
            case 1:
                Instantiate(deathMage, summonLocation.position, Quaternion.identity, this.gameObject.transform);
                break;
            case 2:
                Instantiate(darkHealer, summonLocation.position, Quaternion.identity, this.gameObject.transform);
                break;
            case 3:
                Instantiate(demonSummon, summonLocation.position, Quaternion.identity, this.gameObject.transform);
                break;
            default:
                Instantiate(demonSummon, summonLocation.position, Quaternion.identity, this.gameObject.transform);
                break;

        }
        source.PlayOneShot(summonMinionSound);
    }

    private IEnumerator BuildText()
    {
        speechText.text = "";
        typing = true;
        for (int i = 0; i < text.Length; i++)
        {
            speechText.text = string.Concat(speechText.text, text[i]);
            //Wait a certain amount of time, then continue with the for loop
            yield return new WaitForSeconds(timeLapse);
        }
        
        
    }

    private IEnumerator DelaySpawnOfPillars()
    {
        yield return new WaitForSeconds(4f);
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                GameObject FirePillar1 = Instantiate(pillarOfFire, pillarLocationGroup1[0].transform.position, Quaternion.identity, null);
                GameObject FirePillar2 = Instantiate(pillarOfFire, pillarLocationGroup1[1].transform.position, Quaternion.identity, null);
                GameObject FirePillar3 = Instantiate(pillarOfFire, pillarLocationGroup1[2].transform.position, Quaternion.identity, null);
                GameObject FirePillar4 = Instantiate(pillarOfFire, pillarLocationGroup1[3].transform.position, Quaternion.identity, null);
                Destroy(FirePillar1, durationPillarsLastInSeconds);
                Destroy(FirePillar2, durationPillarsLastInSeconds);
                Destroy(FirePillar3, durationPillarsLastInSeconds);
                Destroy(FirePillar4, durationPillarsLastInSeconds);
                break;
            case 1:
                GameObject FirePillar5 = Instantiate(pillarOfFire, pillarLocationGroup2[0].transform.position, Quaternion.identity, null);
                GameObject FirePillar6 = Instantiate(pillarOfFire, pillarLocationGroup2[1].transform.position, Quaternion.identity, null);
                GameObject FirePillar7 = Instantiate(pillarOfFire, pillarLocationGroup2[2].transform.position, Quaternion.identity, null);
                GameObject FirePillar8 = Instantiate(pillarOfFire, pillarLocationGroup2[3].transform.position, Quaternion.identity, null);
                Destroy(FirePillar5, durationPillarsLastInSeconds);
                Destroy(FirePillar6, durationPillarsLastInSeconds);
                Destroy(FirePillar7, durationPillarsLastInSeconds);
                Destroy(FirePillar8, durationPillarsLastInSeconds);
                break;
            case 2:
                GameObject FirePillar9 = Instantiate(pillarOfFire, pillarLocationGroup3[0].transform.position, Quaternion.identity, null);
                GameObject FirePillar10 = Instantiate(pillarOfFire, pillarLocationGroup3[1].transform.position, Quaternion.identity, null);
                GameObject FirePillar11 = Instantiate(pillarOfFire, pillarLocationGroup3[2].transform.position, Quaternion.identity, null);
                GameObject FirePillar12 = Instantiate(pillarOfFire, pillarLocationGroup3[3].transform.position, Quaternion.identity, null);
                Destroy(FirePillar9, durationPillarsLastInSeconds);
                Destroy(FirePillar10, durationPillarsLastInSeconds);
                Destroy(FirePillar11, durationPillarsLastInSeconds);
                Destroy(FirePillar12, durationPillarsLastInSeconds);
                break;
            case 3:
                GameObject FirePillar13 = Instantiate(pillarOfFire, pillarLocationGroup4[0].transform.position, Quaternion.identity, null);
                GameObject FirePillar14 = Instantiate(pillarOfFire, pillarLocationGroup4[1].transform.position, Quaternion.identity, null);
                GameObject FirePillar15 = Instantiate(pillarOfFire, pillarLocationGroup4[2].transform.position, Quaternion.identity, null);
                GameObject FirePillar16 = Instantiate(pillarOfFire, pillarLocationGroup4[3].transform.position, Quaternion.identity, null);
                Destroy(FirePillar13, durationPillarsLastInSeconds);
                Destroy(FirePillar14, durationPillarsLastInSeconds);
                Destroy(FirePillar15, durationPillarsLastInSeconds);
                Destroy(FirePillar16, durationPillarsLastInSeconds);
                break;
            default:
                FirePillar1 = Instantiate(pillarOfFire, pillarLocationGroup1[0].transform.position, Quaternion.identity, null);
                FirePillar2 = Instantiate(pillarOfFire, pillarLocationGroup1[1].transform.position, Quaternion.identity, null);
                FirePillar3 = Instantiate(pillarOfFire, pillarLocationGroup1[2].transform.position, Quaternion.identity, null);
                FirePillar4 = Instantiate(pillarOfFire, pillarLocationGroup1[3].transform.position, Quaternion.identity, null);
                Destroy(FirePillar1, durationPillarsLastInSeconds);
                Destroy(FirePillar2, durationPillarsLastInSeconds);
                Destroy(FirePillar3, durationPillarsLastInSeconds);
                Destroy(FirePillar4, durationPillarsLastInSeconds);
                break;
        }
    }

 
    private void Die()
    {
        //do the die
        Destroy(this.gameObject);
        //if time permits, input closing dialogue here before transitioning to victory scene

        //input scene move to victory scene
        
    }
}
