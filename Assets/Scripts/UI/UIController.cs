﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public PhantomControls phantomController;
    public GameObject uiObj;
    private necromancerAI necroAI;
    Canvas theUI;
    public Slider manaSlider;
    public Slider healthSlider;
    public Slider primaryCooldown;
    public Slider secondaryCooldown;
    public Slider AIHealthSlider;
    public float currentHealth = 100;
    public float currentMana = 100;
    public float MAXHP = 100;
    public float MAXMANA = 100;
    [Tooltip("Enter a number between 1 and 100 for percentage")]
    public float percentOfHealthToPossess = 20;

    public float rateOfManaRegen = 0.5f;
    public GameObject healthDrop;
    [Tooltip("Enter a number between 0 and 100 for percentage")]
    public float dropChance = 20f;

    public float primaryFireRate = 2f;
    public float nextPrimaryFire = 0;
    public float secondaryFireRate = 5f;
    public float nextSecondaryFire = 0;

    public float maxXPGained = 50;

    public bool inRange = false;

    public BasicAI AI;

    private void Start()
    {
        phantomController = GameObject.Find("Phantom2.0").GetComponent<PhantomControls>();

        if (this.gameObject.tag == "Necromancer") necroAI = this.gameObject.GetComponent<necromancerAI>();
        if (this.gameObject.tag == "Necromancer") MAXHP = necroAI.MAXHEALTH;        
        if (this.gameObject.tag == "Necromancer") uiObj.SetActive(false);

        if (this.gameObject.tag != "Necromancer") theUI = uiObj.GetComponent<Canvas>();
        if (this.gameObject.tag != "Necromancer") theUI.enabled = false;        
        if(this.gameObject.tag != "Necromancer") AI = gameObject.GetComponent<BasicAI>();
        if (this.gameObject.tag != "Necromancer") manaSlider.maxValue = MAXMANA;        
        if (this.gameObject.tag != "Necromancer") AIHealthSlider.maxValue = MAXHP;
        if (this.gameObject.tag != "Necromancer") primaryCooldown.maxValue = primaryFireRate;
        if (this.gameObject.tag != "Necromancer") secondaryCooldown.maxValue = secondaryFireRate;
        if (this.gameObject.tag != "Necromancer") AIHealthSlider.gameObject.SetActive(false);

        healthSlider.maxValue = MAXHP;
        currentHealth = MAXHP;


    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag != "Necromancer") //stuff for generic AI's
        {
            healthSlider.value = currentHealth;
            manaSlider.value = currentMana;
            AIHealthSlider.value = currentHealth;
            primaryCooldown.value = Time.time - nextPrimaryFire;
            secondaryCooldown.value = Time.time - nextSecondaryFire;


            if (this.gameObject.tag == "Possessed")
            {
                print("UI IN!");
                theUI.enabled = true;
            }
            else
            {
                print("UI OUT!");
                uiObj.SetActive(true);
                theUI.enabled = false;
            }

            if (currentHealth <= 0)
                Die();
            if (currentHealth >= MAXHP) currentHealth = MAXHP;
            if (currentMana >= MAXMANA) currentMana = MAXMANA;

            if (currentMana <= 0) currentMana = 0;

            currentMana += rateOfManaRegen * Time.deltaTime;

            if (this.gameObject.tag != "Possessed")
            {
                if (AI.possessOnLowHealth)
                {
                    float healthThreshold = MAXHP * (percentOfHealthToPossess / 100f);
                    if (currentHealth <= healthThreshold)
                    {
                        AI.canPossess = true;
                    }
                }
            }

            if (this.gameObject.tag != "Possessed" && (inRange || AI.isRetaliating))
            {
                AIHealthSlider.gameObject.SetActive(true);
            }
            else
                AIHealthSlider.gameObject.SetActive(false);
        }
        else //stuff for necromancer
        {
            healthSlider.value = currentHealth;
        }
                   


    }

    public void resetHealth()
    {
        currentHealth = MAXHP;
    }

    public void resetMana()
    {
        currentMana = MAXMANA;
    }

    public void takeDamage(float damage)
    {
        print("I am " + this.gameObject.name + " and I am taking " + damage + " damage");
        currentHealth -= damage;
        if (this.gameObject.tag != "Possessed")
            AI.isRetaliating = true;
    }

    public void useMana(float value)
    {
        currentMana -= value;
    }

    public void increaseHealth(float value)
    {
        currentHealth += value;
    }

    public void increaseMana(float value)
    {
        currentMana += value;
    }

    public void increaseStats()
    {
        MAXHP += 10;
        MAXMANA += 10;
        rateOfManaRegen++;
        primaryFireRate *= .95f;
        secondaryFireRate *= .95f;
    }


    public void Die()
    {
        if (this.gameObject.tag != "Possessed" && this.gameObject.tag != "Necromancer")
        {
            if (Random.Range(0, 100) < dropChance)
            {
                GameObject healthpickup = Instantiate(healthDrop, this.transform.position, Quaternion.identity);
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<levelingScript>().gainXP(Random.Range(1, maxXPGained));
            GameObject.FindGameObjectWithTag("Player").GetComponent<levelingScript>().removeID(AI.NPC_ID);
            Destroy(this.gameObject);
        }
        else if(this.gameObject.tag == "Possessed")

        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<levelingScript>().removeID(AI.NPC_ID);
            this.gameObject.GetComponent<BasicMovement>().DED();            
        }
        else
        {
            //necromancer die
        }
    }
}
