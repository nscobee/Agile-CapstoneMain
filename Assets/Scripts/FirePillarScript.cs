using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarScript : MonoBehaviour
{
    public float impactDamage = 20f;
    public float DOTDamage = 5f;

    public float damageRate = 0.5f;
    public float nextDamage = 0;

    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the triggerd object is in the right layer if it is it adds it to potential list
        if (other.gameObject.tag == "Possessed")
        {
            print("FirePillar Hit on: " + other.gameObject.name + " and dealt " + impactDamage + " damage");
            other.gameObject.GetComponent<UIController>().takeDamage(impactDamage);

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Possessed")
            if(Time.time > nextDamage)
            {
                other.gameObject.GetComponent<UIController>().takeDamage(DOTDamage);
                nextDamage = Time.time + damageRate;
            }
    }


}
