using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI : BasicAI
{

    public float timeTillAttack;
    public float startTimeTillAttack;
    public LayerMask playerLayer;
    public LayerMask AILayer;
    public Transform fireball;
    public Transform bulletSpawn;
    public GameObject bullet;
    public float bulletSplashArea = 1f;
    //bool for chase/attack
    //public bool isPursuing = false;
    //bool isFiring;
    public int fireballDamageAmount = 10;
    public int fireDamageAmount = 5;
    public float fireRange = 2f;

    public float fireballManaLoss = 7f;
    public float fireManaLoss = 2f;

    public float mageHp = 40f;
    public float mageAp = 90f;

    public BasicAI basicAI;


    private float fireballNextRound = 0.0f;
    public float fireballFireRate = 7.0f;

    public bool playerInRange;



    // Use this for initialization
    void Start()
    {
        basicAI = this.gameObject.GetComponent<BasicAI>();
        basicAI.setStats(mageHp, mageAp);

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("basic movement script is firing fireball");
                FireballAttack();
            }
            if (Input.GetMouseButton(1))
            {
                print("basic movement script is using fire attack");
                FireAttack();
            }
        }

        
    }

    public void FireAttack()
    {
        Collider2D[] thingsToHit = Physics2D.OverlapCircleAll(Input.mousePosition, fireRange);
        for (int i = 0; i < thingsToHit.Length; i++)
        {
            if (thingsToHit[i].tag == "Player")
            {
                thingsToHit[i].GetComponent<playerController>().takeDamage(fireDamageAmount);
                thingsToHit[i].GetComponent<AIHealth>().LoseMana(fireballManaLoss);
            }
            if(thingsToHit[i].tag == "mage")
            {
                thingsToHit[i].GetComponent<AIHealth>().TakeDamage(fireDamageAmount);
                
            }
        }
        Debug.Log("Secondary spell used, " + fireDamageAmount + " dmg.");
        this.GetComponent<AIHealth>().LoseMana(fireManaLoss);
    }

    public void FireballAttack()
    {
        //isFiring = true;
        fireballNextRound = Time.time + fireballFireRate;
        var projectileBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        projectileBullet.transform.position += projectileBullet.transform.forward * 25f;
        this.GetComponent<AIHealth>().LoseMana(fireManaLoss);

        
        Collider2D[] thingsToHit = Physics2D.OverlapCircleAll(fireball.transform.position, bulletSplashArea);
        for (int i = 0; i < thingsToHit.Length; i++)
        {
            if(thingsToHit[i].tag == "player")
            {
                thingsToHit[i].GetComponent<playerController>().takeDamage(fireballDamageAmount);
            }
            if (thingsToHit[i].tag == "mage")
            {
                thingsToHit[i].GetComponent<AIHealth>().TakeDamage(fireballDamageAmount);
            }
        }
        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
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
