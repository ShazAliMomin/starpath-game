using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
   [SerializeField] public int id;

    public float speed;
    public float stoppingDistance;
    public float retreatdistance;
    public float distanceBetween;
    public float distance;
    private float xpGain = 10f;

    private bool dead = false;

    public float health = 4;
    public Transform player;//this was added here

    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;

    void Start()
    {

        if (hubmanager.slarr[id] == true)
        {
            Destroy(gameObject);
        }


        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;//this was added
        playerLevel = GameObject.Find("Player").GetComponent<PlayerController1>();
        animator = GetComponent<Animator>();

        //added by hasib
      

    }

    void Update()
    {
        Vector2 dir = (transform.position - player.transform.position).normalized;
        int count = rb.Cast(dir, movementFilter, castCollisions, speed * Time.deltaTime);

        distance = Vector2.Distance(transform.position, player.position);
        if (distance < distanceBetween)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance && !dead)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatdistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatdistance && !dead)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
        }
    }


    private void FixedUpdate()
    {
        if (player != null && !dead)
        {
           /* spriteRenderer.flipX = player.position.x > rb.position.x;*/
        }
    }

    private PlayerController1 playerLevel;

    public PlayerController1 p;


    public void Defeated()
    {
       
        Destroy(gameObject);
    }


    public void TakeDamage()
    {
        if (health >= 0) { SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(17, 20)); }
        if (health <= 0)
        {

            
            if (dead == false)
            {

                hubmanager.slarr[id] = true;
                p.slimekill();

                SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(9, 12));
                dead = true;
                playerLevel.addXP(xpGain);
                animator.SetTrigger("isDead");
                Destroy(GetComponent<BoxCollider2D>());
            }
        }
    }
}
