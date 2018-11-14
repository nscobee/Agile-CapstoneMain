using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : MonoBehaviour
{
  private UnitStats statsScript;

  private void Start()
  {
    statsScript = GetComponent<UnitStats>();
  }

  private void Update()
  {
    transform.position += WASDMovement();

    Attacks();

    DePossess();
  }

  public Vector3 WASDMovement()
  {
    Vector2 moveDirection = Vector2.zero;

    if (Input.GetKey(KeyCode.W))
    {
      moveDirection += Vector2.up;
    }
    if (Input.GetKey(KeyCode.S))
    {
      moveDirection += Vector2.down;
    }
    if (Input.GetKey(KeyCode.A))
    {
      moveDirection += Vector2.left;
    }
    if (Input.GetKey(KeyCode.D))
    {
      moveDirection += Vector2.right;
    }

    return moveDirection.normalized * statsScript.moveSpeed * Time.deltaTime;
  }

  public void Attacks()
  {
    if (Input.GetMouseButtonDown(0))
    {
      statsScript.attackOne.Attack();
    }
    if (Input.GetMouseButtonDown(1))
    {
      statsScript.attackTwo.Attack();
    }
  }

  public void DePossess()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      statsScript.Depossess(UnitStats.DepossesType.Willingly);
    }
  }
}