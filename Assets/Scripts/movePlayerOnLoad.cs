using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayerOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject phantom;
    public GameObject parent;
    int buildIndex;

    void Start()
    {
        phantom = GameObject.Find("Phantom2.0");
        if (phantom.transform.parent == null)
        {
            phantom.transform.position = this.gameObject.transform.position;
            SceneManager.MoveGameObjectToScene(phantom.gameObject, SceneManager.GetActiveScene());
        }
        else
        {
            parent = phantom.transform.parent.gameObject;
            parent.transform.position = this.gameObject.transform.position;
            SceneManager.MoveGameObjectToScene(parent.gameObject, SceneManager.GetActiveScene());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
