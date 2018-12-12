using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayerHit : MonoBehaviour
{
    playerHealth playerScript;
    MageAI mageai;
    AIHealth aiHealth;
    BasicAI basicAI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit a thing");
        if (other.tag == "Player")
        {
            playerScript = other.GetComponent<playerHealth>();
            Debug.Log("Player hit, taking damage");
            playerScript.takeDamage(mageai.fireballDamageAmount);
            this.enabled = false;
        }

        if (other.tag == "mage")
        {
            aiHealth = other.GetComponent<AIHealth>();
            Debug.Log("ai hit, taking damage");
            if(mageai == null)
            {
                Debug.Log("Mageai is null");
            }
            if (other.gameObject == null)
            {
                Debug.Log("game object is null?");
            }
            if (other.gameObject.GetComponent<AIHealth>() == null)
            {
                Debug.Log("aihealth is null");
            }
            
            other.gameObject.GetComponent<AIHealth>().TakeDamage(mageai.fireballDamageAmount);
        }
    }
    // Use this for initialization
    void Start ()
    {
        
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
        basicAI = GameObject.FindGameObjectWithTag("mage").GetComponent<BasicAI>();
        //get if mage info
        mageai = GameObject.FindGameObjectWithTag("mage").GetComponent<MageAI>();
    }
    
}
