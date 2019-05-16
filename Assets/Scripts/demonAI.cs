using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonAI : BasicAI
{
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
    // public Transform hitboxOrigin;

    public float fireballNextRound = 0;
    public float fireballFireRate = 5;
    public GameObject AOEbullet;
    public GameObject bulletSpawn;
    public float AOEDamageAmount;

    //private int currentPlayerLevel;
    //public float damageMultiplier;

    public bool playerInRange;
    public Animator hitboxAnim;



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
        hitboxAnim = meleeHitbox.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        

       
    }

    public void meleeAttack(float damage)
    {

        isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage;
        meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;

        //destroys bullet after 4 seconds ish
        // Destroy(swordHitbox);
        hitboxAnim.SetTrigger("Attack");
        meleeHitbox.GetComponent<meleeRange>().isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage;
        //meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;

        //destroys bullet after 4 seconds ish
        // Destroy(swordHitbox);
        StartCoroutine(FinishedAnim());
    }

    public void AOEAttack(Transform playerTransform)
    {
        print("demon using mage attack!");
        if (this.gameObject.GetComponent<BasicAI>().canSpawn)
        {
            this.gameObject.GetComponent<BasicAI>().canSpawn = false;

            fireballNextRound = Time.time + fireballFireRate;

            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            GameObject projectileBulletAOE = Instantiate(AOEbullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

            projectileBulletAOE.GetComponent<AOEProjectile>().damage = AOEDamageAmount;
            projectileBulletAOE.GetComponent<AOEProjectile>().setTarget(playerTransform.position);

           // this.GetComponent<UIController>().useMana(AOEManaLoss);

            //destroys bullet after 4 seconds ish
            Destroy(projectileBulletAOE, 4f);
        }
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

    private IEnumerator FinishedAnim()
    {
        yield return new WaitForSeconds(.5f);
        meleeHitbox.GetComponent<meleeRange>().isAttacking = false;
    }
}
