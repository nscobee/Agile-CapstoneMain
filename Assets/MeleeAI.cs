using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour {

    public bool isAttacking;
    public float nextAttack;
    public float attackRate;

    public float weakAttackDamage;
    public float strongAttackDamage;

    public float activeDamage;
  
    public PhantomControls phantomControls;

    public BasicAI basicAI;
    public GameObject meleeHitbox;
    public Transform hitboxOrigin;


    private int currentPlayerLevel;
    public float damageMultiplier;

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
        damageMultiplier *= currentPlayerLevel;

        basicAI.setStats(fighterHp, fighterAp);

    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                meleeAttack(weakAttackDamage);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                meleeAttack(strongAttackDamage);
            }
        }
    }


    public void meleeAttack(float damage)
    {
        isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage;
        var swordHitbox = Instantiate(meleeHitbox, hitboxOrigin.position, hitboxOrigin.rotation, this.gameObject.transform);

        //destroys bullet after 4 seconds ish
        Destroy(swordHitbox, 0.1f);
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

    /* public void Heal()
     {
         if (phantomControls.isPossessing)
             basicAI.currentHP += healPlayerAmount;
         else
         {
             foreach (GameObject target in inRange)
             {
                 target.GetComponent<BasicAI>().currentHP += healAIAmount;
             }
         }
     }*/
}
