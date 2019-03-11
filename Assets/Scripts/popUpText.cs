using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpText : MonoBehaviour
{
    public Text popUpMessageText;
    public GameObject popUpMessagePanel;
    public string message;
    [Tooltip("The exact name of the prefab you want to trigger the speech bubble")]
    public string triggerPrefabString;

    public bool destroyOnLeave = false;
    private bool hasTriggered = false;
    public bool triggerOnAnyNPC = false;
    
    // Start is called before the first frame update
    void Start()
    {
        popUpMessagePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.Find(triggerPrefabString) || triggerOnAnyNPC)
        {
            popUpMessagePanel.SetActive(true);
            popUpMessageText.text = message;
            hasTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!destroyOnLeave)
            popUpMessagePanel.SetActive(false);
        else if (hasTriggered)
            Destroy(this.gameObject);
    }
}
