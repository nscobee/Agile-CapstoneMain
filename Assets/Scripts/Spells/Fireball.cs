using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : BasicSpells
{
    private const int MAX_LENGTH = 5;
    public Vector3 playerPos;
    public Vector3 mousePos;
    //public Vector3 distanceFromPlayer;
    public GameObject fireball;
    public float spellSpeed = 3f;

    public PhantomControls playerMovement;
    

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.Find("Phantom2.0").transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        /*

        if (Input.GetKeyDown(KeyCode.Mouse0) && playerMovement.isPossessing)

        {
            Fire();
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerPos, mousePos);
        
    }

    public void Fire()
    {
        GameObject.Instantiate(fireball, playerPos, Quaternion.identity);
        fireball.transform.position = fireball.transform.position - playerPos * spellSpeed;

    }
    
}
