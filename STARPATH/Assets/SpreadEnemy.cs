using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public float speed;
    public float stoppingDistance;
    public float retreatdistance;
    public float distanceBetween;
    public float distance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private float xpGain = 20f;

    private bool dead = false;

    private bool stunned = false;
    private float stunTime;

    private bool marching = false;
    private Transform marchTarget;

    public float health = 5;
    public GameObject projectile;
    public GameObject projectile1;
    public GameObject projectile2;
    public Transform player;//this was added here
    private bool shielded; 
    [SerializeField] private GameObject shield;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    void Start()
    {
        shielded = true; 
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;//this was added
        timeBtwShots = startTimeBtwShots;
        playerLevel = GameObject.Find("Player").GetComponent<PlayerController1>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    void Update()
    {
        if (health <= 0)
        {
            CheckIfDead();
        }

        if (!stunned)
        {

            distance = Vector2.Distance(transform.position, player.position);

            if (marching && distance >= distanceBetween)
            {
                transform.position = Vector2.MoveTowards(transform.position, marchTarget.position, speed * Time.deltaTime);
                animator.SetBool("isMoving", true);
            }

            else if (distance < distanceBetween)
            {
                Vector2 dir = (transform.position - player.position).normalized;
                int count = rb.Cast(dir, movementFilter, castCollisions, speed * Time.deltaTime + collisionOffset);

                marching = false;
                if (count == 0 && Vector2.Distance(transform.position, player.position) > stoppingDistance && !dead)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    animator.SetBool("isMoving", true);
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatdistance)
                {
                    transform.position = this.transform.position;
                    animator.SetBool("isMoving", false);
                }
                else if (count == 0 && Vector2.Distance(transform.position, player.position) < retreatdistance && !dead)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                    animator.SetBool("isMoving", true);
                }


                if (timeBtwShots <= 0 && !dead)
                {
                    SoundManager.SoundManagerInstance.Engage();
                    SoundManager.SoundManagerInstance.PlayFireSound(6);
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    Instantiate(projectile1, transform.position, Quaternion.identity);
                    Instantiate(projectile2, transform.position, Quaternion.identity);
                    //Instantiate(projectile, player.position, Quaternion.identity);
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
        if (player != null && !dead)
        {
            spriteRenderer.flipX = player.position.x > rb.position.x;
        }

        if (stunTime <= 0) stunned = false;
        else stunTime -= 1;
    }

    public void March(Transform target)
    {
        marching = true;
        marchTarget = target;
    }

    private PlayerController1 playerLevel;

    //public float speed;
    //public float stoppingDistance;
    //public float retreatdistance;

   // private float timeBtwShots;
    //public float startTimeBtwShots;

    //public GameObject projectile;
    //public Transform player;


    //public float Health
    //{
    //    set
    //    {
    //        health = value;
    //        if (health <= 0)
    //        {
    //           Defeated();
    //        }
    //    }
    //    get
    //    {
    //        return health;
    //    }
    //}
    //private float xpGain = 25f;

    //public float health = 5;

    public void Defeated()
    {
        Destroy(gameObject);
    }

    public void Stun(float duration)
    {
        stunned = true;
        stunTime = duration;
    }


    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").transform;

    //    timeBtwShots = startTimeBtwShots;

    //    playerLevel = GameObject.Find("Player").GetComponent<PlayerController1>();
    //}


    //void Update()
    //{
       // if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        //{
           // transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //}
    //    else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatdistance)
    //    {
    //        transform.position = this.transform.position;
    //    }
    //    else if (Vector2.Distance(transform.position, player.position) < retreatdistance)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
    //    }


    //    if (timeBtwShots <= 0)
    //    {
    //        Instantiate(projectile, transform.position, Quaternion.identity);
    //        timeBtwShots = startTimeBtwShots;
    //    }
    //    else
    //    {
    //        timeBtwShots -= Time.deltaTime;
    //    }

    //}

    public void TakeDamage()
    {
        if (health >= 0) { SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(9, 16)); }
        animator.SetTrigger("isHit");
        if (health <= 0)
        {
            CheckIfDead();

            if (dead == false)
            {
                SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(4, 7));
                dead = true;
                playerLevel.addXP(xpGain);
                animator.SetTrigger("isDead");
                Destroy(GetComponent<BoxCollider2D>());
            }
        }
    }

    private void CheckIfDead()
    {
        animator.SetTrigger("isDead");
        if (dead == false)
        {
            SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(4, 7));
            print("XP!!!");
            playerLevel.addXP(xpGain);
            dead = true;
            //canMove = false;
            Destroy(GetComponent<BoxCollider2D>());
            //LockMovement();
            //Defeated();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "StunAOE") Debug.Log("collided");
    }
}
