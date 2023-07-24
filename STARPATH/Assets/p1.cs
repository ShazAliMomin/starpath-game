using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1 : MonoBehaviour
{
    public float speed;
    public int damage;
    public PlayerController1 playerHealth;

    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] Rigidbody2D m_RigidBody;
    private Transform player;
    private Vector2 target;
    //public Vector2 target;
    public float liveTime;
    //[SerializeField] private bool deflected = false;

    void Start()
    {
        //deflected = false;
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        //target = new Vector2(transform.position.x, transform.postion.y+10);

    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, transform.up, speed * Time.deltaTime);
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.CompareTag("Player"))
        //{
        //    playerHealth.TakeDamage(damage);
        //    DestroyProjectile();
        //}

        if (other.tag == "Player")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            
            other.gameObject.GetComponent<PlayerController1>().TakeDamage(damage);
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "BlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                blastEnemy.health -= damage;

                if (blastEnemy.health == 0)
                {

                }
                else
                {
                    blastEnemy.TakeDamage();
                }
            }
        }
        else if (other.gameObject.tag == "ShieldedBlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                blastEnemy.health -= (damage*.5f);

                if (blastEnemy.health == 0)
                {

                }
                else
                {
                    blastEnemy.TakeDamage();
                }
            }
        }
        else if (other.gameObject.tag == "Slime")
        {
            SlimeScript slime = other.gameObject.GetComponent<SlimeScript>();
            if (slime != null)
            {
                slime.health -= damage;

                if (slime.health == 0)
                {

                }
                else
                {
                    slime.TakeDamage();
                }
            }
        }
        else if (other.tag == "Turret")
        {
            Turret turret = other.GetComponent<Turret>();
            if (turret != null)
            {
                turret.health -= damage;


                turret.TakeDamage();
                turret.Stun(30f);

            }
        }
        else if (other.tag == "generator")
        {
            generatorscript gen = other.GetComponent<generatorscript>();
            if (gen != null && gen.health > 0)
            {
                gen.TakeDamage(damage);

            }
        }
        
        DestroyProjectile();
    }
    



    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}