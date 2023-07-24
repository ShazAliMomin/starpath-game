using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFriendly : MonoBehaviour
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
    private GameObject enemies;

    // private PlayerController1 player;
    private Transform playerPos;

    private static int switchCase = 1;
    public static Vector3 friendlyProjectileVec;
    private int timeDelayDestroy = 0;
    private int shotsFired = 0;
    private int desiredNumberOfShoots = 7;

    private static int turretLevel;
    private static int turretCap;

    private static bool alternateDirection = false;

    //private int consecutiveProjectiles = 4;

    // private float xpGain = 20f;


    // Start is called before the first frame update
    void Start()
    {
        //turretLevel = AbilityManager.AbilityManagerInstance.turretLevel;
        turretLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(6, 0);
        if (turretLevel <= 4) turretCap = turretLevel;
        else turretCap = 4;
        
        timeBtwShots = startTimeBtwShots;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Invoke("destroyFriendlyTurret()", 4f);

    }

    // Update is called once per frame
    void Update()
    {  
      
            
        if (timeBtwShots <= 0)
            {
            //turretLevel = AbilityManager.AbilityManagerInstance.turretLevel;
            turretLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(6, 0);
            if (turretLevel <= 4) turretCap = turretLevel;
                else turretCap = 4;

            //SoundManager.SoundManagerInstance.Engage();
            SoundManager.SoundManagerInstance.PlayFireSound(3);
                for (int i=0 ; i< 4*turretCap; i++) {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    shotsFired++;
                }

                if (shotsFired == (4*turretCap) * desiredNumberOfShoots) {
                    destroyFriendlyTurret();
                }

                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        
    }

    private void FixedUpdate()
    {  
        // if (timeDelayDestroy <200) {
        //     timeDelayDestroy++;
        // }
        // if (Input.GetKeyDown(KeyCode.T) && Shooting.turretPlaced && timeDelayDestroy >190) {
        //     // Destroy(gameObject);
        //     Shooting.turretPlaced = false;
        //     timeDelayDestroy = 0;
        // }

       
        
    }


    public static void shoot() {

        switch (switchCase)
        {
            case 1:
                if(turretCap > 1 || (alternateDirection && turretCap == 1)) friendlyProjectileVec = new Vector3(1, 0, 0);
                else friendlyProjectileVec = new Vector3(1, 1, 0);

                break;
            case 2:
                if (turretCap > 1 || (alternateDirection && turretCap == 1)) friendlyProjectileVec = new Vector3(0, 1, 0);
                else friendlyProjectileVec = new Vector3(-1, 1, 0);

                break;
            case 3:
                if (turretCap > 1 || (alternateDirection && turretCap == 1)) friendlyProjectileVec = new Vector3(-1, 0, 0);
                else friendlyProjectileVec = new Vector3(1, -1, 0);

                break;
            case 4:
                if (turretCap > 1 || (alternateDirection && turretCap == 1)) friendlyProjectileVec = new Vector3(0, -1, 0);
                else friendlyProjectileVec = new Vector3(-1, -1, 0);

                break;
            case 5:
                friendlyProjectileVec = new Vector3(1, 1, 0);
                break;
            case 6:
                friendlyProjectileVec = new Vector3(-1, 1, 0);
                break;
            case 7:
                friendlyProjectileVec = new Vector3(1, -1, 0);
                break;
            case 8:
                friendlyProjectileVec = new Vector3(-1, -1, 0);
                break;
            case 9:
                friendlyProjectileVec = new Vector3(0.5f, 1, 0);
                break;
            case 10:
                friendlyProjectileVec = new Vector3(1, 0.5f, 0);
                break;
            case 11:
                friendlyProjectileVec = new Vector3(-0.5f, -1, 0);
                break;
            case 12:
                friendlyProjectileVec = new Vector3(-1, 0.5f, 0);
                break;
            case 13:
                friendlyProjectileVec = new Vector3(-0.5f, 1, 0);
                break;
            case 14:
                friendlyProjectileVec = new Vector3(-1, -0.5f, 0);
                break;
            case 15:
                friendlyProjectileVec = new Vector3(0.5f, -1, 0);
                break;
            case 16:
                friendlyProjectileVec = new Vector3(1, -0.5f, 0);
                break;

            default:
                break;
        }

        switchCase++;
        if(switchCase > 4 * turretCap)
        {
            switchCase = 1;
            alternateDirection = !alternateDirection;
        }

        /*
        switch (switchCase)
        {
            case 1:
                friendlyProjectileVec = new Vector3(1,0,0);
                break;
            case 2:
                friendlyProjectileVec = new Vector3(1,-1,0);
                break;
            case 3:
                friendlyProjectileVec = new Vector3(0,-1,0);
                break;
            case 4:
                friendlyProjectileVec = new Vector3(-1,-1,0);
                break;
            case 5:
                friendlyProjectileVec = new Vector3(-1,0,0);
                break;
            case 6:
                friendlyProjectileVec = new Vector3(-1,1,0);
                break;
            case 7:
                friendlyProjectileVec = new Vector3(0,1,0);
                break;
            case 8:
                friendlyProjectileVec = new Vector3(1,1,0);
                break;

            default:
                break;
        }
        
        switchCase++;
        if (switchCase > 8) {
            switchCase = 1;
        }
        */
    }

    public void destroyFriendlyTurret() {
        Destroy(gameObject);
    }
}
