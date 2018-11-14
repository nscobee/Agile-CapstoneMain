using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public float fireDamage = 40f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            collision.gameObject.GetComponent<AIHealth>().TakeDamage(fireDamage);
            Debug.Log("Secondary spell used, " + fireDamage + " dmg.");
        }
    }
}
