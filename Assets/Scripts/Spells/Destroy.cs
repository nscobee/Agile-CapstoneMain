using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float duration = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(this.gameObject, duration);
        }
    }
}
