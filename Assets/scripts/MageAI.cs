using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI : EnemyAI
{

    public float timeTillAttack;
    public float startTimeTillAttack;
    public LayerMask playerLayer;
    public LayerMask AILayer;
    public Transform fireball;

    //bool for chase/attack
    //public bool isPursuing = false;
    //bool isFiring;
    public int fireballDamageAmount = 10;
    public int fireDamageAmount = 5;
    public float fireRange = 2f;

    public float fireballManaLoss = 7f;
    public float fireManaLoss = 2f;


    private float fireballNextRound = 0.0f;
    public float fireballFireRate = 7.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

}
