using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : UnitDamageMagic
{
  public TestSpell()
  {
    damage = 5;
    attackAreaOfEffect = 5;
    attackRange = 10;
  }

  public override void Attack()
  {
    Debug.Log("Test Spell just attacked");
    Debug.Log("Damage: " + damage);
    Debug.Log("AOE: " + attackAreaOfEffect);
    Debug.Log("Range: " + attackRange);
  }
}
