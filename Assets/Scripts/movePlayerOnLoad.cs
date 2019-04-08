using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayerOnLoad : MonoBehaviour
{
    public GameObject phantom;
    public GameObject parent;
    int buildIndex;
    //SaveLoadController slControl;

    void Start()
    {
        Debug.Log("the player move script is being called from: " + this.gameObject.name);
        phantom = GameObject.Find("Phantom2.0");
        //slControl = GameObject.Find("SaveLoadControllerObj").GetComponent<SaveLoadController>();
        if (phantom.transform.parent == null)
        {
            Debug.Log("the player move script is being called from: " + this.gameObject.name);
            phantom.transform.position = this.gameObject.transform.position;
            SceneManager.MoveGameObjectToScene(phantom.gameObject, SceneManager.GetActiveScene());
            //phantom.transform.position = slControl.SetPlayerPos(SaveAndLoad.savedGames[0]);
        }
        else
        {
            Debug.Log("the player move script is being called from: " + this.gameObject.name);
            parent = phantom.transform.parent.gameObject;
            parent.transform.position = this.gameObject.transform.position;
            SceneManager.MoveGameObjectToScene(parent.gameObject, SceneManager.GetActiveScene());
            //phantom.transform.position = slControl.SetPlayerPos(SaveAndLoad.savedGames[0]);
        }
    }

}
