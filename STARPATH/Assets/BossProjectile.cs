using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public PlayerController1 playerHealth;

    [SerializeField] private ParticleSystem impactEffect;
    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyProjectile();
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
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
