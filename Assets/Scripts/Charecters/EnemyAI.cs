using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //float variables
    private float nextRound = 0.0f;
    public float fireRate = 7.0f;
    public float moveSpeed = 2.0f;
    public float radiusOfSatisfaction = 2.0f;

    //transforms and objects
    public Transform playerObjTransform;
    public Transform shooterObjTransform;
    public Transform bulletSpawn;
    public GameObject bullet;
    public GameObject shooterObj;
    public float bulletSplashArea = 3f;
    
    //patrolling/detection stuff (to be added later)
    //public GameObject[] waypoints = new GameObject[NUM_WAYPOINTS];
    //public int waypointNumber = 0;
    //public bool isAtWaypoint = false;
    //int waypointNum = 0;
    //public const int NUM_WAYPOINTS = 4;

    EnemyAI movementScript;
    playerController playerScript;
    public bool playerInRange = false;

    //bool for chase/attack
    public bool isPursuing = false;
    bool isFiring;
    public int damageAmount = 10;

    //health stuff
    public float shooterHealth = 5.0f;
    bool isDead = false;

    // Use this for initialization
    void Start()
    {
        playerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        EnemyAI movementScript = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        /**isPatrolling = true;
        *isFiring = false;
        *if (isPatrolling)
        *{
        *    PatrolToNextWaypoint();
        *}
        *else
        *{
        *    //detecting player, so do chasePlayer()
        *}
        */

        //detecting player
        isFiring = false;
        if (playerInRange)
        {
            ChasePlayer();
        }
        else if (!playerInRange)
        {
            //isPatrolling = true;
            isPursuing = false;
        }

        //health system
        if (shooterHealth <= 0 && !isDead)
        {
            isDead = true;
            //isPatrolling = false;
            Destroy(shooterObj, 10f);
            movementScript.enabled = !movementScript.enabled;
        }
    }

    public void ChasePlayer()
    {
        shooterObj.transform.LookAt(playerObjTransform);
        //isPatrolling = false;
        isPursuing = true;
        float shooterYPos = transform.position.y;
        Vector3 customPos = playerObjTransform.transform.position;
        customPos.y = shooterYPos;
        shooterObj.transform.LookAt(customPos);
        shooterObj.transform.position += shooterObj.transform.forward * moveSpeed * Time.deltaTime;

        if (Time.time > nextRound)
        {
            Attack();
        }
    }//end chasePlayer

    public void Attack()
    {
        isFiring = true;
        nextRound = Time.time + fireRate;
        var projectileBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        projectileBullet.transform.position += projectileBullet.transform.forward * 10f;
       
        //destroys bullet after 4 seconds ish
        Destroy(projectileBullet, 4f);
    }

    private void OnTriggerEnter(Collider sceneCollider)
    {
        if (sceneCollider.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider sceneCollider)
    {
        if (sceneCollider.tag == "Player")
        {
            playerInRange = false;
        }
    }
}





