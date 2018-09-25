using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
  public float movementSpeed;

  public BasicAI aiControls;

  public GameObject phantomPrefab;

  private void Update()
  {
    // calls the generic movement passing the speed
    transform.position += GenericFunctions.BasePlayerMovement(movementSpeed);

    if (Input.GetKeyDown(KeyCode.Backslash))
    {
      FuckingDED();

    }

    if (Input.GetKeyDown(KeyCode.Space))
     {
       WithDraw();
     }

    }

  // used when player dies in a body
  // makes a new phantom to use and destories the charecter the player was previously possessing
  public void FuckingDED()
  {
    Instantiate(phantomPrefab, this.transform.position, new Quaternion());
    Destroy(this.gameObject);

  }

  // TODO: Make function for becoming non Possed but not killing the AI
  public void WithDraw()
  {
        // do something like adding phantom and swaping ai and movement enables
        Instantiate(phantomPrefab, this.transform.position, new Quaternion());
        aiControls.enabled = true;
        this.enabled = false;
    }
}
