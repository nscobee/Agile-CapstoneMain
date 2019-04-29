using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class necromancerAI : MonoBehaviour
{
    public GameObject player;
    public GameObject playerVassal;
    public UIController UI;

    [Header("Health and Damage Stat Stuffs")]
    public float currentHealth;
    public float MAXHEALTH;
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
    public GameObject undeadKnight;
    public GameObject deathMage;
    public GameObject darkHealer;
    public GameObject demonSummon;
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UI = this.gameObject.GetComponent<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SingleFireballAttack(); //test only comment out when done testing: tests single fireball attack
        if (Input.GetKeyDown(KeyCode.P)) PillarsOfFireAttack(); //test only comment out when done testing: tests fire pillar Attack


        if (player.transform.parent != null)
            playerVassal = player.transform.parent.gameObject;
        else playerVassal = null;

        currentHealth = UI.currentHealth;

        if (canAttack)
        {
            //enter code for auto-Attacks here
        }

        if (!canTakeDamage) //is immortal until said otherwise
        {
            currentHealth = MAXHEALTH;
        }


        if(!isDoneMonologuing && !isMonologuing) //if text isn't done cycling, and the monologue hasn't started, start monologuing
        {
            Monologuing();
        }
        else if(isDoneMonologuing) //if hes done talking, do stuff (like summon the first wave of monsters)
        {
            player.GetComponent<PhantomControls>().enabled = true;
            if(playerVassal) playerVassal.GetComponent<BasicMovement>().enabled = true;

            UI.uiObj.SetActive(true);
            TextPanel.SetActive(false);
            firstWaveSummoned = true; //summons the first wave of minions to their set locations
            Instantiate(summonsHealer, summonsHealerLocation.position, Quaternion.identity, null);
            Instantiate(summonsMage, summonsMageLocation.position, Quaternion.identity, null);
            Instantiate(summonsMelee, summonsMeleeLocation.position, Quaternion.identity, null);
            
        }

        if(firstWaveSummoned && firstWaveSummonsKilled == 3) //if the first wave minions are murdered in not so cold blood, summon wave 2
        {
            secondWaveSummoned = true;
            Instantiate(demonOne, demonOneLocation.position, Quaternion.identity, null);
            Instantiate(demonTwo, demonTwoLocation.position, Quaternion.identity, null);
            Instantiate(demonThree, demonThreeLocation.position, Quaternion.identity, null);
        }
    }

    private void Monologuing()
    {
        isMonologuing = true;
        player.GetComponent<PhantomControls>().enabled = false;
        if (playerVassal) playerVassal.GetComponent<BasicMovement>().enabled = false;
        //insert code for the boss monologuing here
    }

    private void SingleFireballAttack() //throws a single fireball at the player
    {            
            
        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

        projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
        projectileBullet.GetComponent<Projectile>().setTarget(player.transform.position);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
        
    }

    private void PillarsOfFireAttack() //Summons pillars of fire at one of 4 set groups of locations
    {
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

    private void summonMinion() //Summons random minion
    {

    }
    
    private void Die()
    {
        //do the die
    }
}
