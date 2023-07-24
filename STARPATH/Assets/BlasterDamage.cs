using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterDamage : MonoBehaviour
{
    public int damage;
    public PlayerController1 player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);
        }
    }

}
