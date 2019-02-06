using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public BasicMovement playerControls;

	public float currentHealth;
    public float maxHealth = 100f;
    
    //public dealDamage damageScript;
    public Slider healthSlider;
    public float healthPotionHealAmount;

    float incomingDamage;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
       // healthSlider.value = currentHealth;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "healthPotion")
        {
            print("healing using the thing that hit me: " + other.name);
            currentHealth += healthPotionHealAmount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            Destroy(other.gameObject);
        }

        //if(other.gameObject.tag == "DamageSource")
        //{
        //   damageScript = other.GetComponent<dealDamage>();
        //    takeDamage(damageScript.damage);
        //}
        
    }

    public void takeDamage(float Damage)
    {
        incomingDamage = Damage;
        currentHealth -= incomingDamage;
        if(currentHealth <= 0)
        {
            playerControls.ReallyDED();
        }
    }

}
