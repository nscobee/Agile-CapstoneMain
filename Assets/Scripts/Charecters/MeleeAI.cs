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
    public Animator hitboxAnim;
   // public Transform hitboxOrigin;


    //private int currentPlayerLevel;
    //public float damageMultiplier;

    public float fighterHp = 90f;
    public float fighterAp = 40f;

    public bool playerInRange;

    [Header("Audio Stuff")]
    public AudioClip primaryAttackSound;
    public AudioClip secondaryAttackSound;
    private AudioSource source;



    public List<GameObject> inRange = new List<GameObject>();

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

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
        meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;
        anim = this.gameObject.GetComponent<Animator>();
        hitboxAnim = meleeHitbox.GetComponent<Animator>();
        primaryAttackSound = basicAI.swordAttackSound;
        secondaryAttackSound = basicAI.heavySwordAttackSound;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (this.gameObject.tag == "Possessed" && Time.timeScale > 0)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > UI.nextPrimaryFire)
            {
                //print(UI.currentMana);
                if (UI.currentMana > weakManaLoss)
                {
                    meleeAttack(weakAttackDamage, weakManaLoss);
                    UI.nextPrimaryFire = Time.time + UI.primaryFireRate;
                    source.PlayOneShot(primaryAttackSound);
                    UI.primaryCooldown.maxValue = UI.nextPrimaryFire;                    
                    UI.primaryCooldown.minValue = UI.primaryCooldown.maxValue - UI.primaryFireRate;
                    if (UI.isFirstPrimaryAttack) UI.isFirstPrimaryAttack = false;
                    

                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > UI.nextSecondaryFire)
            {
                if (UI.currentMana > strongManaLoss)
                {
                    meleeAttack(strongAttackDamage, strongManaLoss);
                    UI.nextSecondaryFire = Time.time + UI.secondaryFireRate;
                    source.PlayOneShot(secondaryAttackSound);
                    UI.secondaryCooldown.maxValue = UI.nextSecondaryFire;
                    UI.secondaryCooldown.minValue = UI.secondaryCooldown.maxValue - UI.secondaryFireRate;
                    if (UI.isFirstSecondaryAttack) UI.isFirstSecondaryAttack = false;
                }
            }
        }
    }

    public void meleeAttack(float damage)
    {
        anim.SetTrigger("IsAttacking");
        hitboxAnim.SetTrigger("Attack");
        meleeHitbox.GetComponent<meleeRange>().isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage;
        //meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;

        //destroys bullet after 4 seconds ish
        // Destroy(swordHitbox);
        StartCoroutine(FinishedAnim());
    }
    public void meleeAttack(float damage, float manaLoss)
    {

        anim.SetTrigger("IsAttacking"); //replace with diff trigger for strong melee attk
        hitboxAnim.SetTrigger("Attack");
        meleeHitbox.GetComponent<meleeRange>().isAttacking = true;
        nextAttack = Time.time + attackRate;
        activeDamage = damage * Mathf.Pow(2.78f, 0.114f * basicAI.currentLevel);
        //meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;

        this.GetComponent<UIController>().useMana(manaLoss);

        //destroys bullet after 4 seconds ish
        //Destroy(swordHitbox);
        StartCoroutine(FinishedAnim());
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
