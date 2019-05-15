using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateLevelUp : MonoBehaviour
{
    public GameObject phantom;
    public Animator anim;
    public levelingScript levelScript;

    // Start is called before the first frame update
    void Start()
    {
        phantom = GameObject.FindGameObjectWithTag("Player");
        levelScript = phantom.GetComponent<levelingScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelScript.currentLevel >= levelScript.xpTillNextLevel)
            anim.SetTrigger("levelUp");
    }
}
