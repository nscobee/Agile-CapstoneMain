using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : BasicAI {

    public bool isAttacking;
    public float nextAttack;
    public float attackRate;

    public float weakAttackDamage;
    public float strongAttackDamage;

    public float weakManaLoss;
    public float strongManaLoss;

    public float activeDamage;
  
    public BasicAI basicAI;
    private UIController UI;
    public GameObject meleeHitbox;
    public Animator anim;
   // public Transform hitboxOrigin;


    //private int currentPlayerLevel;
    //public float damageMultiplier;

    public float fighterHp = 90f;
    public float fighterAp = 40f;

    public bool playerInRange;



    public List<GameObject> inRange = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        basicAI = this.gameObject.GetComponent<BasicAI>();
        phantomControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PhantomControls>();
        //currentPlayerLevel = phantomControls.currentLevel;
        //damageMultiplier *= currentPlayerLevel;
        UI = gameObject.GetComponent<UIController>();

        //basicAI.setStats(fighterHp, fighterAp);
        meleeHitbox = this.gameObject.transform.GetChild(0).gameObject;
        meleeHitbox.GetComponent<BoxCollider2D>().enabled = false;
        anim = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (this.gameObject.tag == "Possessed")
        {

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > UI.nextPrimaryFire)
            {
                //print(UI.currentMana);
                if (UI.currentMana > 0)
                {
                    meleeAttack(weakAttackDamage, weakManaLoss);
                    UI.nextPrimaryFire = Time.time + UI.primaryFireRate;
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > UI.nextSecondaryFire)
            {
                if (UI.currentMana > 0)
                {
                    meleeAttack(strongAttackDamage, strongManaLoss);
                    UI.nextSecondaryFire = Time.time + UI.secondaryFireRate;
                }
            }
        }
    }

    public void meleeAttack(float damage)
    {
        anim.SetTrigger("IsAttacking");
        isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage;
        meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;

        //destroys bullet after 4 seconds ish
        // Destroy(swordHitbox);
    }
    public void meleeAttack(float damage, float manaLoss)
    {

        anim.SetTrigger("IsAttacking"); //replace with diff trigger for strong melee attk
        isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage * Mathf.Pow(2.78f, 0.114f * basicAI.currentLevel);
        meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;

        this.GetComponent<UIController>().useMana(manaLoss);

        //destroys bullet after 4 seconds ish
        //Destroy(swordHitbox);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }


}
