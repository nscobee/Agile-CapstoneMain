using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float primaryAtkDmg = 25f;
    public float secondaryAtkDmg = 50f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            collision.gameObject.GetComponent<AIHealth>().TakeDamage(primaryAtkDmg);
            Debug.Log("Primary attack used, " + primaryAtkDmg + " dmg.");

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            collision.gameObject.GetComponent<AIHealth>().TakeDamage(secondaryAtkDmg);
            Debug.Log("Secondary attack used, " + secondaryAtkDmg + " dmg.");
        }
    }
}
