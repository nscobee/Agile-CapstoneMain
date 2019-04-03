using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointFunctions : MonoBehaviour
{
    public SaveLoadController scontrol;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Phantom2.0")
        {
            scontrol.SaveLevel();
        }

    }
}
