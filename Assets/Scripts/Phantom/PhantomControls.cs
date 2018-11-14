using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * handals the basic movement of the phantom
 */

public class PhantomControls : MonoBehaviour
{
    public bool isPossessing;

  public float speed;
  public GameObject phantomTarget = null;
    public GameObject phantom;
  public static ReaperCountdown reaper;

  private void Start()
  {
        reaper = phantom.GetComponent<ReaperCountdown>();
    

  }

  private void Update()
  {
    // uses the generic movement for movement passing desired speed
    transform.position += GenericFunctions.BasePlayerMovement(speed);

    // if the phantom has a target when the player presses space they could call the possession function on that AI
    if (phantomTarget)
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        phantomTarget.GetComponent<BasicAI>().Possess(this.gameObject);
                reaper.outOfBody = false;
                isPossessing = true;

      }
    }
  }

  private void OnDestroy()
  {
   
  }
}
