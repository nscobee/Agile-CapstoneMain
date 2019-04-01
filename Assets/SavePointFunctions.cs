using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointFunctions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Phantom2.0")
        {
            SaveLoadController.control.SaveLevel("bleh");
        }

    }
}
