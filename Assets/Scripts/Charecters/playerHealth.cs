using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public BasicMovement playerControls;

	public float currentHealth;
    public float maxHealth;
    
    public dealDamage damageScript;
    public Slider healthSlider;
    public float healthPotionHealAmount;

    float incomingDamage;
    private void Update()
    {
        healthSlider.value = currentHealth;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "healthPotion")
        {
            currentHealth += healthPotionHealAmount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "DamageSource")
        {
           damageScript = other.GetComponent<dealDamage>();
            takeDamage(damageScript.damage);
        }
        
    }

    public void takeDamage(float Damage)
    {
        incomingDamage = Damage;
        currentHealth -= incomingDamage;
        if(currentHealth <= 0)
        {
            playerControls.FuckingDED();
        }
    }

}
