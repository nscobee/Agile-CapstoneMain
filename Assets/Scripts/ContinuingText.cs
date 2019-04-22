using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuingText : MonoBehaviour
{
    public Text popupText;
    public GameObject popupTextPanel;

    public string[] messages;

    [Tooltip("The exact name of the prefab you want to trigger the speech bubble")]
    public string triggerPrefabString;

    public bool destroyOnLeave = false;
    private bool hasTriggered = false;
    public bool triggerOnAnyNPC = false;

    public float timeStart = 0;

    void Start()
    {
        popupTextPanel.SetActive(false);
    }
    
    void Update()
    {
        if (this.transform.parent.gameObject.tag == "Possessed")
            Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameObject.Find(triggerPrefabString) ||
            (triggerOnAnyNPC && other.gameObject.GetComponent<BasicAI>()))
        {
            popupTextPanel.SetActive(true);
            int messageNum = 0;

            timeStart += Time.deltaTime;

            while (messageNum < messages.Length && timeStart >= 3f)
            {
                timeStart = 0;
                popupText.text = messages[messageNum];
                //messages[messageNum].Remove(messageNum);
                messageNum++;
            }
            hasTriggered = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!destroyOnLeave)
            popupTextPanel.SetActive(false);
        else if (hasTriggered)
            Destroy(this.gameObject);
    }

}
