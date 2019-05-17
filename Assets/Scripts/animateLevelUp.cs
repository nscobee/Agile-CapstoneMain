using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateLevelUp : MonoBehaviour
{
    
    public Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);       
    }
}
