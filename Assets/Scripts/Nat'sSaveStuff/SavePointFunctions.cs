using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointFunctions : MonoBehaviour
{
    public SaveLoadController scontrol;
    public void OnPossess()
    {
       
            scontrol.SaveLevel();
        

    }
}
