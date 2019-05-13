using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    public GameObject player;
    public GameObject target;
    private AudioSource source;

    // Use this for initialization
    private void Awake()
    {
        source = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
    }

	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);

        source = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }
}
