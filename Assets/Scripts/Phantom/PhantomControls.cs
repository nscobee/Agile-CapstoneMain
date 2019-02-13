using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * handals the basic movement of the phantom
 */

public class PhantomControls : MonoBehaviour
{
    public bool isShowing;
    public float speed;
    public GameObject phantomTarget = null;
    public static ReaperCountdown reaper;
    public bool isPossessing;
    public GameObject phantom;

    //Simple Leveling System
    public int currentLevel = 1;
    public float currentExperience = 0;
    public float experienceTillNextLevel;
    private float startingExperienceTillNextLevel;
    public int MAX_LEVEL = 5;



    private void Start()
    {

        reaper = phantom.GetComponent<ReaperCountdown>();
        reaper.outOfBody = true;
        startingExperienceTillNextLevel = experienceTillNextLevel;


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
                this.gameObject.tag = "Player";
                isPossessing = true;

                if (isPossessing)
                {
                    speed = 0f;
                }

                reaper.outOfBody = false;
                phantomTarget.GetComponent<BasicAI>().Possess(this.gameObject);


                //if the game object being posessed is a scribe, save the game
                if (phantomTarget.tag == "Scribe" && isPossessing)
                {
                    //make a new save file directory
                    SaveLoadSystem.MakeNewPlayerSave("ScribeTests");

                    //make a new save in the folder for that scene
                    SaveData newSaveTest = new SaveData("Scene01", 1);

                    //save the stuff
                    SaveLoadSystem.SavePlayer(newSaveTest, "ScribeTests");
                }
            }
            if (!isPossessing)
            {
                speed = 5f;
            }

        }
        else
        {
            isPossessing = false;
        }

        //Simple Leveling System
        if (currentLevel == MAX_LEVEL) currentExperience = 0;
        if (currentExperience >= experienceTillNextLevel)
        {
            currentLevel++;
            currentExperience = 0;
            experienceTillNextLevel *= currentLevel;
        }

    }


    private void OnDestroy()
    {
        reaper.outOfBody = false;
    }

    public void resetLevel()
    {
        currentLevel = 0;
        currentExperience = 0;
        experienceTillNextLevel = startingExperienceTillNextLevel;
    }

}
