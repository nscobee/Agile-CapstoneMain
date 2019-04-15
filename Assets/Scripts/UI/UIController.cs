using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PhantomControls phantomController;
    public GameObject uiObj;
    Canvas theUI;
    public Slider manaSlider;
    public Slider healthSlider;
    public float currentHealth = 100;
    public float currentMana = 100;
    public float MAXHP = 100;
    public float MAXMANA = 100;
    [Tooltip("Enter a number between 1 and 100 for percentage")]
    public float percentOfHealthToPossess = 20;

    public float rateOfManaRegen = 0.5f;
    public GameObject healthDrop;

    public BasicAI AI;

    private void Start()
    {
        theUI = uiObj.GetComponent<Canvas>();
        theUI.enabled = false;
        phantomController = GameObject.Find("Phantom2.0").GetComponent<PhantomControls>();
        AI = gameObject.GetComponent<BasicAI>();

        manaSlider.maxValue = MAXMANA;
        healthSlider.maxValue = MAXHP;
    }

    // Update is called once per frame
    void Update()
    {

        healthSlider.value = currentHealth;
        manaSlider.value = currentMana;


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

        if(this.gameObject.tag != "Possessed")
        {
            if(AI.possessOnLowHealth)
            {
                float healthThreshold = MAXHP * (percentOfHealthToPossess / 100f);
                if(currentHealth <= healthThreshold)
                {
                    AI.canPossess = true;
                }
            }
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
        currentHealth -= damage;
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

    public void Die()
    {
        if (this.gameObject.tag != "Possessed")
        {
            GameObject healthpickup = Instantiate(healthDrop, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.GetComponent<BasicMovement>().DED();
        }
    }
}
