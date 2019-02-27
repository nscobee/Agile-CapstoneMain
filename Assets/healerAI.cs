using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerAI : MonoBehaviour {

    public bool isFiring;
    public float nextRound;
    public float fireRate;
    public GameObject bullet;
    public Transform bulletSpawn;

    public float speed = 5f;

    public PhantomControls phantomControls;

    public BasicAI basicAI;

    public float healerHp = 45f;
    public float healerAp = 75f;

    public int fireballDamageAmount = 10;
    public int fireDamageAmount = 5;
    public float fireRange = 2f;

    public float fireballManaLoss = 7f;
    public float fireManaLoss = 2f;

    private float fireballNextRound = 0.0f;
    public float fireballFireRate = 7.0f;

    public float healPlayerAmount;
    public float healAIAmount;

    private int currentPlayerLevel;
    public float healMultiplier;

    public bool playerInRange;
    
    public List<GameObject> inRange = new List<GameObject>();

    // Use this for initialization
    void Start () {
        basicAI = this.gameObject.GetComponent<BasicAI>();
       // currentPlayerLevel = phantomControls.currentLevel;
        healMultiplier *= currentPlayerLevel;
        basicAI.setStats(healerHp, healerAp);

    }
	
	// Update is called once per frame
	void Update () {

        if (this.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FireAttack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Heal();
            }
        }
    }


    public void FireAttack()
    {
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);

        projectileBullet.GetComponent<Projectile>().setTarget(target);

        this.GetComponent<AIHealth>().LoseMana(fireManaLoss);

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
