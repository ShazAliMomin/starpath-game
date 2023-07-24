using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private PlayerController1 playerLevel;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public int damage = 2;
    private Transform playerPos;

    public GameObject player;
    private Transform target;
    public float speed;
    public float distanceBetween;

    private float minRange = .60f;
    private float distance = 5f;
    private bool canMove = true;

    private bool dead = false;

    public float meleeCooldown = 10f;
    private float canAttack;

    private float xpGain = 15f;

    public float health = 2;
    public float meleeResistance = 0.05f;
    public bool takingDamage;

    public float stunTime;
    private bool stunned = false;

    private bool deadByMelee = false;

    private bool marching = false;
    private Transform marchTarget;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
        playerPos = FindObjectOfType<PlayerController1>().transform;
        playerLevel = GameObject.Find("Player").GetComponent<PlayerController1>();
        takingDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            CheckIfDead();
            LockMovement();
        }

        Vector2 dir = (transform.position - player.transform.position).normalized;
        int count = rb.Cast(dir, movementFilter, castCollisions, speed * Time.deltaTime + collisionOffset);
        for(int i=0; i<castCollisions.Count; i++)
        {
            if(castCollisions[i].transform != null)
            {
                if (castCollisions[i].transform.tag == "BlasterEnemy" || castCollisions[i].transform.tag == "Enemy" || castCollisions[i].transform.tag == "Bullet")
                {
                    count--;
                }
            }
        }
        


        if (player != null)
        {
            if (canMove)
            {
                distance = Vector2.Distance(transform.position, player.transform.position);
                Vector2 direction = player.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (marching && distance >= distanceBetween)
                {
                    transform.position = Vector2.MoveTowards(transform.position, marchTarget.position, speed * Time.deltaTime);
                    animator.SetBool("isMoving", true);
                }

                else if (distance < distanceBetween && distance > 0.5 && count == 0)
                {
                    SoundManager.SoundManagerInstance.Engage();
                    animator.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null && !dead)
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

        if (stunned)
        {
            stunTime -= 1;
            if(stunTime <= 0)
            {
                stunned = false;
                UnlockMovement();
            }
        }
    }

    public void March(Transform target)
    {
        marching = true;
        marchTarget = target;
    }

    public void TakeDamage()
    {
        if (health >= 0) { SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(9, 16)); }
        animator.SetBool("isHit", true);
        if (health <= 0)
        {
            CheckIfDead();
            /**
            animator.SetTrigger("isDead");
            if (dead == false) {
                SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(4, 7));
                print("XP!!!");
                playerLevel.addXP(xpGain);
                dead = true;
                canMove = false;
                Destroy(GetComponent<BoxCollider2D>());
                LockMovement();
                //Defeated();
            }
        
    */
        }
    }

    public void TakeDamage(bool melee)
    {
        if (health >= 0) { SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(9, 16)); }
        animator.SetBool("isHit", true);
        if (health <= 0)
        {
            deadByMelee = true;
            CheckIfDead();
            /*
            if (melee == true)
            {
                player.GetComponent<PlayerController1>().gainHealth(1);
            }*
            /**
            animator.SetTrigger("isDead");
            if (dead == false) {
                SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(4, 7));
                print("XP!!!");
                playerLevel.addXP(xpGain);
                dead = true;
                canMove = false;
                Destroy(GetComponent<BoxCollider2D>());
                LockMovement();
                //Defeated();
            }
        
    */
        }
    }



    private void CheckIfDead()
    {
            animator.SetTrigger("isDead");
            if (dead == false)
            {
                if(deadByMelee == true)
                {
                playerLevel.gainHealth(1);
                }
                SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(4, 7));
                print("XP!!!");
                playerLevel.addXP(xpGain);
                dead = true;
                canMove = false;
                Destroy(GetComponent<BoxCollider2D>());
                LockMovement();
                //Defeated();
            }
    }

    private void CheckIfDead(bool melee)
    {
        Debug.Log("New method called");
        animator.SetTrigger("isDead");
        if (dead == false)
        {
            if (melee)
            {
                Debug.Log("giving player health");
                playerLevel.gainHealth(1);
            }
            SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(4, 7));
            print("XP!!!");
            playerLevel.addXP(xpGain);
            dead = true;
            canMove = false;
            Destroy(GetComponent<BoxCollider2D>());
            LockMovement();
            //Defeated();
        }
    }

    public void StopDamage()
    {
        animator.SetBool("isHit", false);
    }

    public void Defeated()
    {
       
        Destroy(gameObject);
    }

    public void LockMovement()
    {
        canMove = false;
        animator.SetBool("isMoving", false);
    }

    public void UnlockMovement()
    {
        canMove = true;
        animator.SetBool("isMoving", true);
    }

    public void Stun(float duration)
    {
        stunned = true;
        LockMovement();
        stunTime = duration;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (meleeCooldown <= canAttack)
            {
                animator.SetTrigger("meleeAttack");
                other.gameObject.GetComponent<PlayerController1>().TakeDamage(damage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "StunAOE") Debug.Log("collided");
        

        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            animator.SetBool("isMoving", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = null;
            animator.SetBool("isMoving", false);
        }
    }
}
