using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PhantomControls phantomControls;
    public GameObject uiObj;
    Canvas theUI;

    private void Start()
    {
        theUI = uiObj.GetComponent<Canvas>();
        theUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (phantomControls.isPossessing && this.gameObject.GetComponent<BasicAI>().playerInRangeR)
        {
            theUI.enabled = true;
        }
        else
        {
            uiObj.SetActive(true);
            theUI.enabled = false;
        }
    }
}
