using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float movementSpeed;
    public GameObject healthAndAbilities;

    public BasicAI aiControls;
    public PhantomControls phantomControls;

    public GameObject phantomPrefab;

    public GameObject phantom; //obtain info on phantom for possession
    public BoxCollider2D phantomBox; //reinable when depossessing
    public MeshRenderer phantomMesh; //^^same
    public static ReaperCountdown reaper;


    private void Start()
    {
        phantom = GameObject.FindWithTag("Player");
        phantomBox = phantom.GetComponent<BoxCollider2D>();
        phantomMesh = phantom.GetComponent<MeshRenderer>();
        reaper = phantom.GetComponent<ReaperCountdown>();
        phantomControls = phantom.GetComponent<PhantomControls>();
        if(phantomControls == null)
        {
            print("phantom controls not found");
        }

    }

    private void Update()
    {
        // calls the generic movement passing the speed
        transform.position += GenericFunctions.BasePlayerMovement(movementSpeed);

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            DED();

            reaper.outOfBody = true;
            phantom.GetComponent<PhantomControls>().isPossessing = false;
            phantom.GetComponent<ReaperCountdown>().despawnTime = 0;

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            WithDraw();

            reaper.outOfBody = true;
            phantom.GetComponent<PhantomControls>().isPossessing = false;
            phantom.GetComponent<ReaperCountdown>().despawnTime = 0;
        }

        //use primary attack for mage
        if (this.GetComponent<BasicAI>().isPosessingMage)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("basic movement script is firing fireball");
                this.GetComponent<MageAI>().FireballAttack();
            }
            if (Input.GetMouseButton(1))
            {
                print("basic movement script is using fire attack");
                this.GetComponent<MageAI>().FireAttack();
            }
        }

    }

    // used when player dies in a body
    // makes a new phantom to use and destories the charecter the player was previously possessing
    public void DED()
    {

        phantomBox.enabled = true; //re-enable phantom
        phantomMesh.enabled = true; //^^same
        Destroy(this.gameObject); //kill off the dead Ai

    }

    public void ReallyDED()
    {
        phantomBox.enabled = false;
        phantomMesh.enabled = false;
        this.enabled = false;
    }

    // TODO: Make function for becoming non Possed but not killing the AI
    public void WithDraw()
    {
        // do something like adding phantom and swaping ai and movement enables
        //aiControls.isPosessingMage = false;
        //aiControls.isPosessingFighter = false;

        phantomControls.isPossessing = false;

        phantomBox.enabled = true; //re-enable phantom
        phantomMesh.enabled = true; //^^^what he said
        
        aiControls.enabled = true;
        this.enabled = false;
    }

}
