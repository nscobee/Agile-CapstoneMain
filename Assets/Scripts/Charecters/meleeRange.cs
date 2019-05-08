using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeRange : MonoBehaviour {

    public GameObject meleeChar;
    public bool isAttacking = false;
    
    //private bool isColliding;
    

    // Use this for initialization
    void Start ()
    {
        meleeChar = transform.parent.gameObject;
	}

    // Update is called once per frame
    void Update()
    {
        //isColliding = false;
       // if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
          //  this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the triggerd object is in the right layer if it is it adds it to potential list
        if (other.gameObject.layer == LayerMask.NameToLayer("AI") && isAttacking)
        {
           // if (isColliding) return;
            //isColliding = true;
            print("Melee Hit on: " + other.gameObject.name);
            if(meleeChar.GetComponent<MeleeAI>() != null)
                other.gameObject.GetComponent<UIController>().takeDamage(meleeChar.GetComponent<MeleeAI>().activeDamage);
            else if(meleeChar.GetComponent<demonAI>() != null)
                other.gameObject.GetComponent<UIController>().takeDamage(meleeChar.GetComponent<demonAI>().activeDamage);
            //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isAttacking = false;
         

        }
    }



  
}
