using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessIcon : MonoBehaviour
{
    public SpriteRenderer PossessionIcon;
    public Sprite noPossess;
    public Sprite yesPossess;

    public BasicAI AI;


    // Start is called before the first frame update
    void Start()
    {
        PossessionIcon.enabled = false;
        AI = this.transform.parent.gameObject.GetComponent<BasicAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AI.canPossess && this.transform.parent.gameObject.tag != "NoPossess")
        {
            PossessionIcon.sprite = yesPossess;
        }
        else if (!AI.canPossess || this.transform.parent.gameObject.tag == "NoPossess")
        { 
            PossessionIcon.sprite = noPossess;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "Possessed")
        {
            PossessionIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Possessed")
        {
            PossessionIcon.enabled = false;
        }
    }

    public void PossessYes()
    {
        PossessionIcon.sprite = yesPossess;
    }

    public void PossessNo()
    {
        PossessionIcon.sprite = noPossess;
    }
}
