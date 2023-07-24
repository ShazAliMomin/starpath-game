using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shield : MonoBehaviour
{

    public float health;
    public Transform target;

    [SerializeField] private ParticleSystem impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Shield").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            DestroyShield();
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
       
        if(other.tag == "Bullet")
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            health = health - 1;
            if (health <= 0)
            {
                DestroyShield();
            }
        }
        else if (other.tag == "MeleeHitbox")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            DestroyShield();
        }

    }

    void DestroyShield()
    {
        Destroy(gameObject);

    }
}
