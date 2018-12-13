using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerAI : MonoBehaviour {

    public bool isFiring;
    public float nextRound;
    public float fireRate;
    public GameObject bullet;
    public Transform bulletSpawn;

    public PhantomControls phantomControls;

    public BasicAI basicAI;

    public float healPlayerAmount;
    public float healAIAmount;

    private int currentPlayerLevel;
    public float healMultiplier;

        

    public List<GameObject> inRange = new List<GameObject>();

    // Use this for initialization
    void Start () {
        currentPlayerLevel = phantomControls.currentLevel;
        healMultiplier *= currentPlayerLevel;
    }
	
	// Update is called once per frame
	void Update () {

        
        

        if (Input.GetKeyDown(KeyCode.Mouse0) && phantomControls.isPossessing)
        {
            FireAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && phantomControls.isPossessing)
        {
            Heal();
        }
    }


    public void FireAttack()
    {
        isFiring = true;
        nextRound = Time.time + fireRate;
        var projectileBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        projectileBullet.GetComponent<Rigidbody2D>().velocity = projectileBullet.transform.forward * 10f;

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
    }

    public void Heal()
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
    }

    
}
