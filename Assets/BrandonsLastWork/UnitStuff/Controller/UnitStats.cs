/*
 * Check and see what this is supposted to do at later time, remove in not needed
 * 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitAI))]
[RequireComponent(typeof(UnitPlayer))]
public class UnitStats : MonoBehaviour, IPossessable
{
  [Header("Health")]
  public float healthMax;
  public float healthCurrent;

  [Header("Mana")]
  public float manaMax;
  public float manaCurrent;

  [Header("Stamina")]
  public float staminaMax;
  public float staminaCurrent;

  [Header("Attacks")]
  public UnitAttack attackOne;
  public UnitAttack attackTwo;

  [Header("Movement")]
  public float moveSpeed;

  [Header("Damage Resitance?")]
  public float armor = 1;
  public float magicResist = 1;

  [Header("Possession")]
  public bool possessed;
  public Phatom backpackedPhantom;
  public float depossessDamage;
  public GameObject phatomPrefab;

  [Header("Debugging")]
  public bool kill;

  [Space()]
  public bool doDamage;
  public float damage;
  public DamageType doDamageType;

  [Space()]
  public bool doHeal;
  public float heal;

  private UnitAI aiScript;
  private UnitPlayer playerScript;

  private void Update()
  {
    if (kill)
    {
      Kill();
    }
    if (doDamage)
    {
      doDamage = false;
      Damage(damage, doDamageType);
    }
    if (doHeal)
    {
      doHeal = false;
      Heal(heal);
    }
  }

  public enum DepossesType
  {
    Willingly,
    Foreced,
    Death
  }

  public enum DamageType
  {
    Basic,
    Magic,
    Physical
  }

  private void Start()
  {
    OurConsoleLog.AddToLog("Instatiating Unit " + this.transform.name);

    aiScript = GetComponent<UnitAI>();
    playerScript = GetComponent<UnitPlayer>();

    aiScript.enabled = true;
    playerScript.enabled = false;

    healthCurrent = healthMax;
    manaCurrent = manaMax;
    staminaCurrent = staminaMax;

  }

  public void Heal(float heal)
  {
    OurConsoleLog.AddToLog(transform.name + " was headled for " + heal + " health");

    if (healthCurrent + heal <= healthMax)
    {
      healthCurrent += heal;
    }
    else
    {
      healthCurrent = healthMax;
    }
  }

  public void Damage(float damage, DamageType damageType)
  {
    OurConsoleLog.AddToLog(transform.name + " was damaged for " + damage + " damage, damage type " + damageType);

    float attackDamageMultiplier = 1;

    if (damageType == DamageType.Physical)
    {
      attackDamageMultiplier = 1F - (.05F * armor / (1F + 0.05F * armor));
    }
    else if (damageType == DamageType.Magic)
    {
      attackDamageMultiplier = 1F - (0.5F * magicResist / (1F * 0.05F * armor));
    }


    float targetHealth = healthCurrent - (damage * attackDamageMultiplier);
    if (targetHealth > 0)
    {
      healthCurrent -= (damage * attackDamageMultiplier);
    }
    else
    {
      Kill();
    }
  }

  public void Possess(Phatom phantom)
  {
    OurConsoleLog.AddToLog(transform.name + " was possessed");

    aiScript.enabled = false;
    playerScript.enabled = true;

    backpackedPhantom = phantom;
    backpackedPhantom.transform.position = transform.position;
    backpackedPhantom.transform.parent = transform;
    backpackedPhantom.gameObject.SetActive(false);

  }

  public void Depossess(DepossesType exitType)
  {
    OurConsoleLog.AddToLog(transform.name + " was " + exitType + ", depossessed");

    if (backpackedPhantom != null)
    {
      if (exitType == DepossesType.Willingly)
      {
        playerScript.enabled = false;
        aiScript.enabled = true;

        backpackedPhantom.gameObject.SetActive(true);
        backpackedPhantom.transform.parent = null;

        Damage(depossessDamage, DamageType.Basic);
      }
      else if (exitType == DepossesType.Foreced)
      {
        playerScript.enabled = false;
        aiScript.enabled = true;

        backpackedPhantom.gameObject.SetActive(true);
        backpackedPhantom.transform.parent = null;
      }
      else if (exitType == DepossesType.Death)
      {
        backpackedPhantom.gameObject.SetActive(true);
        backpackedPhantom.transform.parent = null;

        Kill();
      }
    }
    else
    {
      OurConsoleLog.AddToLog(transform.name + " was depossessed but had no backpaced phantom");
      Instantiate(phatomPrefab, transform.position, phatomPrefab.transform.rotation);

    }
  }

  public void Kill()
  {
    OurConsoleLog.AddToLog(transform.name + " was killed");
    Destroy(this.gameObject);
  }
}

public interface IPossessable
{
  void Possess(Phatom phatom);

}
*/