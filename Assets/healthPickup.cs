using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    //if colliding with the player who is possessing an NPC (because the ghosty boi doesn't have health) increase mana and health by 20%
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Possessed")
        {
            other.gameObject.GetComponent<UIController>().increaseHealth(other.gameObject.GetComponent<UIController>().MAXHP * .2f);
            other.gameObject.GetComponent<UIController>().increaseMana(other.gameObject.GetComponent<UIController>().MAXMANA * .2f);
            Destroy(this.gameObject);
        }
    }
}
