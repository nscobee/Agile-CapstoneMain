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

    private AudioSource source;
    public AudioClip floating;
    public float floatVolume = 1;

    public bool playerHasDied = false;


    //Simple Leveling System
    //public int currentLevel = 1;
    // public float currentExperience = 0;
    //public float experienceTillNextLevel;
    // private float startingExperienceTillNextLevel;
    //public int MAX_LEVEL = 5;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        reaper = phantom.GetComponent<ReaperCountdown>();
        slControl = FindObjectOfType<SaveLoadController>();
        reaper.outOfBody = true;
        //startingExperienceTillNextLevel = experienceTillNextLevel;
    }

    private void Update()
    {
        if (playerHasDied)
            Die();
        //DontDestroyOnLoad(this.gameObject);

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
                if (phantomTarget.name == "Scribe"/* && isPossessing*/)
                {
                    Debug.Log("POSSESSING SCRIBE; SAVING");
                    slControl.SaveLevel();
                    phantomTarget.GetComponent<BasicAI>().Possess(this.gameObject);
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

        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ) && !isPossessing) //if moving and in spirit form, play float sounds
        {
            if(!source.isPlaying)
            source.PlayOneShot(floating, floatVolume); 
        }
        if(isPossessing)
        {
            source.Stop();
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Reaper")
            Die();
    }

}
