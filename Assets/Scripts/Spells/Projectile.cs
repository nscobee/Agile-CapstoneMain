using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 target;
    public float speed = 25f;
    public float maxDistance = 15f;

    // Use this for initialization
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }
}
