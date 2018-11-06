using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Transform target;
    public float damage = 10.0f;

    //public Collider attackArea;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print(target.name + " collided with " + this.name);

        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(target.name + " collided with " + this.name);
        this.transform.LookAt(target);

    }
}
