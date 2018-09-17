using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The goal of this is to set target of possession for the phantom
 * 
 * returns a Gameobject
 */
public class PhantomRange : MonoBehaviour
{

  public List<GameObject> inRange = new List<GameObject>();

  public GameObject primaryTarget;

  public PhantomControls controller;

  // on start set up link between this is and phantom controlls
  private void Start()
  {
    controller = gameObject.GetComponentInParent<PhantomControls>();

  }

  private void Update()
  {
    // if there are more than 1 potential targets in range
    if (inRange.Count > 1)
    {
      GameObject closest = null;

      // finds closest potential targets
      foreach (GameObject target in inRange)
      {
        if (closest == null)
        {
          closest = target;

        }

        else
        {
          if (Vector3.Distance(this.transform.position, closest.transform.position) >
            Vector3.Distance(this.transform.position, target.transform.position))
          {
            closest = target;

          }

        }

      }

      // sets the target to the closest one
      primaryTarget = closest;

    }

    // if there is just one potential targets it makes that the primary
    else if (inRange.Count == 1)
    {
      primaryTarget = inRange[0];

    }

    // no targets in range no primary targets
    else
    {
      primaryTarget = null;
    }

    // sets the target in here to the primary one
    controller.phantomTarget = primaryTarget;



  }


  private void OnTriggerEnter(Collider other)
  {
    // checks if the triggerd object is in the right layer if it is it adds it to potential list
    if (other.gameObject.layer == LayerMask.NameToLayer("AI"))
    {
      inRange.Add(other.gameObject);

    }
  }

  private void OnTriggerExit(Collider other)
  {
    // checks if its in the right layer then if it is it removes it from potential list
    if (other.gameObject.layer == LayerMask.NameToLayer("AI"))
    {
      inRange.Remove(other.gameObject);

    }
  }
}