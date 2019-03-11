using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    public GameObject[] matchingDoor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameObject.Find("Phantom2.0") || other.gameObject.tag == "Player")
        {
            for(int i = 0; i < matchingDoor.Length; i++)
            {
                Destroy(matchingDoor[i]);
            }
            Destroy(this.gameObject);

        }
    }
}
