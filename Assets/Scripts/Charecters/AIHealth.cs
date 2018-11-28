using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public const float MAX_HEALTH = 100;
    public float currentHealth;

   // public dealDamage damageScript;

    private float incomingDamage;

    // Use this for initialization
    void Start()
    {
        currentHealth = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        incomingDamage = amount;
        currentHealth -= incomingDamage;
        if (currentHealth <= 0)
        {
            this.GetComponent<BasicMovement>().WithDraw();
            Destroy(this.gameObject);
        }
    }
}
