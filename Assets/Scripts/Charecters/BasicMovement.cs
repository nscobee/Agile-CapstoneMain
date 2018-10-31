using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
  public float movementSpeed;

  public BasicAI aiControls;

  public GameObject phantomPrefab;

    public GameObject phantom; //obtain info on phantom for possession
    public BoxCollider phantomBox; //reinable when depossessing
    public MeshRenderer phantomMesh; //^^same
    public static ReaperCountdown reaper;

    private void Start()
    {
        phantom = GameObject.FindWithTag("Player"); 
        phantomBox = phantom.GetComponent<BoxCollider>();
        phantomMesh = phantom.GetComponent<MeshRenderer>();
        reaper = phantom.GetComponent<ReaperCountdown>();
    }

    private void Update()
  {
    // calls the generic movement passing the speed
    transform.position += GenericFunctions.BasePlayerMovement(movementSpeed);

    if (Input.GetKeyDown(KeyCode.Backslash))
    {
      FuckingDED();
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

    }

  // used when player dies in a body
  // makes a new phantom to use and destories the charecter the player was previously possessing
  public void FuckingDED()
  {

        phantomBox.enabled = true; //re-enable phantom
        phantomMesh.enabled = true; //^^same
        Destroy(this.gameObject); //kill off the dead Ai

  }

  // TODO: Make function for becoming non Possed but not killing the AI
  public void WithDraw()
  {
        // do something like adding phantom and swaping ai and movement enables
        
        phantomBox.enabled = true; //re-enable phantom
        phantomMesh.enabled = true; //^^^what he said

        aiControls.enabled = true;
        this.enabled = false;
    }
}
