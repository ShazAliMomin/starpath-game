using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFriendlyTurret : MonoBehaviour
{
    public float speed;
    public int damage = 3;
    // public PlayerController1 playerHealth;
    private Rigidbody2D rb;

    [SerializeField] private ParticleSystem impactEffect;

    private Transform player;
    private Vector2 target;

    public Vector3 direction;

    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;

        // target = new Vector2(player.position.x, player.position.y);
        rb = GetComponent<Rigidbody2D>();

        // direction = TurretFriendly.friendlyProjectileVec;
        // TurretFriendly.friendlyProjectileVec = new Vector3()
        
        TurretFriendly.shoot();
        Vector3 direction = TurretFriendly.friendlyProjectileVec;
        Debug.Log(direction + "direction");
        Vector3 rotation = new Vector3(1,1,1);
        rb.velocity = new Vector2(direction.x, direction.y).normalized * 10;
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        
        // transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyProjectile();
        }
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "TurretBullet"
            && other.gameObject.tag != "TurretFriendly" && other.gameObject.tag != "TriggerBox") {

            if (AbilityManager.AbilityManagerInstance.getUnlockAbility(6, 0) && (other.gameObject.tag != "Enemy" && other.gameObject.tag != "BlasterEnemy" 
                                                                             && other.gameObject.tag != "Turret" && other.gameObject.tag != "generator"  
                                                                             && other.gameObject.tag != "Boss"))
            {
                rb.velocity = new Vector2( -1 * (rb.velocity.x + (Random.Range(-5f, 5f))), -1 * (rb.velocity.x + (Random.Range(-5f, 5f))));
            }
            else
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            
        }

        if (other.gameObject.tag == "Enemy")
        {
            MeleeEnemy enemy = other.gameObject.GetComponent<MeleeEnemy>();
            if (enemy != null)
            {
                enemy.health -= damage;
                enemy.animator.SetBool("isHit", true);
                if (enemy.health == 0)
                {

                }
                else
                {
                    enemy.TakeDamage();
                }
            }
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
                blastEnemy.health -= (damage * .2f);

                if (blastEnemy.health == 0)
                {

                }
                else
                {
                    blastEnemy.TakeDamage();
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


            }
        }
        else if (other.tag == "generator")
        {
            generatorscript gen = other.GetComponent<generatorscript>();
            if ( gen != null && gen.health > 0)
            {
                gen.TakeDamage(damage);

            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (boss != null)
            {

                if (boss.health == 0)
                {

                }
                else
                {
                    boss.TakeDamage(damage);
                }
            }
        }

        else if (other.tag == "beacon")
        {
            beac1 b = other.GetComponent<beac1>();
            if (b != null && b.health > 0)
            {
                b.TakeDamage(damage);

            }
        }

        else if (other.tag == "lasert")
        {
            lasert l = other.GetComponent<lasert>();
            if (l != null && l.health > 0)
            {
                l.TakeDamage(damage);

            }
        }




    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}