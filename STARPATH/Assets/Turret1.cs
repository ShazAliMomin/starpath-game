using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret1 : MonoBehaviour
{
    public float maxHealth = 50f;
    public float health = 50f;
    public float maxShield = 100f;

    public Image healthBar;
    public Image shieldBar;
    public GameObject healthUI;

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
    public GameObject rocketProjectile;
    public GameObject defenseShield;
    private float timeBtwShots;
    private float timeBtwRapidShots;
    private float timeBtwShield;

    public float startTimeBtwShots;
    public float startTimeBtwRapidShots;
    public float startTimeBtwShield;
    public float distanceBetween;
    public float criticalDistanceBetween;
    public float distance;

    private bool missileReady;
    private bool berserkMode = false;

    private PlayerController1 player;
    private Transform playerPos;
    //private Transform target;
    //private MeleeAttack melee;
    private float xpGain = 20f;

    public float stunTime;
    private bool stunned = false;

    public bool shieldDisabled = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController1>();
        //melee = GameObject.Find("MeleeHitbox").GetComponent<MeleeAttack>();
        timeBtwShots = startTimeBtwShots;
        timeBtwShield = startTimeBtwShield;

        playerPos = FindObjectOfType<PlayerController1>().transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defenseShield.SetActive(true);

        missileReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();

        if (!stunned)
        {
            if(health < 50f && !berserkMode)
            {
                startTimeBtwShots /= 2;
                berserkMode = true;
            }

            distance = Vector2.Distance(transform.position, playerPos.position);
            if (distance < distanceBetween && distance > criticalDistanceBetween)
            {

                if (timeBtwShots <= 0)
                {
                    /*
                    SoundManager.SoundManagerInstance.Engage();
                    SoundManager.SoundManagerInstance.PlayFireSound(3);
                    Instantiate(projectile, transform.position, (Quaternion.identity * Quaternion.Euler(0, Random.Range(-5f, 5f), 0)));
                    */

                    if(defenseShield != null && !shieldDisabled) defenseShield.SetActive(false);
                    fireSmallArms();
                    Invoke("fireSmallArms", .05f);
                    Invoke("fireSmallArms", .1f);
                    Invoke("fireSmallArms", .15f);
                    Invoke("fireSmallArms", .2f);
                    Invoke("fireSmallArms", .25f);
                    if (missileReady) Invoke("fireMissile", .4f);
                    missileReady = !missileReady;
                    
                    if (defenseShield != null && !shieldDisabled) Invoke("engageShield", .5f);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }

                

            }
            else if(distance < criticalDistanceBetween )
            {

                if (timeBtwRapidShots <= 0)
                {
                    timeBtwRapidShots = startTimeBtwRapidShots;
                    if (defenseShield != null && !shieldDisabled) defenseShield.SetActive(false);
                    
                    SoundManager.SoundManagerInstance.PlayFireSound(12);
                    rapidFireProcess();
                }
                else
                {
                    timeBtwRapidShots -= Time.deltaTime;
                }



                timeBtwShots -= Time.deltaTime;
               
            }
            //Debug.Log("Position: " + (transform.position - playerPos.position));
            checkDirection();

            if (shieldDisabled && timeBtwShield <= 0)
            {
                shieldDisabled = false;
            }
            else
            {
                timeBtwShield -= Time.deltaTime;
            }
        }
    }

    private void updateUI()
    { 
        healthBar.fillAmount = health / maxHealth;

        if(defenseShield!=null)
        {
            float shieldHealth = defenseShield.GetComponent<shield>().health;
            shieldBar.fillAmount = shieldHealth / maxShield;
        }
        else
        {
            shieldBar.fillAmount = 0f;
        }
        
    }

    private void engageShield()
    {
        defenseShield.SetActive(true);
        /*
        if (defenseShield.GetComponent<shield>().health < 50f)
        {
            defenseShield.GetComponent<shield>().health += 5f;
        }
        */
    }

    private void fireSmallArms()
    {
        //SoundManager.SoundManagerInstance.Engage();
        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(projectile, transform.position, Quaternion.identity);           
    }

    private void fireRapidArms()
    {
        Instantiate(projectile, transform.position, Quaternion.identity); 
    }

    private void fireMissile()
    {
        SoundManager.SoundManagerInstance.PlayMiscSound(4);
        GameObject rocket = Instantiate(rocketProjectile, transform.position, Quaternion.identity);
        rocket.GetComponent<missletrack>().speed = 10;
        rocket.GetComponent<missletrack>().damage = 8;
        //rocket.GetComponent<missletrack>().AdjustImpactScale(.3f, .3f, .3f);
    }

    private void rapidFireProcess()
    {
        fireRapidArms();
        Invoke("fireRapidArms", .025f);
        Invoke("fireRapidArms", .05f);
        Invoke("fireRapidArms", .075f);
        Invoke("fireRapidArms", .1f);
        Invoke("fireRapidArms", .125f);
        Invoke("fireRapidArms", .15f);
        Invoke("fireRapidArms", .175f);
        Invoke("fireRapidArms", .2f);
        Invoke("fireRapidArms", .225f);
        Invoke("fireRapidArms", .25f);
        Invoke("fireRapidArms", .275f);
        Invoke("fireRapidArms", .3f);
        Invoke("fireRapidArms", .325f);
        Invoke("fireRapidArms", .35f);
        Invoke("fireRapidArms", .375f);
        Invoke("fireRapidArms", .4f);
        Invoke("fireRapidArms", .425f);
        Invoke("fireRapidArms", .45f);
        Invoke("fireRapidArms", .475f);
        Invoke("fireRapidArms", .5f);
        Invoke("fireRapidArms", .525f);
        Invoke("fireRapidArms", .55f);
        Invoke("fireRapidArms", .575f);
        Invoke("fireRapidArms", .6f);
        Invoke("fireRapidArms", .625f);
        Invoke("fireRapidArms", .65f);
        Invoke("fireRapidArms", .675f);
        Invoke("fireRapidArms", .7f);
        Invoke("fireRapidArms", .725f);
        Invoke("fireRapidArms", .75f);
        Invoke("fireRapidArms", .775f);
        Invoke("fireRapidArms", .8f);
        if (defenseShield != null && !shieldDisabled) Invoke("engageShield", 1f);


    }

    private void checkDirection()
    {
        //Vector2 relativePos = transform.position - playerPos.position;

        Vector2 relativePos = playerPos.position - transform.position;



        if(relativePos.y < -2)
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
        else if(relativePos.y > -2 && relativePos.y < 2)
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

    public void DamageShield()
    {
        defenseShield.GetComponent<shield>().health -= 25f;
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
        healthUI.SetActive(false);   
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
