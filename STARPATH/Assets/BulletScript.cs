using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private GameObject controllerCursor;
    private Rigidbody2D rb;
    public float force;
    private float damage;
    public Vector3  vec3 = new Vector3(0,0,0);
    private bool robotStun = false;


    [SerializeField] private ParticleSystem impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        robotStun = false;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        controllerCursor = GameObject.FindGameObjectWithTag("GameCursor");
        rb = GetComponent<Rigidbody2D>();

        if (AbilityManager.AbilityManagerInstance.GetController())
        {
            Debug.Log("controller");
            mousePos = controllerCursor.transform.position;
        }
        else
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
        


        Shooting.adjustShotgunVecVal();
        //vec3 = new Vector3 (0, Shooting.shotgunVecVal, 0);
        //vec3 = new Vector3(Shooting.shotgunVecVal, Shooting.shotgunVecVal, 0);

        Vector3 relativePos = mousePos - transform.position;
        if(Mathf.Abs(relativePos.x) < (Mathf.Abs(relativePos.y) / 2)){
            vec3 = new Vector3(Shooting.shotgunVecVal, 0, 0);
        }
        else
        {
            vec3 = new Vector3(0, Shooting.shotgunVecVal, 0);
        }
    
        Vector3 direction = mousePos - transform.position - vec3;
        Vector3 rotation = transform.position - mousePos;





        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Destroy(gameObject, Shooting.bulletTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damage = Shooting.shootingDamage;

        if (AbilityManager.AbilityManagerInstance.getUnlockAbility(1, 1)) robotStun = true;

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "TurretBullet"
                && other.gameObject.tag != "TurretFriendly" && other.gameObject.tag != "BossBullet" && other.gameObject.tag != "TriggerBox" && other.gameObject.tag != "Breaking Ground" && other.gameObject.tag != "flaskSplash") {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            MeleeEnemy enemy = other.gameObject.GetComponent<MeleeEnemy>();
            if (enemy != null)
            {
                if (AbilityManager.AbilityManagerInstance.getUnlockAbility(2, 0) && AbilityManager.AbilityManagerInstance.GetShootMode() == 2)
                {
                    enemy.health -= damage;
                }
                else
                {
                    enemy.health -= (damage - (.3f));
                }
                
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
            SpreadEnemy spreadenemy = other.gameObject.GetComponent<SpreadEnemy>();
            ScientistEnemy evilnorman = other.gameObject.GetComponent<ScientistEnemy>();
            Debug.Log(evilnorman);
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
            else if (spreadenemy != null)
            {
                
                spreadenemy.health -= damage;

                if (spreadenemy.health == 0)
                {

                }
                else
                {
                    spreadenemy.TakeDamage();
                }
            }
            else if( evilnorman != null)
            {
                Debug.Log("HelloWOW");
                evilnorman.health -=damage;

                if(evilnorman.health == 0)
                {

                }
                else
                {
                    evilnorman.TakeDamage();
                }

            }


        }
        
        

        else if (other.gameObject.tag == "ShieldedBlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                if (AbilityManager.AbilityManagerInstance.getUnlockAbility(2, 0) == true && AbilityManager.AbilityManagerInstance.GetShootMode() == 2) blastEnemy.health -= damage;
                else blastEnemy.health -= (damage * .2f);

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
            turret_rocket rocketTurret = other.GetComponent<turret_rocket>();
            if (turret != null)
            {
                turret.health -= damage;
                turret.TakeDamage();

                if(AbilityManager.AbilityManagerInstance.getUnlockAbility(1,1) == true ) turret.Stun(30f);
            }
            if (rocketTurret != null)
            {
                rocketTurret.health -= damage;
                rocketTurret.TakeDamage();
                if (AbilityManager.AbilityManagerInstance.getUnlockAbility(1, 1) == true) rocketTurret.Stun(30f);
            }
        }

        else if(other.tag == "TurretBoss")
        {
            Turret1 turret = other.GetComponent<Turret1>();
            if(turret != null)
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

        else if (other.tag == "beacon")
        {
            beac1 b = other.GetComponent<beac1>();
            if (b != null && b.health > 0)
            {
                b.TakeDamage(damage);

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
                    boss.TakeDamage( damage );
                }
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
