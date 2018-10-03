using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayerHit : MonoBehaviour
{
    playerController playerScript;
    EnemyAI enemyai;
    //MageAI mageai;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player hit, taking damage");
        playerScript.takeDamage(enemyai.damageAmount);
        this.enabled = false;
    }

    // Use this for initialization
    void Start ()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        enemyai = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyAI>();
        //mageai = GameObject.FindGameObjectWithTag("mage").GetComponent<MageAI>();
    }
	
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(enemyai.bullet.transform.position, enemyai.bulletSplashArea);
    //}
}
