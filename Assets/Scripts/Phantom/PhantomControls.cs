using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * handals the basic movement of the phantom
 */

public class PhantomControls : MonoBehaviour
{
    public GameObject healthAndAbilities;
    public bool isShowing;
    public float speed;
    public GameObject phantomTarget = null;
    public static ReaperCountdown reaper;
    public bool isPossessing;
    public GameObject phantom;



    private void Start()
    {
        reaper = phantom.GetComponent<ReaperCountdown>();
        reaper.outOfBody = true;

    }

    private void Update()
    {
        // uses the generic movement for movement passing desired speed
        transform.position += GenericFunctions.BasePlayerMovement(speed);

        // if the phantom has a target when the player presses space they could call the possession function on that AI
        if (phantomTarget && phantomTarget.tag != "NoPossess" && phantomTarget.tag != "Reaper")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isShowing = !isShowing;
                healthAndAbilities.SetActive(isShowing);
                isPossessing = true;
                reaper.outOfBody = false;
                phantomTarget.GetComponent<BasicAI>().Possess(this.gameObject);
                if(phantomTarget.tag == "mage")
                {
                    phantomTarget.GetComponent<BasicAI>().isPosessingMage = true;
                }

                //if the game object being posessed is a scribe, save the game
                if (phantomTarget.tag == "Scribe")
                {
                    //make a new save file directory
                    SaveLoadSystem.MakeNewPlayerSave("ScribeTests");

                    //make a new save in the folder for that scene
                    SaveData newSaveTest = new SaveData("Scene01", 1);

                    //save the stuff
                    SaveLoadSystem.SavePlayer(newSaveTest, "ScribeTests");
                }
            }
        }
    }


    private void OnDestroy()
    {
        reaper.outOfBody = false;
    }

}
