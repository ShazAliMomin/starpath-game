using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeStun : MonoBehaviour
{

    private float stunTime = 90f;
    private float lifetime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + 0.00001f, transform.position.y);
    }

    void FixedUpdate()
    {
        lifetime -= 1f;
        if(lifetime <= 0) Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("collision");

        if (other.tag == "Enemy")
        {
            MeleeEnemy enemy = other.GetComponent<MeleeEnemy>();

            if (enemy != null)
            {
                Debug.Log("hit melee enemy");
                enemy.Stun(stunTime);
            }
        }
        else if (other.gameObject.tag == "BlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                Debug.Log("hit blaster enemy");
                blastEnemy.Stun(stunTime);
            }

        }
        else if (other.gameObject.tag == "Slime")
        {
            SlimeScript slime = other.gameObject.GetComponent<SlimeScript>();
            if (slime != null)
            {

            }
        }
        else if (other.tag == "Turret")
        {

        }

    }
}
