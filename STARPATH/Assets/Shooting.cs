using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public GameObject grenade;
    public GameObject friendlyTurretObj;
    public Transform bulletTransform;
    public float shootCooldown;
    public float gunCooldown = 20;

    public Image turretIcon;

    public static float shootingDamage = 2;
    public static float bulletTime = 2;
    public static float shotgunVecVal = 0f;
    private bool canPlaceTurret = true;
    private static int shootMode = 1; // Might not need
    private static int OPGunBulletsLimit = 20;
    private float OPGunBulletsCoolDown = 4f;
    private int OPGunBullets = OPGunBulletsLimit;
    private float timeForNextTurret = 15f;
    public float turretTimer;
    private bool playedReadyNoise = true;
    //private BulletScript bulletObj;
    public Transform player;//this was added here
    public Transform gun;
    private static bool pistolAltFire = false;
    private static bool smgAltFire = false;
    private static bool shotgunAltFire = false;

    private int pistolLevel, smgLevel, aoeLevel, dashLevel, turretLevel, orbsLevel;
    private int rofLevelPistol, damageLevelPistol, damageLevelSMG, capacityLevelSMG;
    private bool threeBurst, stunGun, ignoreResist, overcharge, grenadeLauncher, slamfire;

    private static int shotgunLevel;
    private static float projectileLevelShotgun, spreadLevelShotgun;

    public Image cooldownBar;
    public Image ammoBar;
    private float initialCooldown;

    private float smgCooldownElapsed;
    private float flickerStart;
    private float flickerTime = .2f;
    private bool flicker;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //bulletObj = GameObject.Find("Bullet").GetComponent<BulletScript>();
        turretIcon.fillAmount = 1;

        shootMode = AbilityManager.AbilityManagerInstance.GetShootMode();
        pistolAltFire = AbilityManager.AbilityManagerInstance.GetAltFire(1);
        smgAltFire = AbilityManager.AbilityManagerInstance.GetAltFire(2);
        shotgunAltFire = AbilityManager.AbilityManagerInstance.GetAltFire(3);

        smgCooldownElapsed = -1;
        flickerStart = Time.time;
        flicker = false;
    }

    // Update is called once per frame
    void Update() 
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (rotZ < 89 && rotZ > -89)
        {
            bulletTransform.position = new Vector3(player.position.x + 0.221f, player.position.y - 0.322f, 0f);
        }
        else
        {
            // Debug.Log(mousePosition.x);
            bulletTransform.position = new Vector3(player.position.x - 0.221f, player.position.y - 0.322f, 0f);
        }
        /* //# Hasib
         * 
         * 
         if(transform.localScale.x == 1)
         {
             mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
             //mousePos.z = 5.23f;

             Vector3 rotation = mousePos - transform.position;

           *//*  float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
             transform.rotation = Quaternion.Euler(0, 0, rotz);*//*

             Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
             mousePos.x = mousePos.x - gunPos.x;
             mousePos.y = mousePos.y - gunPos.y;

             float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
             transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
         }
         else if (transform.localScale.x == -1)
         {
             mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
             //mousePos.z = 5.23f;

             Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
             mousePos.x = mousePos.x - gunPos.x;
             mousePos.y = mousePos.y - gunPos.y;

             float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
             transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
             }*/
        determineShootMode();
        checkToPlaceTurret();
        if (turretTimer > 0)
        {
            turretTimer -= Time.deltaTime;
            turretIcon.fillAmount = 1- (turretTimer / timeForNextTurret);
        }

        if(turretTimer <= 0)
        {
            turretIcon.fillAmount = 1;
        }

        if(smgCooldownElapsed >= 0)
        {
            smgCooldownElapsed += Time.deltaTime;
        }


        if (shootMode == 1 || shootMode == 3)
        {
            ammoBar.fillAmount = 0;
            cooldownBar.fillAmount = shootCooldown / gunCooldown;
            cooldownBar.color = new Color(0.58f, 0.99f, 1, 1);
        }
        else if (shootMode == 2)
        {
            ammoBar.fillAmount = (float)OPGunBullets / (float)OPGunBulletsLimit;
            Debug.Log(ammoBar.fillAmount);
        }
        if(shootMode == 1 || shootMode == 3)
        {
            ammoBar.fillAmount = 0;
            cooldownBar.fillAmount = shootCooldown / gunCooldown;
            cooldownBar.color = new Color(0.58f, 0.99f, 1, 1);
        }
        else if(shootMode == 2)
        {
            ammoBar.fillAmount = (float)OPGunBullets / (float)OPGunBulletsLimit;
            //Debug.Log(ammoBar.fillAmount);

            if(smgCooldownElapsed < 0)
            {
                cooldownBar.fillAmount = 1;
                cooldownBar.color = new Color(0.58f, 0.99f, 1, 1);
            }
            else
            {

                if(flicker == true && OPGunBullets <= 0)
                {
                    cooldownBar.fillAmount = 0;
                }
                else
                {
                    cooldownBar.fillAmount = smgCooldownElapsed / OPGunBulletsCoolDown;
                    float diff = (OPGunBulletsLimit - OPGunBullets) * .01f;
                    cooldownBar.color = new Color(.58f + diff * 3, .99f - diff * 3, 1 - diff * 3, 1);
                }


            }

        }

        if (Time.time >= (flickerStart + flickerTime))
        {
            flickerStart = Time.time;
            flicker = !flicker;
        }
    }

    void FixedUpdate()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //gun position fixin g

        //Vector3 rotation = mousePos - transform.position;

        //float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotz);

        ///# gun position fixin g
        if (shootCooldown != 0)
        {
            shootCooldown -= 1;
        }


        /*
        if(playedReadyNoise == false && OPGunBulletsCoolDown <= 0)
        {
            playedReadyNoise = true;
            SoundManager.SoundManagerInstance.PlayMiscSound(6);
        }
        */

            updateLevels();
        confirmShootMode();

        if ((Input.GetMouseButton(0) || Input.GetAxis("RightTrigger") > 0) && shootCooldown == 0 && PlayerController1.playerCanShoot == true)
        {
            shootCooldown = gunCooldown;
            shootGun();
            switch (shootMode)
            {
                case 1:
                    if (threeBurst && pistolAltFire == true) SoundManager.SoundManagerInstance.PlayFireSound(11);
                    else SoundManager.SoundManagerInstance.PlayFireSound(0);

                    if(threeBurst && pistolAltFire == true)
                    {
                        Invoke("shootGun", 0.10f);
                        Invoke("shootGun", 0.20f);
                    }

                    break;
                case 2:
                    if (OPGunBullets <= 6 && OPGunBullets > 0) SoundManager.SoundManagerInstance.PlayFireSound(7);
                    else if (OPGunBullets == 0) SoundManager.SoundManagerInstance.PlayFireSound(8);
                    else if(OPGunBullets > 6) SoundManager.SoundManagerInstance.PlayFireSound(5); //was 5
                    break;
                case 3:
                    SoundManager.SoundManagerInstance.PlayFireSound(6); // was 6
                    break;
                default:
                    SoundManager.SoundManagerInstance.PlayFireSound(0);
                    break;
            }

            
        }

        
    }


    //void allowTurret() {
    //    canPlaceTurret = true;
    //}
    void checkToPlaceTurret() {

        if(PlayerController1.zeke == false)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.T)||(Input.GetButtonDown("RBButton"))) && turretTimer <= 0) {
            Instantiate(friendlyTurretObj,bulletTransform.position, Quaternion.identity);
            //canPlaceTurret = false;
            turretTimer = timeForNextTurret;
            turretIcon.fillAmount = 0;
            //Invoke("allowTurret", timeForNextTurret);
        }
    }

    void updateLevels()
    {
        bool resetAmmo = false;
        pistolLevel = AbilityManager.AbilityManagerInstance.pistolLevel;
        rofLevelPistol = AbilityManager.AbilityManagerInstance.getUnlockLevel(1, 0);
        damageLevelPistol = AbilityManager.AbilityManagerInstance.getUnlockLevel(1, 1);
        threeBurst = AbilityManager.AbilityManagerInstance.getUnlockAbility(1, 0);
        stunGun = AbilityManager.AbilityManagerInstance.getUnlockAbility(1, 1);


        smgLevel = AbilityManager.AbilityManagerInstance.smgLevel;
        damageLevelSMG = AbilityManager.AbilityManagerInstance.getUnlockLevel(2, 0);
        capacityLevelSMG = AbilityManager.AbilityManagerInstance.getUnlockLevel(2, 1);
        ignoreResist = AbilityManager.AbilityManagerInstance.getUnlockAbility(2, 0);
        overcharge = AbilityManager.AbilityManagerInstance.getUnlockAbility(2, 1);


        if (OPGunBullets == OPGunBulletsLimit) resetAmmo = true;
        //if (smgLevel < 5) OPGunBulletsLimit = 20 + (5 * (smgLevel - 1));
        if (capacityLevelSMG < 5) OPGunBulletsLimit = 20 + (5 * (capacityLevelSMG));
        else OPGunBulletsLimit = 50;
        if (resetAmmo) OPGunBullets = OPGunBulletsLimit;

        shotgunLevel = AbilityManager.AbilityManagerInstance.shotgunLevel;
        projectileLevelShotgun = AbilityManager.AbilityManagerInstance.getUnlockLevel(3, 0);
        spreadLevelShotgun = AbilityManager.AbilityManagerInstance.getUnlockLevel(3, 1);
        grenadeLauncher = AbilityManager.AbilityManagerInstance.getUnlockAbility(3, 0);
        slamfire = AbilityManager.AbilityManagerInstance.getUnlockAbility(3, 1);

        aoeLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(4, 1); ;

        dashLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(5, 0); ;

        //turretLevel = AbilityManager.AbilityManagerInstance.turretLevel;
        turretLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(6, 0);

        orbsLevel = AbilityManager.AbilityManagerInstance.orbsLevel;
    }

    //void allowTurret() {
    //    canPlaceTurret = true;
    //}

    void determineShootMode() {

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("ButtonX"))
        {
            switch (shootMode)
            {
                case 1:
                    if (!threeBurst) break;
                    pistolAltFire = !pistolAltFire;
                    AbilityManager.AbilityManagerInstance.SetAltFire(1, pistolAltFire);
                    break;
                case 2:
                    if (!overcharge) break;
                    smgAltFire = !smgAltFire;
                    AbilityManager.AbilityManagerInstance.SetAltFire(2, smgAltFire);
                    break;
                case 3:
                    if (!grenadeLauncher) break;
                    shotgunAltFire = !shotgunAltFire;
                    AbilityManager.AbilityManagerInstance.SetAltFire(3, shotgunAltFire);
                    break;
                default:
                    break;
            }
            confirmShootMode();
        }

        if (Input.GetButtonDown("Cycle"))
        {
            shootMode++;
            if (shootMode > 3 || !PlayerController1.freya) shootMode = 1;

            switch (shootMode)
            {
                case 1:
                    if (!threeBurst || (threeBurst && pistolAltFire == false)) gunCooldown = 30 - (5 * (rofLevelPistol - 1));
                    else gunCooldown = 25 - (5 * (rofLevelPistol - 4));
                    shootingDamage = 2 + (damageLevelPistol * .5f);
                    bulletTime = 2;
                    break;
                case 2:
                    gunCooldown = 2;
                    shootingDamage = 0.5f + ((damageLevelSMG - 1) * .15f);
                    bulletTime = .9f;

                    if(smgAltFire == true && overcharge)
                    {
                        gunCooldown = 1.0f;
                        shootingDamage = 2f + ((damageLevelSMG - 1) * .30f);
                    }
                    break;
                case 3:
                    gunCooldown = 30;
                    if (slamfire) gunCooldown = 15;
                    shootingDamage = 1.5f;
                    bulletTime = .65f;

                    if (shotgunAltFire == true) gunCooldown = 90;
                    break;
                default:
                    if (!threeBurst || (threeBurst && pistolAltFire == false)) gunCooldown = 30 - (5 * (rofLevelPistol - 1));
                    else gunCooldown = 25 - (5 * (rofLevelPistol - 4));
                    shootingDamage = 2 + (damageLevelPistol * .5f);
                    bulletTime = 2;
                    break;
            }

            AbilityManager.AbilityManagerInstance.SetShootMode(shootMode);
        }

        // Slower gun
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            shootMode = 1;
            if (!threeBurst || (threeBurst && pistolAltFire == false)) gunCooldown = 30 - (5 * (rofLevelPistol - 1));           //was 20
            //else gunCooldown = 20;
            else gunCooldown = 25 - (5 * (rofLevelPistol - 4));
            //shootingDamage = 2 + ((damageLevelPistol-1) * 2);
            shootingDamage = 2 + (damageLevelPistol * .5f);
            bulletTime = 2;
            //bulletObj.changeDamage(shootMode);
        }

        // Faster Gun
        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerController1.freya) {
            shootMode = 2;
            gunCooldown = 2;
            shootingDamage = 0.5f + ((damageLevelSMG-1) * .15f);
            bulletTime = .9f;

            if(smgAltFire == true && overcharge)
            {
                gunCooldown = 1.0f;
                shootingDamage = 2f + ((damageLevelSMG-1) * .30f);
            }

            //bulletObj.changeDamage(shootMode);
        }

        // Shotgun (Does nothing)
        if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerController1.freya) {
            shootMode = 3;
            gunCooldown = 30;
            if (slamfire) gunCooldown = 15;
            shootingDamage = 1.5f;
            bulletTime = .65f;

            if (shotgunAltFire == true) gunCooldown = 90;

        }

        AbilityManager.AbilityManagerInstance.SetShootMode(shootMode);
    }

    void confirmShootMode()
    {

        switch (shootMode)
        {
            case 1:
                if (!threeBurst || (threeBurst && pistolAltFire == false)) gunCooldown = 30 - (5 * (rofLevelPistol - 1));           //was 20
                //else gunCooldown = 20;
                else gunCooldown = 25 - (5 * (rofLevelPistol - 4));

                //shootingDamage = 2 + ((damageLevelPistol - 1) * 2);
                shootingDamage = 2 + (damageLevelPistol*.5f);
                bulletTime = 2;
                break;
            case 2:
                if (PlayerController1.freya)
                {
                    shootMode = 2;
                    gunCooldown = 2;
                    shootingDamage = 0.5f + ((damageLevelSMG-1)*.15f);
                    bulletTime = .9f;

                    //Debug.Log("Ammo capacity = " + OPGunBulletsLimit);

                    if (smgAltFire == true && overcharge)
                    {
                        gunCooldown = 1f;
                        shootingDamage = 1.5f + ((damageLevelSMG-1) * .15f);
                    }

                }
                
                break;
            case 3:
                if (PlayerController1.freya)
                {
                    shootMode = 3;
                    gunCooldown = 30;
                    if (slamfire) gunCooldown = 15;
                    shootingDamage = 1.5f;
                    bulletTime = .75f;
                }

                if (shotgunAltFire == true) gunCooldown = 90;
                break;
        }

    }

    public static void adjustShotgunVecVal() 
    {
        if (shootMode == 3 && shotgunAltFire == false) {

            float shotgunVecRatio;

            switch (projectileLevelShotgun)
            {
                case 1:
                    shotgunVecRatio = 1f;
                    break;
                case 2:
                    shotgunVecRatio = 0.5f;
                    break;
                case 3:
                    shotgunVecRatio = 0.33f;
                    break;
                case 4:
                    shotgunVecRatio = 0.25f;
                    break;
                case 5:
                    shotgunVecRatio = 0.25f;
                    break;
                default:
                    shotgunVecRatio = 1f;
                    break;
            }
            shotgunVecRatio /= (1.0f + (0.5f * (spreadLevelShotgun-1)));


            if(shotgunVecVal < 1f - (0.33f * (spreadLevelShotgun - 1)))
            {
                shotgunVecVal += shotgunVecRatio;
            }
            else
            {
                shotgunVecVal = -1f + (0.33f * (spreadLevelShotgun - 1));
            }

            
        } else {
            shotgunVecVal = 0;
        }
    }

    void shootGun() {
        if (PlayerController1.playerCanShoot == false) return;

        if (shootMode != 2 || (shootMode == 2 && OPGunBullets > 0)) {
            if (!(shootMode == 3 && shotgunAltFire == true))
            {
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                //Debug.Log("Shooting pistol at level " + pistolLevel + " or smg at level " + smgLevel);
            }
        }
        if (shootMode == 2) {
            if (overcharge && smgAltFire == true) OPGunBullets -= 2;
            else if(OPGunBullets > -1) OPGunBullets -= 1;

            if (OPGunBullets == (OPGunBulletsLimit-1) || (OPGunBullets == (OPGunBulletsLimit-2) && overcharge && smgAltFire == true)) {
                Debug.Log("Out of ammo, capacity was: " + OPGunBulletsLimit);
                Invoke("resetOPGunBullets", OPGunBulletsCoolDown);
                smgCooldownElapsed = 0f;
            }
        }
        if (shootMode == 3) {
            Debug.Log("Out of ammo, capacity was:");
            if (shotgunAltFire == false)
            {
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
               
                Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                for (int i = 1; i < projectileLevelShotgun; i++)
                {
                    Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                }


              /*  player = GameObject.FindGameObjectWithTag("Player").transform;
                if (rotZ < 89 && rotZ > -89)
                {



                    bulletTransform.position = new Vector3(player.position.x + 0.221f, player.position.y - 0.322f, 0f);
                    Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                }
                else
                {
                    // Debug.Log(mousePosition.x);


                    bulletTransform.position = new Vector3(player.position.x - 0.221f, player.position.y - 0.322f, 0f);
                    Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                }*/
                //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                /*  Vector3 rotation = mousePos - transform.position;

                  float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                  transform.rotation = Quaternion.Euler(0, 0, rotz);
                  for (int i = 1; i < shotgunLevel; i++)
                  {
                      Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                  }*/

                //Debug.Log("Shooting shotgun at level " + projectileLevelShotgun);


            }
            else
            {
                Instantiate(grenade, bulletTransform.position, Quaternion.identity);
            }

        }
    }

    void resetOPGunBullets() {
        Debug.Log("Gun reloaded. Capacity: " + OPGunBulletsLimit);
        OPGunBullets = OPGunBulletsLimit;
        SoundManager.SoundManagerInstance.PlayMiscSound(6);

        smgCooldownElapsed = -1f;
    }
}
