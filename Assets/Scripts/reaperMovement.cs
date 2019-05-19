using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reaperMovement : MonoBehaviour {

    public GameObject phantom;

    public float moveSpeed;
    private float nextCheckForSpeedSlow;
    private float nextCheckForSpeedIncrease;
    public float increaseSpeed = 1.5f;
    public float decreaseSpeed = 3f;
    public float increaseSpeedFactor = 0.1f;
    public float decreaseSpeedFactor = 0.1f;
    public float MAXSPEED = 1.5f;
    public float MINSPEED = 8f;

	// Use this for initialization
	void Start () {
        phantom = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(!phantom.GetComponent<PhantomControls>().isPossessing)
        {
            transform.position = Vector3.MoveTowards(transform.position, phantom.transform.position, moveSpeed * Time.deltaTime);

            if(Time.time > nextCheckForSpeedIncrease && moveSpeed < MAXSPEED)
            {
                moveSpeed += increaseSpeedFactor;
                nextCheckForSpeedIncrease = Time.time + increaseSpeed;
            }
        }
        else if (Time.time > nextCheckForSpeedSlow && moveSpeed < MINSPEED)
        {
            moveSpeed -= decreaseSpeedFactor;
            nextCheckForSpeedSlow = Time.time + decreaseSpeed;
        }
    }

}

