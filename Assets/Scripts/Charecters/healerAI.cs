﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerAI : BasicAI {

   // public bool isFiring;
    //public float nextRound;
    public GameObject bullet;
    public Transform bulletSpawn;





    public BasicAI basicAI;
    public Animator anim;

    public float healerHp = 45f;
    public float healerAp = 75f;

    public float fireballDamageAmount = 10;
    public float fireDamageAmount = 5;
    public float fireRange = 2f;

    public float healManaLoss = 5f;

    public float fireballManaLoss = 7f;
    public float fireManaLoss = 2f;

    private float fireballNextRound = 0.0f;
    public float fireballFireRate = 7.0f;

    public float healPlayerAmount;
    public float healAIAmount;

    //private int currentPlayerLevel;
    public float healMultiplier;

    public bool playerInRange;
    
    public List<GameObject> inRange = new List<GameObject>();

    [Header("Audio Stuff")]
    public AudioClip primaryAttackSound;
    public AudioClip secondaryAttackSound;
    private AudioSource source;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        basicAI = this.gameObject.GetComponent<BasicAI>();
        UIControls = this.gameObject.GetComponent<UIController>();
        anim = this.gameObject.GetComponent<Animator>();
        // currentPlayerLevel = phantomControls.currentLevel;
        //healMultiplier *= currentPlayerLevel;
        //basicAI.setStats(healerHp, healerAp);
        primaryAttackSound = basicAI.MagicAttackSound;
        secondaryAttackSound = basicAI.HealSound;


    }
	
	// Update is called once per frame
	void Update () {

        if (this.gameObject.tag == "Possessed")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > UIControls.nextPrimaryFire)
            {
                if (UIControls.currentMana > 0)
                {
                    FireAttack();
                    source.PlayOneShot(primaryAttackSound);
                }
                UIControls.nextPrimaryFire = Time.time + UIControls.primaryFireRate;

            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > UIControls.nextSecondaryFire)
            {
                if (UIControls.currentMana > 0)
                {
                    Heal();
                    source.PlayOneShot(secondaryAttackSound);
                }
                UIControls.nextSecondaryFire = Time.time + UIControls.secondaryFireRate;
            }
        }
    }


    public void FireAttack()
    {
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.gameObject.transform);
        
        projectileBullet.GetComponent<Projectile>().damage = fireballDamageAmount;

        projectileBullet.GetComponent<Projectile>().setTarget(target);

        this.GetComponent<UIController>().useMana(fireManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
    }

    public void FireAttack(Transform playerTransform)
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

    public void Heal()
    {
        if (this.gameObject.tag == "Possessed")
        {
            UIControls.currentHealth += healPlayerAmount;
            this.GetComponent<UIController>().useMana(healManaLoss);
        }
        else
        {
            foreach (GameObject target in inRange)
            {
                target.GetComponent<UIController>().currentHealth += healAIAmount;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
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
