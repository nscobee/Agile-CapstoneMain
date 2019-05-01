using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    SaveLoadController slControl;
    

    //Simple Leveling System
    //public int currentLevel = 1;
   // public float currentExperience = 0;
    //public float experienceTillNextLevel;
   // private float startingExperienceTillNextLevel;
    //public int MAX_LEVEL = 5;

    private void Start()
    {
        reaper = phantom.GetComponent<ReaperCountdown>();
        slControl = FindObjectOfType<SaveLoadController>();
        reaper.outOfBody = true;
        //startingExperienceTillNextLevel = experienceTillNextLevel;
    }

    private void Update()
    {
        
        DontDestroyOnLoad(this.gameObject);

        // uses the generic movement for movement passing desired speed
        transform.position += GenericFunctions.BasePlayerMovement(speed);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

        // if the phantom has a target when the player presses space they could call the possession function on that AI
        if (phantomTarget && phantomTarget.tag != "NoPossess" && phantomTarget.tag != "Reaper" && phantomTarget.GetComponent<BasicAI>().canPossess)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isPossessing)
            {
                this.gameObject.tag = "Player";
                //isPossessing = true;

                //if the game object being posessed is a scribe, save the game
                if (phantomTarget.tag == "Scribe" && isPossessing)
                {
                    Debug.Log("POSSESSING SCRIBE; SAVING");
                    SaveLoadController.control.SaveLevel();
                }

                if (isPossessing)
                {
                    speed = 0f;
                }

                reaper.outOfBody = false;
                phantomTarget.GetComponent<BasicAI>().Possess(this.gameObject);
            }
            if (!isPossessing)
            {
                speed = 5f;
            }
        }
    }

    public void Die()
    {
        slControl.SwitchScene(7);
        reaper.timeTillDespawn = 0f;
    }
    private void OnDestroy()
    {
        reaper.outOfBody = false;
    }
}
