using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class secretButtonLogic : MonoBehaviour
{
    public superSecretScript secretScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Secret") != null)
        {
            secretScript = GameObject.FindGameObjectWithTag("Secret").GetComponent<superSecretScript>();
        }
    }


    public void heroEnd()
    {
        secretScript.heroEnd();
    }

    public void evilEnd()
    {
        secretScript.evilEnd(); 
    }

    public void bestBudEnd()
    {
        secretScript.budEnd();
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
