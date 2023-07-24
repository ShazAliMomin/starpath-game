using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airstrike : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private float damage;
    public Vector3 vec3 = new Vector3(0, 0, 0);

    [SerializeField] private ParticleSystem impactEffect;

    // Start is called before the first frame update
    void Start()
    {
       // mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        
        
        

        Vector3 direction = Vector3.down;
        Vector3 rotation = transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damage = Shooting.shootingDamage;
        
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "TurretBullet"
                && other.gameObject.tag != "TurretFriendly" && other.gameObject.tag != "BossBullet")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
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
        else if (other.gameObject.tag == "Boss")
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                //boss.health -= damage;

                if (boss.health == 0)
                {

                }
                else
                {
                    boss.TakeDamage( damage);
                }
            }
        }
    }

    //public void changeDamage(int shootMode)
    //{
    //    if(shootMode == 1)
    //    {
    //        damage = 2;
    //    }
    //    if(shootMode == 2)
    //    {
    //        damage = 0.3f;
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D other) {
    //    // Debug.Log(obj);
    //    //SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(5, 7));

    //    if(other.gameObject.tag != "Player")
    //        Destroy(gameObject);

    //     if (other.gameObject.tag == "Enemy")
    //    {
    //        MeleeEnemy enemy = other.gameObject.GetComponent<MeleeEnemy>();
    //        if (enemy != null)
    //        {
    //            enemy.Health -= damage;
    //            enemy.animator.SetBool("isHit", true);
    //            if (enemy.Health == 0)
    //            {

    //            }
    //            else
    //            {
    //                enemy.TakeDamage();
    //            }
    //        }
    //    }
    //    else if(other.gameObject.tag == "BlasterEnemy")
    //    {
    //        BlasterEnemy enemy = other.gameObject.GetComponent<BlasterEnemy>();
    //        if (enemy != null)
    //        {
    //            enemy.Health -= damage;

    //            if (enemy.Health == 0)
    //            {

    //            }
    //            else
    //            {

    //            }
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {

    }
}