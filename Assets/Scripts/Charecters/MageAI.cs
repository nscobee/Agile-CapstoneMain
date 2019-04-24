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

    public int fireballDamageAmount = 10;
    public int AOEDamageAmount = 5;
    public float fireRange = 2f;

    public float fireballManaLoss = 5f;
    public float AOEManaLoss = 20f;

    public float mageHp = 40f;
    public float mageAp = 90f;

    public BasicAI basicAI;
    public UIController UI;

    


    private float fireballNextRound = 0.0f;
    public float fireballFireRate = 7.0f;

    public bool playerInRange;
    

    // Use this for initialization
    void Start()
    {
        basicAI = this.gameObject.GetComponent<BasicAI>();
        basicAI.setStats(mageHp, mageAp);
        UI = gameObject.GetComponent<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Possessed")
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (UI.currentMana > fireballManaLoss)
                {
                    print("basic movement script is firing fireball");

                    FireballAttack();
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (UI.currentMana > AOEManaLoss)
                {
                    print("basic movement script is using fire attack");
                    AOEAttack();
                }
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
        
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);
        projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
        projectileBullet.GetComponent<Projectile>().setTarget(target);

        this.GetComponent<UIController>().useMana(fireballManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
         
    }

    public void FireballAttack(Transform playerTransform)
    {
        if (this.gameObject.GetComponent<BasicAI>().canSpawn)
        {
            this.gameObject.GetComponent<BasicAI>().canSpawn = false;
            fireballNextRound = Time.time + fireballFireRate;

            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);
            projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;
            projectileBullet.GetComponent<Projectile>().setTarget(playerTransform.position);

            this.GetComponent<UIController>().useMana(fireballManaLoss);

            //destroys bullet after 4 seconds ish
            Destroy(projectileBullet, 4f);
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

}
