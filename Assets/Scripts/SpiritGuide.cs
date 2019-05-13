using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpiritGuide : MonoBehaviour
{
    public List<Transform> pointsList = new List<Transform>();
    public float dialogueWait = 7f;
    public float speed = 1.5f;
    private int count = 0;
    private bool playerIsInRange = false;

    [Header("Text Stuffs")]
    public GameObject TextPanel;
    public Text speechText;

    [Tooltip("Is the guide talking to us?")]
    public bool isTalking = false;
    public bool isDoneTalking = false;

    [Tooltip("How long it takes for the text string to 'type' itself in seconds")]
    public float timeLapse = 2;
    private bool typing = false;

    [Tooltip("Text that the guide says in his speech bubbles, set to whatever you want his first words to be:")]
    public string text = "";
    private float nextRead;

    [Tooltip("how long (in seconds) we are forcing the player to read the dialogue")]
    public float checkForNextRead = 4;

    bool readPart1 = false;
    bool readPart2 = false;
    bool readPart3 = false;
    bool readPart4 = false;



    // Update is called once per frame
    void Update()
    {
        if (playerIsInRange)
        {
            //if text isn't done cycling, and the monologue hasn't started, start monologuing
            if (!isDoneTalking)
            {
                Talking();
            }

            if (!readPart1)
            {
                readPart1 = true;
                nextRead = Time.time + checkForNextRead;
                text = "If we leave the graveyard the reaper will get us.";
                typing = false;
            }
            if (!readPart2 && readPart1 && Time.time > nextRead)
            {
                readPart2 = true;
                nextRead = Time.time + checkForNextRead;
                text = "But if we both leave at the same time we might have a fighting chance!";
                typing = false;
            }
            if (!readPart3 && readPart2 && Time.time > nextRead)
            {
                readPart3 = true;
                nextRead = Time.time + checkForNextRead;
                text = "When you leave the graveyard press space when near an easy target to possess him.";
                typing = false;
            }
            if (!readPart4 && readPart3 && Time.time > nextRead)
            {
                readPart4 = true;
                nextRead = Time.time + checkForNextRead;
                text = "Try for someone who is sleeping or weak.";
                typing = false;
            }
        }
        //moves the player after all the dialogue is done
        if (isDoneTalking && playerIsInRange)
        {
            TextPanel.SetActive(false);
            if (Vector3.Distance(this.transform.position, pointsList[count].transform.position) > .3f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, pointsList[count].transform.position, speed * Time.deltaTime);

            }
            else
            {
                count += 1;
            }
        }

        if (count >= 5)
        {
            Destroy(this.gameObject);
        }

    }


    private void Talking()
    {
        isTalking = true;
        TextPanel.SetActive(true);
        if (!typing)
            StartCoroutine(BuildText());

        if (readPart1)
        {
            if (!typing)
                StartCoroutine(BuildText());
        }
        if (readPart2)
        {
            if (!typing)
                StartCoroutine(BuildText());
        }
        if (readPart3)
        {
            if (!typing)
                StartCoroutine(BuildText());
        }
        if (readPart4)
        {
            isTalking = false;
            isDoneTalking = true;
        }
        //isDoneTalking = true;
    }

    private IEnumerator BuildText()
    {
        speechText.text = "";
        typing = true;
        for (int i = 0; i < text.Length; i++)
        {
            speechText.text = string.Concat(speechText.text, text[i]);
            //Wait a certain amount of time, then continue with the for loop
            yield return new WaitForSeconds(timeLapse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Phantom2.0")
        {
            playerIsInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsInRange = false;
    }
}
