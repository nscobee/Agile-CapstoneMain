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
                if (UI.currentMana > 0)
                {
                    print("basic movement script is firing fireball");

                    FireballAttack();
                }

            }
            if (Input.GetMouseButton(1))
            {
                if (UI.currentMana > 0)
                {
                    print("basic movement script is using fire attack");
                    FireAttack();
                }
            }
        }


    }

    public void FireAttack()
    {
        foreach (GameObject target in inRange)
        {
            target.GetComponent<UIController>().currentHealth -= fireDamageAmount;
        }
        Debug.Log("Secondary spell used, " + fireDamageAmount + " dmg.");
        this.GetComponent<UIController>().useMana(fireManaLoss);
    }

    public void FireballAttack()
    {
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity, this.transform);

        projectileBullet.GetComponent<Projectile>().setTarget(target);

        this.GetComponent<UIController>().useMana(fireManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
    }

    public void FireballAttack(Transform playerTransform)
    {
        fireballNextRound = Time.time + fireballFireRate;

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        GameObject projectileBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);

        projectileBullet.GetComponent<Projectile>().setTarget(playerTransform.position);

        this.GetComponent<UIController>().useMana(fireManaLoss);

        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
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
