using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class missletrack2 : MonoBehaviour
{
    public float speed;
    public int damage;
    public int defdmgproj = 1;
    public PlayerController1 playerHealth;
    public Transform target;
    private Vector2 target_pos;
    public float health;
    private bool dead = false;
    public float liveTime;

    [SerializeField] private ParticleSystem impactEffect;
    public float rotateSpeed = 200f;


    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //target_pos = new Vector2(target.position.x, target.position.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            DestroyProjectile();
        }
        if (health <= 0)
        {
            DestroyProjectile();
        }
        //transform.up = target.transform.position - transform.position;
        Vector2 direction = (Vector2)target.position - rb.position;
        //transform.position = Vector2.MoveTowards(transform.position, target_pos, speed * Time.deltaTime);
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerController1>().TakeDamage(damage);
            DestroyProjectile();
        }
        else if(other.tag == "Bullet")
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            health = health - 1;
            if (health <= 0)
            {
                DestroyProjectile();
            }
        }
        else if (other.tag == "edge")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            DestroyProjectile();
        }
        else if (other.tag == "MeleeHitbox")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            DestroyProjectile();
        }

    }

    void DestroyProjectile()
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void CheckIfDead()
    {
        if (dead == false)
        {
        Instantiate(impactEffect, transform.position, transform.rotation);
        DestroyProjectile();
        }
    }
}

