using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI : EnemyAI{

    public float timeTillAttack;
    public float startTimeTillAttack;
    public LayerMask playerLayer;
    public Transform fireball;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if(playerInRange)
        {
            ChasePlayer();
            //attack with fireball
            if (timeTillAttack < 0)
            {
                Attack();
                Collider2D[] thingsToHit = Physics2D.OverlapCircleAll(fireball.transform.position, bulletSplashArea, playerLayer);
                for (int i = 0; i < thingsToHit.Length; i++)
                {
                    thingsToHit[i].GetComponent<playerController>().takeDamage(damageAmount);
                }
            }
        }
    }

    
}
