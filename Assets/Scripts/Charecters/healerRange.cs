using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerRange : MonoBehaviour {

    public healerAI healer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the triggerd object is in the right layer if it is it adds it to potential list
        if (other.gameObject.layer == LayerMask.NameToLayer("AI"))
        {


        }
    }

    private void On2DTriggerExit2D(Collider2D other)
    {
        // checks if its in the right layer then if it is it removes it from potential list
        if (other.gameObject.layer == LayerMask.NameToLayer("AI"))
        {
            healer.inRange.Remove(other.gameObject);

        }
    }
   
}
