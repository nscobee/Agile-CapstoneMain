using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpText : MonoBehaviour
{
    public Text popUpMessageText;
    public GameObject popUpMessagePanel;
    public string regularMessage;
    public string phantomSpecificMessage;
    [Tooltip("The exact name of the prefab you want to trigger the speech bubble")]
    public string triggerPrefabString;

    public bool destroyOnLeave = false;
    private bool hasTriggered = false;
    public bool triggerOnAnyNPC = false;

    PhantomControls phantom;

    // Start is called before the first frame update
    void Start()
    {
        phantom = GameObject.Find("Phantom2.0").GetComponent<PhantomControls>();
        popUpMessagePanel.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject == GameObject.Find(triggerPrefabString) || triggerOnAnyNPC)
        {
            popUpMessagePanel.SetActive(true);
            //if the phantom isnt possessing, display phantom message
            //else display character's reaction to another
            if (!phantom.isPossessing)
                popUpMessageText.text = phantomSpecificMessage;
            else
                popUpMessageText.text = regularMessage;
            hasTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!destroyOnLeave)
        popUpMessagePanel.SetActive(false);
        else if(hasTriggered)
        Destroy(this.gameObject);
    }
}
