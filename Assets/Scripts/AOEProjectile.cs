using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : MonoBehaviour
{
    private Vector3 target;
    public float speed = 25f;
    //public float maxDistance = 15f;
    public float damage = 15;
    public GameObject ObjectThatSpawnedMe;

    private bool noHit = true;
    

    // Use this for initialization
    void Start()
    {
        if (this.transform.parent.gameObject.GetComponent<MageAI>())
        {
            damage = this.transform.parent.gameObject.GetComponent<MageAI>().AOEDamageAmount;
            damage = damage * Mathf.Pow(2.78f, 0.114f * this.transform.parent.gameObject.GetComponent<BasicAI>().currentLevel);
        }
        if (this.transform.parent.gameObject.GetComponent<healerAI>())
        {
            damage = this.transform.parent.gameObject.GetComponent<healerAI>().fireDamageAmount;
            damage = damage * Mathf.Pow(2.78f, 0.114f * this.transform.parent.gameObject.GetComponent<BasicAI>().currentLevel);
        }
        if (this.transform.parent.gameObject.GetComponent<demonAI>())
        {
            damage = this.transform.parent.gameObject.GetComponent<demonAI>().AOEDamageAmount;
        }
        ObjectThatSpawnedMe = this.transform.parent.gameObject;
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(noHit)
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        

        if (this.transform.position == target)
        {
           // this.gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
            this.gameObject.transform.localScale = new Vector3(20, 20, 1);

            if (ObjectThatSpawnedMe.tag == "Possessed")
                Destroy(this.gameObject, 0.45f);
            else Destroy(this.gameObject, 0.45f);
        }
    }

    public void setTarget(Vector3 targetPoint)
    {
        target = targetPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the triggerd object is in the right layer if it is it adds it to potential list
        //if (other.gameObject.layer == LayerMask.NameToLayer("AI") && other.gameObject != ObjectThatSpawnedMe)
        if(other.gameObject.layer == LayerMask.NameToLayer("AI") && (ObjectThatSpawnedMe.gameObject.tag == "Possessed" ^ other.gameObject.tag == "Possessed"))
        {
            print("Fireball Hit on: " + other.gameObject.name + " and dealt " + damage + " damage");
            if (other.gameObject.GetComponent<UIController>() && other.gameObject.tag != "Possessed" && ObjectThatSpawnedMe.tag == "Possessed")
            {
               // noHit = false;
                
               // this.gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
                this.gameObject.transform.localScale = new Vector3(20, 20, 1);

                other.gameObject.GetComponent<UIController>().takeDamage(damage);
                Destroy(this.gameObject, 0.45f);
                
            }
            else if (other.gameObject.tag == "Possessed")
            {
                //this.gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
                this.gameObject.transform.localScale = new Vector3(20, 20, 1);

                other.gameObject.GetComponent<UIController>().takeDamage(damage);
                
                Destroy(this.gameObject, 0.45f);
            }

        }
    }

    private void OnDestroy()
    {
       ObjectThatSpawnedMe.GetComponent<BasicAI>().canSpawn = true;
    }
}
