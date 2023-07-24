using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public static int rdep = 0;
    

    public float maxHealth;
    public float health;
    public Image healthBar;
    int thresh;
    int depd;

    public GameObject bossProjectile;
    public GameObject bossRocket;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public int slamDamage = 8;

    private float count = 0;
    private bool canAttack = true;
    private bool isImmune = false;
    private bool isBerserk = false;

    public float speed = 1f;
    public float distanceBetween;
    private float distance = 5f;
    private bool canMove = true;

    Animator animator;

    public GameObject player;
    public GameObject sheild, sheild2;
    private PlayerController1 playerLevel;
    private Transform playerPos;
    private float xpGain = 50f;

    private float slamSoundTimerMax = 1.0f;
    private float slamSoundTimer;
    public static int p = 1;

    void Start()
    {
        
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        timeBtwShots = startTimeBtwShots;

        slamSoundTimer = 0.0f;

        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);

        playerLevel = GameObject.Find("Player").GetComponent<PlayerController1>();
        playerPos = FindObjectOfType<PlayerController1>().transform;
        thresh = 3;
        depd = 0;
        

    }

    void Update()
    {
        print("boss.p = " + p);
        /*
        if (health <= 100 && !isBerserk)
        {
            isBerserk = true;
            isImmune = true;
            Invoke("CanDamage", 3);
            print("IsBeserk");
            speed = 1.7f;
        }
        */

        if (depd  > 3 && depd <= 6)
        {
            thresh = 2;
        }

        if (depd > 6 && depd <= 12)
        {
            thresh = 3;
        }


        if (bosslevelmanager.mi < 3)
        {
            p = 1;
            print("phase 1");
        }

        else if (health >= (maxHealth * (3.0 / 4.0)) )
        { 
            p = 2;
            print("phase 2");
        
        }
        
        else if (health < (maxHealth * (3.0 / 4.0)) && health >= (maxHealth * (1.0 / 2.0)) )
        {
            p = 2;
            print("othernphase 2");
        }
        else if (health < (maxHealth * (1.0 / 2.0)) && health >= (maxHealth * (1.0 / 4.0)) )
        {
            p = 3;
            print("phase 3");
        }
        else
        {
            p = 4;
            print("phase 4");
        }

        if (p == 1)
        {
            sheild.SetActive(false);
            sheild2.SetActive(false);
            canMove = false;
            canAttack = false;
        
        }
        else if( p == 2)
        {
            
            canMove = false;
            canAttack = true;
            if (bosslevelmanager.beacs == 2)
            {
                print("Immunity Down");
                isImmune = false;
                sheild.SetActive(false);
                sheild2.SetActive(false);

            }
            else if (bosslevelmanager.beacs == 1)
            {
                sheild.SetActive(true);
                sheild2.SetActive(false);
            }
            else
            {
                isImmune = true;
                sheild.SetActive(true);
                sheild2.SetActive(true);
            }
        }

        else
        {
            canMove = true;
            isImmune = false;
            sheild.SetActive(false);
            sheild2.SetActive(false);
        }



        if (slamSoundTimer > 0)
        {
            slamSoundTimer -= Time.deltaTime;
        }

        if (player != null)
        {
            if (canMove)
            {
                animator.SetBool("isMoving", true);
                distance = Vector2.Distance(transform.position, player.transform.position);
                Vector2 direction = player.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (distance < distanceBetween && distance > 0.5)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    //animator.SetBool("isMoving", false);
                }

                if (timeBtwShots <= 0 && canAttack)
                {
                    SoundManager.SoundManagerInstance.PlayFireSound(5);
                    Instantiate(bossProjectile, transform.position, Quaternion.identity);
                    depd += 1;
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
            else if (p == 2)
            {

                if (timeBtwShots <= 0 && canAttack)
                {
                    
                    if (rdep < thresh)
                    {
                        SoundManager.SoundManagerInstance.PlayFireSound(10);
                        Instantiate(bossRocket, transform.position, Quaternion.identity);
                    }
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }


            }
        }



    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (playerPos.position.x > rb.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    public void TakeDamage( float d)
    {
        if (!isImmune)
        {
            health -= d;
            SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(9, 16));
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0)
            {
                SoundManager.SoundManagerInstance.PlayDeathSound(8);
                playerLevel.addXP(xpGain);
                animator.SetTrigger("isDead");
            }
        }
    }

    public void Defeated()
    {

        Destroy(gameObject);
    }

    private void CanDamage()
    {
        isImmune = false;
    }

    public void CanAttack()
    {
        canAttack = true;
    }

    public void CannotAttack()
    {
        canAttack = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMove = false;
            animator.SetBool("isMoving", false);
            if (slamSoundTimer <= 0 && canAttack)
            {
                slamSoundTimer = slamSoundTimerMax;
                animator.SetTrigger("slam");
                other.gameObject.GetComponent<PlayerController1>().TakeDamage(slamDamage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canMove = true;
    }

    private void PlaySlamSound()
    {
        SoundManager.SoundManagerInstance.PlayImpactSound(8);
    }
}
