using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float movementSpeed;
    //public GameObject healthAndAbilities;

    public BasicAI aiControls;
    public PhantomControls phantomControls;
    public Camera mainCamera;

    public GameObject phantomPrefab;
    


    public GameObject phantom; //obtain info on phantom for possession
    public BoxCollider2D phantomBox; //reinable when depossessing
    public SpriteRenderer phantomMesh; //^^same
    public Rigidbody2D phantomRigid;
    public static ReaperCountdown reaper;

    public bool isLookingRight = true;

    private AudioSource source;
    public AudioClip depossessSound;
    public AudioClip walking;
    public float walkingVolume;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {

        mainCamera = Camera.main;
        phantom = GameObject.Find("Phantom2.0");
        phantomBox = phantom.GetComponent<BoxCollider2D>();
        phantomMesh = phantom.GetComponent<SpriteRenderer>();
        phantomRigid = phantom.GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.Backslash) && phantomControls.isPossessing)
        {
            DED(); 
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && phantomControls.isPossessing)
        {
            WithDraw();

            reaper.outOfBody = true;
            phantomControls.enabled = true;
            phantomControls.isPossessing = false;
            phantom.GetComponent<ReaperCountdown>().despawnTime = 0;
            //mainCamera.transform.parent = phantom.gameObject.transform;
        }

        if(phantomControls.isPossessing)
        {
            phantom.transform.position = phantom.transform.parent.transform.position;
        }

        if (Input.GetKey(KeyCode.A))
        {
            isLookingRight = false;
            if(!source.isPlaying)
                source.PlayOneShot(walking, walkingVolume);

        }

        if (Input.GetKey(KeyCode.D))
        {
            isLookingRight = true;
            if (!source.isPlaying)
                source.PlayOneShot(walking, walkingVolume);

        }

        if (Input.GetKey(KeyCode.W))
        {
            if (!source.isPlaying)
                source.PlayOneShot(walking, walkingVolume);

        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!source.isPlaying)
                source.PlayOneShot(walking, walkingVolume);

        }

        if (isLookingRight)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            //mainCamera.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
           // mainCamera.transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }


  // used when player dies in a body
  // makes a new phantom to use and destories the charecter the player was previously possessing
  public void DED()
  {

        WithDraw();

      //  phantomControls.resetLevel();
        reaper.outOfBody = true;
        phantomControls.enabled = true;
        phantomControls.isPossessing = false;
        phantom.GetComponent<ReaperCountdown>().despawnTime = 0;
       // mainCamera.transform.parent = phantom.gameObject.transform;
        Destroy(this.gameObject);
        phantomControls.speed = 5;
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
        source.PlayOneShot(depossessSound);

        
        phantomControls.isPossessing = false;

        phantomBox.enabled = true; //re-enable phantom
        phantomMesh.enabled = true; //^^^what he said
        phantomRigid.WakeUp();
        aiControls.resetTag();

        phantom.transform.parent = null;
        

        aiControls.enabled = true;
        this.enabled = false;
        
    }

    

}
