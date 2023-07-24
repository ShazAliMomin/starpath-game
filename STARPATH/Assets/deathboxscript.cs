using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathboxscript : MonoBehaviour

{
    bool playerdead = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
            }

    // Update is called once per frame
    void Update()
    {
        if(playerdead)
        {
            player.GetComponent<PlayerController1>().TakeDamage(10000);
        }

        



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerdead = true;
            
        }

        else if (other.gameObject.tag == "BlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                blastEnemy.health -= 10000;

                if (blastEnemy.health == 0)
                {

                }
                else
                {
                    blastEnemy.TakeDamage();
                }
            }
        }







    }


}
