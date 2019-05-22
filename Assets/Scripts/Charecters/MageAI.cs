using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI : BasicAI
{

    public float timeTillAttack;
    public float startTimeTillAttack;
    public LayerMask playerLayer;
    public LayerMask AILayer;
    public Transform bulletSpawn;
    public GameObject bullet;
    public GameObject AOEbullet;
    public float bulletSplashArea = 1f;

    public List<GameObject> inRange = new List<GameObject>();

    public float fireballDamageAmount = 10;
    public float AOEDamageAmount = 5;
    public float fireRange = 2f;

    public float fireballManaLoss = 5f;
    public float AOEManaLoss = 20f;

    public float mageHp = 40f;
    public float mageAp = 90f;

    public BasicAI basicAI;
    public UIController UI;
    public Animator anim;
    

    


    private float fireballNextRound = 0.0f;
    public float fireballFireRate = 7.0f;

    public bool playerInRange;

    [Header("Audio Stuff")]
    public AudioClip primaryAttackSound;
    public AudioClip secondaryAttackSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    // Use this for initialization
    void Start()
    {
        basicAI = this.gameObject.GetComponent<BasicAI>();
       // basicAI.setStats(mageHp, mageAp);
        UI = gameObject.GetComponent<UIController>();
        anim = this.gameObject.GetComponent<Animator>();
        primaryAttackSound = basicAI.MagicAttackSound;
        secondaryAttackSound = basicAI.BigMagicAttackSound;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Possessed" && Time.timeScale > 0)
        {
            if (Input.GetMouseButtonDown(0) && Time.time > UI.nextPrimaryFire)
            {
                if (UI.currentMana > fireballManaLoss)
                {
                    print("basic movement script is firing fireball");

                    FireballAttack();
                    UI.nextPrimaryFire = Time.time + UI.primaryFireRate;
                    source.PlayOneShot(primaryAttackSound);
                    UI.primaryCooldown.maxValue = UI.nextPrimaryFire;
                    UI.primaryCooldown.minValue = UI.primaryCooldown.maxValue - UI.primaryFireRate;
                    if (UI.isFirstPrimaryAttack) UI.isFirstPrimaryAttack = false;
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (UI.currentMana > AOEManaLoss && Time.time > UI.nextSecondaryFire)
                {
                    print("basic movement script is using fire attack");
                    AOEAttack();
                    UI.nextSecondaryFire = Time.time + UI.secondaryFireRate;
                    source.PlayOneShot(secondaryAttackSound);
                    UI.secondaryCooldown.maxValue = UI.nextSecondaryFire;
                    UI.secondaryCooldown.minValue = UI.secondaryCooldown.maxValue - UI.secondaryFireRate;
                    if (UI.isFirstSecondaryAttack) UI.isFirstSecondaryAttack = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) //cheat code to make u stronk

        {
            if (this.gameObject.tag == "Possessed")
            {
                fireballDamageAmount = 100;
                AOEDamageAmount = 100;
            }
        }


    }

    public void AOEAttack()
    {
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        GameObject projectileBulletAOE = Instantiate(AOEbullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);
        
        projectileBulletAOE.GetComponent<AOEProjectile>().damage = AOEDamageAmount;
        projectileBulletAOE.GetComponent<AOEProjectile>().setTarget(target);

        this.GetComponent<UIController>().useMana(AOEManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBulletAOE, 4f);
    }

    public void AOEAttack(Transform playerTransform)
    {
        if (this.gameObject.GetComponent<BasicAI>().canSpawn)
        {
            this.gameObject.GetComponent<BasicAI>().canSpawn = false;

            fireballNextRound = Time.time + fireballFireRate;

            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            GameObject projectileBulletAOE = Instantiate(AOEbullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

            projectileBulletAOE.GetComponent<AOEProjectile>().damage = AOEDamageAmount;
            projectileBulletAOE.GetComponent<AOEProjectile>().setTarget(playerTransform.position);

            this.GetComponent<UIController>().useMana(AOEManaLoss);

            //destroys bullet after 4 seconds ish
            Destroy(projectileBulletAOE, 4f);
        }
    }

    public void FireballAttack()
    {
        if(anim)
        anim.SetTrigger("IsPrimaryAttacking");
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        /* GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

         projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
         projectileBullet.GetComponent<Projectile>().setTarget(target);

         this.GetComponent<UIController>().useMana(fireballManaLoss);

         //destroys bullet after 4 seconds ish
         Destroy(projectileBullet, 4f);*/
        StartCoroutine(AttackAtAnimationTime(.1f, target));
         
    }

    public void FireballAttack(Transform playerTransform)
    {
        
        if (this.gameObject.GetComponent<BasicAI>().canSpawn)
        {
            if(anim)
            anim.SetTrigger("IsPrimaryAttacking");
            this.gameObject.GetComponent<BasicAI>().canSpawn = false;
            fireballNextRound = Time.time + fireballFireRate;

            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            /*GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);
            
            projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
            projectileBullet.GetComponent<Projectile>().setTarget(playerTransform.position);

            this.GetComponent<UIController>().useMana(fireballManaLoss);

            //destroys bullet after 4 seconds ish
            Destroy(projectileBullet, 4f); */
            StartCoroutine(AttackAtAnimationTime(.1f, playerTransform));
        }
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

    private IEnumerator AttackAtAnimationTime(float animTime, Vector3 target)
    {

        yield return new WaitForSeconds(animTime);
        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

        projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
        projectileBullet.GetComponent<Projectile>().setTarget(target);

        this.GetComponent<UIController>().useMana(fireballManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
    }

    private IEnumerator AttackAtAnimationTime(float animTime, Transform playerTransform)
    {

        yield return new WaitForSeconds(animTime);
        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);

        projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
        projectileBullet.GetComponent<Projectile>().setTarget(playerTransform.position);

        this.GetComponent<UIController>().useMana(fireballManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
    }

}
