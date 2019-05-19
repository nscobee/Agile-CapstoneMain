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
            StartCoroutine(fadeOut(this.gameObject.GetComponent<SpriteRenderer>(), 2f));
            //Destroy(this.gameObject, 5f);
           // this.gameObject.GetComponent<UIController>().Die();
        }
    }

    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration) //death animation, prevents player from moving while dying as well
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            //Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
