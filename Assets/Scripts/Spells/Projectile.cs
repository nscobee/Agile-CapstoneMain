using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    public float speed = 25f;
    //public float maxDistance = 15f;
    public float damage;
    private GameObject ObjectThatSpawnedMe;

    private bool noHit = true;

    // Use this for initialization
    void Start()
    {
        print(this.gameObject.transform.parent.gameObject);
        ObjectThatSpawnedMe = this.gameObject.transform.parent.gameObject;
        if (ObjectThatSpawnedMe.GetComponent<BasicAI>().startingTag == "mage")
        {
            damage = this.transform.parent.gameObject.GetComponent<MageAI>().fireballDamageAmount;
            damage = damage * Mathf.Pow(2.78f, 0.114f * this.transform.parent.gameObject.GetComponent<BasicAI>().currentLevel);
        }
        if (ObjectThatSpawnedMe.GetComponent<BasicAI>().startingTag == "healer")
        {
            damage = this.transform.parent.gameObject.GetComponent<healerAI>().fireDamageAmount;
            damage = damage * Mathf.Pow(2.78f, 0.114f * this.transform.parent.gameObject.GetComponent<BasicAI>().currentLevel);
        }
        
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
 
        if (noHit)
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);

        if (this.transform.position == target && ObjectThatSpawnedMe.tag == "Possessed")
        {
           Destroy(this.gameObject, 0.5f);
        }

        if (this.transform.position == target && ObjectThatSpawnedMe.tag != "Possessed")
            Destroy(this.gameObject, 4f);
    }

    public void setTarget(Vector3 targetPoint)
    {
        target = targetPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the triggerd object is in the right layer if it is it adds it to potential list
        if (other.gameObject.layer == LayerMask.NameToLayer("AI") && other.gameObject != ObjectThatSpawnedMe)
        {
            

            if (other.gameObject.GetComponent<UIController>() && other.gameObject.tag != "Possessed")
            {
                noHit = false;
                other.gameObject.GetComponent<UIController>().takeDamage(damage);
                damage = 0;
            }
            else if(other.gameObject.tag == "Possessed")
            {
                print("Fireball Hit on: " + other.gameObject.name + " and dealt " + damage + " damage");
                other.gameObject.GetComponent<UIController>().takeDamage(damage);
                damage = 0;
            }

        }
    }

    private void OnDestroy()
    {
        ObjectThatSpawnedMe.GetComponent<BasicAI>().canSpawn = true;
    }
}
