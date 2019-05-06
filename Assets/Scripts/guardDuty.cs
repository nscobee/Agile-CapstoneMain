using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardDuty : MonoBehaviour
{
    public GameObject invisibleWall;
    public GameObject[] speechBubble;

    public string messageForGuildedKnight = "You may pass.. Sir Knight";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Destroy(invisibleWall);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<BasicAI>().tagOverride == "guilded")
        {
            for(int i = 0; i < speechBubble.Length; i++)
            {
                speechBubble[i].GetComponent<popUpText>().message = messageForGuildedKnight;
            }
            Destroy(this.gameObject, 5f);
        }
    }
}
