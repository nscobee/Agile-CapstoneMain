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
        if(this.transform.parent.gameObject.GetComponent<MageAI>())
        damage = this.transform.parent.gameObject.GetComponent<MageAI>().fireballDamageAmount;
        if (this.transform.parent.gameObject.GetComponent<healerAI>())
            damage = this.transform.parent.gameObject.GetComponent<healerAI>().fireDamageAmount;
        ObjectThatSpawnedMe = this.transform.parent.gameObject;
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (noHit)
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);

        if (this.transform.position == target)
        {
           Destroy(this.gameObject, 0.5f);
        }
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
            print("Fireball Hit on: " + other.gameObject.name);

            if (other.gameObject.GetComponent<UIController>() && other.gameObject.tag != "Possessed")
            {
                noHit = false;
                other.gameObject.GetComponent<UIController>().takeDamage(damage);
            }

        }
    }
}
