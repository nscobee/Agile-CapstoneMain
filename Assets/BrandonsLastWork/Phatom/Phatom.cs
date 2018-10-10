using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phatom : MonoBehaviour
{
  public static GameObject currentPlayer;
  public float moveSpeed;

  private CircleCollider2D possessionCollider;

  public List<GameObject> possessableEntitiesGameObj = new List<GameObject>();

  private void Start()
  {
    possessionCollider = GetComponentInChildren<CircleCollider2D>();
  }

  private void Update()
  {
    transform.position += WASDMovement();

    CheckToPossess();
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

    return moveDirection.normalized * moveSpeed * Time.deltaTime;
  }

  public void CheckToPossess()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (possessableEntitiesGameObj.Count > 0)
      {
        GameObject closest = possessableEntitiesGameObj[0];


        foreach (GameObject possessable in possessableEntitiesGameObj)
        {
          if (Vector3.Distance(transform.position, possessable.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
          {
            closest = possessable;
          }
        }

        possessableEntitiesGameObj = new List<GameObject>();

        currentPlayer = closest;
        closest.GetComponent<IPossessable>().Possess(this);
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    IPossessable possessableComponent = collision.gameObject.GetComponent<IPossessable>();

    if (possessableComponent != null)
    {
      possessableEntitiesGameObj.Add(collision.gameObject);

    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    IPossessable possessableComponent = collision.gameObject.GetComponent<IPossessable>();

    if (possessableComponent != null)
    {
      possessableEntitiesGameObj.Remove(collision.gameObject);

    }
  }
}
