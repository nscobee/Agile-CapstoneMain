using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeRange : MonoBehaviour {

    public GameObject meleeChar;

	// Use this for initialization
	void Start ()
    {
        meleeChar = transform.parent.gameObject;
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the triggerd object is in the right layer if it is it adds it to potential list
        if (other.gameObject.layer == LayerMask.NameToLayer("AI"))
        {
            print("Melee Hit on: " + other.gameObject.name);
            other.gameObject.GetComponent<UIController>().takeDamage(meleeChar.GetComponent<MeleeAI>().activeDamage);

        }
    }



  
}
