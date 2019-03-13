using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGuide : MonoBehaviour
{
    public List<Transform> pointsList = new List<Transform>();
    public float dialogueWait = 7f;
    public float speed = 1.5f;
    private int count = 0;
    private bool playerIsInRange = false;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= dialogueWait && playerIsInRange)
        {
            if (Vector3.Distance(this.transform.position, pointsList[count].transform.position) > .3f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, pointsList[count].transform.position, speed * Time.deltaTime);

            }
            else
            {
                count += 1;
            }
        }

        if(count >= 5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Phantom2.0")
        {
            playerIsInRange = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsInRange = false;
    }
}
