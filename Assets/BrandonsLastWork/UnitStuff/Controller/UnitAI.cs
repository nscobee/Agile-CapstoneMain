/*
 * Check and see what this is supposted to do at later time, remove in not needed
 * 
 * using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitPlayer))]
[RequireComponent(typeof(UnitStats))]
public class UnitAI : MonoBehaviour
{
  [Header("General")]
  public AiState currentState;
  public Vector2 homePosition;

  [Header("Idle")]
  public float idleRangeDiameter;
  public float idleTimeInOnePoint;
  public Vector2 idleCurrentPoint;
  public float idleStopMovingRange;
  public bool idleChangingPoints;

  [Header("Patroling")]
  public Transform[] patrolPoints;
  public bool patrolRandomly;
  public Vector2 patrolCurrentPoint;
  public int patrolNextPoint = 0;
  public float patrolStopMovingRange;

  [Header("Chase")]
  public Transform chaseTarget;

  [Header("FleeTarget")]
  public Transform fleeTarget;


  private UnitStats statsScript;

  public enum AiState
  {
    Idle,
    Pattroling,
    Chasing,
    Attacking,
    ReturingHome,
    Flee
  }

  private void Start()
  {
    statsScript = GetComponent<UnitStats>();

    if (homePosition == Vector2.zero)
    {
      homePosition = transform.position;
    }

    if (chaseTarget == null)
    {
      chaseTarget = GameObject.FindGameObjectWithTag("Phantom").transform;
    }

    if (fleeTarget == null)
    {
      fleeTarget = GameObject.FindGameObjectWithTag("Phantom").transform;
    }
  }


  public virtual void Update()
  {
    if (currentState == AiState.Idle)
    {
      Idle();
    }
    if (currentState == AiState.Pattroling)
    {
      Patroling();
    }
    if (currentState == AiState.Chasing)
    {
      Chaseing();
    }
    if (currentState == AiState.Attacking)
    {
      Attacking();
    }
    if (currentState == AiState.ReturingHome)
    {
      ReturningHome();
    }
    if (currentState == AiState.Flee)
    {
      Flee();
    }
  }

  #region Idle
  public virtual void Idle()
  {
    if (!idleChangingPoints)
    {
      if (idleCurrentPoint == Vector2.zero)
      {
        idleCurrentPoint = (UnityEngine.Random.insideUnitCircle * idleRangeDiameter) + homePosition;
      }

      if (Vector2.Distance(this.transform.position, idleCurrentPoint) > idleStopMovingRange)
      {
        transform.position = Vector2.MoveTowards(transform.position, idleCurrentPoint, statsScript.moveSpeed * Time.deltaTime);
      }
      else
      {
        StartCoroutine(IdleWaitToChangePoint());
      }
    }
  }

  private IEnumerator IdleWaitToChangePoint()
  {
    idleChangingPoints = true;
    yield return new WaitForSeconds(idleTimeInOnePoint);
    idleCurrentPoint = Vector2.zero;
    idleChangingPoints = false;
  }

  public virtual void IdleStopMovingOnCollsionEnter(Collision2D collision)
  {
    if (collision.transform.tag == "Wall")
    {
      transform.position = Vector3.MoveTowards(transform.position, idleCurrentPoint, -statsScript.moveSpeed * Time.deltaTime);

      StartCoroutine(IdleWaitToChangePoint());
    }
  }
  #endregion

  #region Patrol
  public virtual void Patroling()
  {
    if (patrolCurrentPoint == Vector2.zero)
    {
      if (patrolRandomly)
      {
        patrolCurrentPoint = patrolPoints[UnityEngine.Random.Range(0, patrolPoints.Length)].position;
      }

      else
      {
        Debug.Log("lastPoint: " + patrolNextPoint);
        Debug.Log("lengeth: " + patrolPoints.Length);

        patrolCurrentPoint = patrolPoints[patrolNextPoint].position;

        if (patrolNextPoint + 1 >= patrolPoints.Length)
        {
          patrolNextPoint = 0;
        }
        else
        {
          patrolNextPoint += 1;
        }
      }
    }

    if (Vector2.Distance(this.transform.position, patrolCurrentPoint) > patrolStopMovingRange)
    {
      transform.position = Vector2.MoveTowards(transform.position, patrolCurrentPoint, statsScript.moveSpeed * Time.deltaTime);
    }
    else
    {
      patrolCurrentPoint = Vector2.zero;
    }
  }

  #endregion

  #region Chase
  public virtual void Chaseing()
  {
    if (chaseTarget != null)
    {
      transform.position = Vector2.MoveTowards(transform.position, chaseTarget.position, statsScript.moveSpeed * Time.deltaTime);
    }
    else
    {
      Debug.LogWarning(transform.name + " is in chase mode but has nothing to chase");
    }
  }
  #endregion

  #region Attack
  public virtual void Attacking()
  {
    throw new NotImplementedException("Add attack to unitAI");
  }
  #endregion

  #region Return Home
  public virtual void ReturningHome()
  {
    if (homePosition != null)
    {
      transform.position = Vector2.MoveTowards(transform.position, homePosition, statsScript.moveSpeed * Time.deltaTime);
    }
    else
    {
      Debug.LogWarning(transform.name + " is trying to return back to default location but has none");
    }
  }
  #endregion

  #region Flee
  public virtual void Flee()
  {
    transform.position = Vector2.MoveTowards(transform.position, fleeTarget.position, statsScript.moveSpeed);
  }
  #endregion

  #region Collisions
  private void OnCollisionEnter2D(Collision2D collision)
  {
    #region Idle
    IdleStopMovingOnCollsionEnter(collision);
    #endregion

  }
  #endregion
}
*/