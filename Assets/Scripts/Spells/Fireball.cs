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
    //public float spellSpeed = 3f;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerPos, mousePos);
        
    }

    public void Fire()
    {
        GameObject.Instantiate(fireball, playerPos, Quaternion.identity);
        //fireball.GetComponent<Rigidbody2D>().velocity = fireball.transform.position - playerPos * spellSpeed;

    }
    
}
