using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
    public float runMultiplier;
    private bool isRunning = false;
    private float MAXSPEED;
    private float BASESPEED;

    //health
    public float health = 100f;
    public bool isDead = false;
    
	// Use this for initialization
	void Start () {
        speed = 10;
        BASESPEED = speed;
        MAXSPEED = speed * runMultiplier;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed = BASESPEED;
        }
        if (isRunning)
        {
            speed *= runMultiplier;
            if (speed > MAXSPEED) speed = MAXSPEED;
        }

        //movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }

        //health system
        if (health <= 0 && !isDead)
        {
            isDead = true;
            this.enabled = !this.enabled;
        }


    }
    public void takeDamage(int damage)
    {
        health -= damage;
        //Debug.Log("damaged");
    }
}
