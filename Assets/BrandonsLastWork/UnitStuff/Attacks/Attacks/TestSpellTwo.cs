using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpellTwo : UnitDamageMagic {

  public TestSpellTwo()
  {
    damage = 20;
    attackAreaOfEffect = 25;
    attackRange = 100;
  }

  public override void Attack()
  {
    Debug.Log("Test Spell Two just attacked");
    Debug.Log("Damage: " + damage);
    Debug.Log("AOE: " + attackAreaOfEffect);
    Debug.Log("Range: " + attackRange);
  }
}
