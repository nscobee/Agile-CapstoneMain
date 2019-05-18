using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
