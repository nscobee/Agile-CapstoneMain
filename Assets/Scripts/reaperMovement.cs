using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reaperMovement : MonoBehaviour {

    public GameObject phantom;

    public float moveSpeed;

	// Use this for initialization
	void Start () {
        phantom = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(!phantom.GetComponent<PhantomControls>().isPossessing)
        {
            transform.position = Vector3.MoveTowards(transform.position, phantom.transform.position, moveSpeed * Time.deltaTime);
        }

	}
}
