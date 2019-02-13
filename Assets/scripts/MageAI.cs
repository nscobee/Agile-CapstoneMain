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
    public float bulletSplashArea = 1f;

    public List<GameObject> inRange = new List<GameObject>();

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

    public Vector3 target;

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

                target = Input.mousePosition;
                Debug.Log("Target point is: " + target.ToString());


                FireballAttack(target);

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
        foreach (GameObject target in inRange)
        {
            target.GetComponent<BasicAI>().currentHP -= fireDamageAmount;
        }
        Debug.Log("Secondary spell used, " + fireDamageAmount + " dmg.");
        this.GetComponent<AIHealth>().LoseMana(fireManaLoss);
    }

    public void FireballAttack(Vector3 targetPoint)
    {
        fireballNextRound = Time.time + fireballFireRate;


        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, bulletSpawn.transform);



        projectileBullet.GetComponent<Rigidbody2D>().velocity = Vector3.MoveTowards(transform.position, targetPoint.normalized, speed * Time.deltaTime);


        this.GetComponent<AIHealth>().LoseMana(fireManaLoss);

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
