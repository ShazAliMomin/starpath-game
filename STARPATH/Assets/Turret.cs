using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float health = 4f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite south;
    public Sprite north;
    public Sprite east;
    public Sprite west;
    public Sprite nw;
    public Sprite ne;
    public Sprite sw;
    public Sprite se;

    public GameObject projectile;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float distanceBetween;
    public float distance;

    private PlayerController1 player;
    private Transform playerPos;
    //private Transform target;
    //private MeleeAttack melee;
    private float xpGain = 20f;

    public float stunTime;
    private bool stunned = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController1>();
        //melee = GameObject.Find("MeleeHitbox").GetComponent<MeleeAttack>();
        timeBtwShots = startTimeBtwShots;
        playerPos = FindObjectOfType<PlayerController1>().transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stunned)
        {
            distance = Vector2.Distance(transform.position, playerPos.position);
            if (distance < distanceBetween)
            {

                if (timeBtwShots <= 0)
                {
                    SoundManager.SoundManagerInstance.Engage();
                    SoundManager.SoundManagerInstance.PlayFireSound(3);
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }

            }
            checkDirection();
        }
    }

    private void FixedUpdate()
    {
        if (stunTime <= 0) stunned = false;
        else stunTime -= 1;


        //if(playerPos.position.x < rb.position.x && playerPos.position.y <= rb.position.y+30 && playerPos.position.y >= rb.position.y-30)
        //{
        //    spriteRenderer.sprite = west;
        //}

        //if(playerPos.position.x > rb.position.x && playerPos.position.y <= rb.position.y+30 && playerPos.position.y >= rb.position.y-30)
        //{
        //    spriteRenderer.sprite = east;
        //}

        //if (playerPos.position.y > rb.position.y && playerPos.position.x <= rb.position.x + 30 && playerPos.position.x >= rb.position.x - 30)
        //{
        //    spriteRenderer.sprite = north;
        //}

        //if (playerPos.position.y < rb.position.y && playerPos.position.x <= rb.position.x + 30 && playerPos.position.x >= rb.position.x - 30)
        //{
        //    spriteRenderer.sprite = south;
        //}


    }


    private void checkDirection()
    {
        //Vector2 relativePos = transform.position - playerPos.position;

        Vector2 relativePos = playerPos.position - transform.position;



        if (relativePos.y < -2)
        {
            if (Mathf.Abs(relativePos.x) < (Mathf.Abs(relativePos.y / 2)))
            {
                //Debug.Log("Direction: South");
                spriteRenderer.sprite = south;
            }
            else if (relativePos.x < 0)
            {
                //Debug.Log("Direction: South West");
                spriteRenderer.sprite = sw;
            }
            else
            {
                //Debug.Log("Direction: South East");
                spriteRenderer.sprite = se;
            }
        }
        else if (relativePos.y > -2 && relativePos.y < 2)
        {
            if (relativePos.x < 0)
            {
                //Debug.Log("Direction: West");
                spriteRenderer.sprite = west;
            }
            else
            {
                //Debug.Log("Direction: East");
                spriteRenderer.sprite = east;
            }
        }
        else
        {
            if (Mathf.Abs(relativePos.x) < (Mathf.Abs(relativePos.y / 2)))
            {
                //Debug.Log("Direction: North");
                spriteRenderer.sprite = north;
            }
            else if (relativePos.x < 0)
            {
                //Debug.Log("Direction: North West");
                spriteRenderer.sprite = nw;
            }
            else
            {
                //Debug.Log("Direction: North East");
                spriteRenderer.sprite = ne;
            }
        }
    }


    public void TakeDamage()
    {
        SoundManager.SoundManagerInstance.PlayImpactSound(3);
        if (health <= 0)
        {
            player.addXP(xpGain);
            Defeated();
        }
    }

    public void Stun(float duration)
    {
        stunned = true;
        stunTime = duration;
    }

    public void Defeated()
    {
        SoundManager.SoundManagerInstance.PlayExplosionSound((int)Random.Range(0, 2));
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (timeBtwShots <= 0)
    //        {
    //            Instantiate(projectile, transform.position, Quaternion.identity);
    //            timeBtwShots = startTimeBtwShots;
    //        }
    //        else
    //        {
    //            timeBtwShots -= Time.deltaTime;
    //        }
    //    }
    //}
}
