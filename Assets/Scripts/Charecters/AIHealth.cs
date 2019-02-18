using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public const float MAX_HEALTH = 100;
    public float currentHealth;

    public float currentMana;
    
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

    public void LoseMana(float amount)
    {
        currentMana -= amount;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Ai taking damage");
        //incomingDamage = amount;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            this.GetComponent<BasicMovement>().WithDraw();
            Destroy(this.gameObject);
        }
    }
}
