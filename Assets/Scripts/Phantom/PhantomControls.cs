using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * handals the basic movement of the phantom
 */

public class PhantomControls : MonoBehaviour
{
    public float speed;
    public GameObject phantomTarget = null;
    public static ReaperCountdown reaper;
    public GameObject healthAndAbilities;
    public bool isShowing;
  


    private void Start()
    {
        reaper.outOfBody = true;

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
                isShowing = !isShowing;
                healthAndAbilities.SetActive(isShowing);
                


            }
        }
    }

    private void OnDestroy()
    {
        reaper.outOfBody = false;
    }
}
