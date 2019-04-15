using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : BasicAI {

    public bool isAttacking;
    public float nextAttack;
    public float attackRate;

    public float weakAttackDamage;
    public float strongAttackDamage;

    public float activeDamage;
  
    private BasicAI basicAI;
    private UIController uiControl;
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
        uiControl = this.gameObject.GetComponent<UIController>();
        phantomControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PhantomControls>();
        //currentPlayerLevel = phantomControls.currentLevel;
        damageMultiplier *= currentPlayerLevel;

        basicAI.setStats(fighterHp, fighterAp);

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Possessed")
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
        if (other.gameObject.name == "Phantom2.0")
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Phantom2.0")
        {
            playerInRange = false;
        }
    }


}
