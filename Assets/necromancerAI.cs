using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class necromancerAI : MonoBehaviour
{
    public GameObject player;
    public GameObject playerVassal;

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

    [Header("List of Positions for fire pillar attack")]
    public Vector3[] pillarLocationGroup1;
    public Vector3[] pillarLocationGroup2;
    public Vector3[] pillarLocationGroup3;
    public Vector3[] pillarLocationGroup4;

    [Header("Various Summonable Minions")]
    public GameObject undeadKnight;
    public GameObject deathMage;
    public GameObject darkHealer;
    public GameObject demonSummon;
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.parent != null)
            playerVassal = player.transform.parent.gameObject;
        else playerVassal = null;

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
        //insert code for the boss monologuing here
    }

    private void SingleFireballAttack() //throws a single fireball at the player
    {

    }

    private void PillarsOfFireAttack() //Summons pillars of fire at one of 4 set groups of locations
    {

    }

    private void summonMinion() //Summons random minion
    {

    }
    
    private void Die()
    {
        //do the die
    }
}
