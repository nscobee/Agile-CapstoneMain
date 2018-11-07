using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * handals the basic movement of the phantom
 */

public class PhantomControls : MonoBehaviour
{
    public float speed;
    public GameObject phantomTarget = null;
    public static ReaperCountdown reaper;

    private void Start()
    {
        reaper.outOfBody = true;
    }

    private void Update()
    {
        // uses the generic movement for movement passing desired speed
        transform.position += GenericFunctions.BasePlayerMovement(speed);

        // if the phantom has a target when the player presses space they could call the possession function on that AI
        if (phantomTarget && phantomTarget.tag != "NoPossess")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                phantomTarget.GetComponent<BasicAI>().Possess(this.gameObject);

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
