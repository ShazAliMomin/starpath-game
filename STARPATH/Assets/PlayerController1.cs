using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
using System.Threading;
using System.Threading.Tasks;

public class PlayerController1 : MonoBehaviour
{
    private Collider2D others;
    Rigidbody2D rb;
    public static bool zeke = false;
    public static bool freya = false;
    public static bool norman = false;
    public static bool janna = false;
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public InventoryObject inventory;
    [SerializeField] private GameObject itextgo;
    [SerializeField] private TextMeshProUGUI itext;
    public static int fuel = 0;
    public static int slimes = 0;
    public static int shint = 0;

    private SwitchWeaponSprite switchWeaponSprite;

    string filePath;

    public AreaOfEffectSkill aoe;
    public Image dashIcon;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;

    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    [SerializeField] private DeathMenu deathMenuUI;

    //[SerializeField] private UnityEngine.Rendering.Universal.Light2D aoeLight;
    [SerializeField] private GameObject aoeLight;
    [SerializeField] private GameObject aoeLightStun;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject aoeStunObject;
    [SerializeField] private GameObject controllerReticle;
    private bool controllerActive = false;

    private bool upgradeUIActive = false;

    private bool dashKeyPressed;
    private float dashCoolDown = 2f;
    public float dashTimer;

    public Vector3 mousePosition;

    bool canMove = true;
    private bool dead;

    public MeleeAttack meleeAttack;
    private MeleeAttack meleeDamage;

    //public int maxHealth = 10;

    public int maxHealth = 15;
    public Image healthBar;
    private float immunity = 1.5f;
    private float canDamage = 3f;

    public Image xpBar;
    public float currentXP = 0f;
    private float xpToLevelUp = 100f;
    public int currentLevel = 1;
    public int currentSkillPoints = 0;
    public static bool ts1 = false;
    public ItemObject scrap;
    public ItemObject zekei;
    public static int scraps = 0;
    bool sir = false;
    public string currentScene = "";
    public static bool playerCanShoot = true;
    private List<int> poisonTick = new List<int>();
    public static string[] starr = new string[10];
    bool zekealert = false;


    private SpriteRenderer blaster;

    [SerializeField] private AudioClip dashSound, laserSound, meleeSound;

    public float health;

    public int pistolLevel, smgLevel, shotgunLevel,shootMode;
    private int dashLevel;

    public int playerLevel;
    public TMP_Text playerLevelCounter;
    public Image playerLevelUpgradeAvailable;
    public TMP_Text pressTabPrompt;
    
    private bool tabbed;
    private bool isCont = false;
    //private SaveData saveData;
    

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/gameData.json";
        starr[0] = "Norman: Check the desks to find a keycard to use the elevator";
        starr[1] = "Norman: You'll need to flip the right switches to turn all those shield gates red";
        starr[2] = "Norman: This switch will definitely need to be flipped. Not sure about the rest";
        starr[3] = "Norman: Destroy those generators to open the sheilds!";
        starr[4] = "Press 'R' to trigger Janna's air strike. You can use this TWICE";
        //isCont = AbilityManager.AbilityManagerInstance.getGameContinuationStatus();
        //AbilityManager.AbilityManagerInstance.setGameContinuationStatus(isCont);
        shootMode = AbilityManager.AbilityManagerInstance.GetShootMode();
        //AbilityManager.AbilityManagerInstance.GetAltFire(shootMode);
        AbilityManager.AbilityManagerInstance.SetShootMode(shootMode);
        itextgo.SetActive(false);
        dead = false;

        tabbed = false;
        
        //health = maxHealth;
        health = AbilityManager.AbilityManagerInstance.GetHealth();
        healthBar.fillAmount = health / maxHealth;
        maxHealth = AbilityManager.AbilityManagerInstance.GetMaxHealth();

        currentXP = AbilityManager.AbilityManagerInstance.GetXP();
        xpBar.fillAmount = currentXP / xpToLevelUp;
        playerLevel = AbilityManager.AbilityManagerInstance.GetPlayerLevel();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isDead", false);
       
        blaster = GameObject.Find("BulletTransform").GetComponent<SpriteRenderer>();
        
        meleeDamage = GameObject.Find("MeleeHitbox").GetComponent<MeleeAttack>();

        dashIcon.fillAmount = 1;

        //dashLevel = AbilityManager.AbilityManagerInstance.dashLevel;
        dashLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(5, 0);
        dashCoolDown = 2f - (float)(0.45 * (dashLevel + 0));

        Scene currentScene = SceneManager.GetActiveScene();
        SoundManager.SoundManagerInstance.ResetSettings();
        SoundManager.SoundManagerInstance.CheckScene(true);
        //if (currentScene.name == "Town") SoundManager.SoundManagerInstance.ChangeMusicTrack(0);
        //else if (currentScene.name == "Tavern") SoundManager.SoundManagerInstance.ChangeMusicTrack(2);

        print(SceneManager.GetActiveScene().name);

      
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            //transform.localPosition = new Vector3(0, 0, 0);
            //print(transform.position);
            print("Hi");
            Vector3 newPos = new Vector3(-2.62f, -0.99f, 0);
            rb.position = newPos;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            health = maxHealth;
        }

        //isCont = AbilityManager.AbilityManagerInstance.getGameContinuationStatus();
        //AbilityManager.AbilityManagerInstance.setGameContinuationStatus(isCont);
        shootMode = AbilityManager.AbilityManagerInstance.GetShootMode();
        //AbilityManager.AbilityManagerInstance.GetAltFire(shootMode);
        AbilityManager.AbilityManagerInstance.SetShootMode(shootMode);


        AbilityManager.AbilityManagerInstance.SetController(Input.GetJoystickNames().Length > 0);
        if(AbilityManager.AbilityManagerInstance.GetController() == true)
        {
            controllerActive = true;
        }
        else
        {
            controllerActive = false;
        }

        //Debug.Log("Right stick X: " + Input.GetAxis("RightStickX"));
        //Debug.Log("Right stick Y: " + Input.GetAxis("RightStickY"));

        healthBar.fillAmount = health / maxHealth;
        freya = AbilityManager.AbilityManagerInstance.getFreyaStatus();
        norman = AbilityManager.AbilityManagerInstance.getNormanStatus();
        zeke = AbilityManager.AbilityManagerInstance.getZekeStatus();

        playerLevel = AbilityManager.AbilityManagerInstance.GetPlayerLevel();
        playerLevelCounter.text = playerLevel.ToString();

        if (AbilityManager.AbilityManagerInstance.GetUpgradePointsAvailable() > 0) { playerLevelUpgradeAvailable.color = new Color(.99f, .98f, .38f, .35f); pressTabPrompt.text = "Press TAB to level up."; }
        else { playerLevelUpgradeAvailable.color = new Color(0, 0, 0, 0); pressTabPrompt.text = ""; }

        AbilityManager.AbilityManagerInstance.SetXP(currentXP);
    
        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("zekeack")).value)
        {

            zeke = true;
            if (!zekealert)
            {
                itext.text = "Press 'T' Button to try out Zeke as a turret";
                StartCoroutine(dispinv2());
                zekealert = true;
            }

            if (AbilityManager.AbilityManagerInstance.questLevelUp(2) == true) addXP(100f);
        }

        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("dockdone")).value)
        {

            freya = true;
            if (AbilityManager.AbilityManagerInstance.questLevelUp(0) == true) addXP(100f);
        }




        if (slimes == 15)
        {
            bool b = true;
            Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(b);
            // call the DialogueManager to set the variable in the globals dictionary
            DialogueManager.GetInstance().SetVariableState("slimek", obj);
        }

        if(sir)
        {

          

            var item = others.GetComponent<searchObject>();

            

            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ButtonA"))
            {

                


                SoundManager.SoundManagerInstance.PlayMiscSound(0);
                if (others.CompareTag("Search"))
                {



                    if (item)
                    {

                        if (item.item.Id != 4)
                        {
                           
                            inventory.AddItem(new Item(item.item), 1);
                            itext.text = "Acquired ";
                            itext.text += item.item.name;
                            if (item.item.name == "Scrap")
                                {
                                scraps += 1;
                            }
                            StartCoroutine(dispinv());

                        }
                        else
                        {
                            itext.text = "Nothing Here";
                            StartCoroutine(dispinv());
                        }
                        //other.gameObject.SetActive(false);
                        others.gameObject.GetComponent<searchObject>().used = true;
                    }

                }


            }

            
            

        }

        rb.AddForce(Vector2.zero);

        if (scraps >= 6)
        {
            // convert the variable into a Ink.Runtime.Object value
            bool b = true;
            Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(b);
            // call the DialogueManager to set the variable in the globals dictionary
            DialogueManager.GetInstance().SetVariableState("scrap", obj);

            itext.text = "All Scraps Collected";
            StartCoroutine(dispinv());
            scraps = 0;

        }

        if (!zeke)
        {
            if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("zekeack")).value)
            {
                zeke = true;

                /*
                SoundManager.SoundManagerInstance.PlayMiscSound(2);
                inventory.AddItem(new Item(zekei), 1);
                itext.text = "Acquired ";
                itext.text += zekei.name;
                StartCoroutine(dispinv());*/
                
            }

        }

        if (!norman)
        {
            if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("norhelp")).value)
            {
                norman = true;
                if (AbilityManager.AbilityManagerInstance.questLevelUp(1) == true) addXP(100f); ;
            }

        }


        if (!ts1)
        {
            if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("ts1")).value)
            {
                ts1 = true;


                SoundManager.SoundManagerInstance.PlayMiscSound(2);
                inventory.AddItem(new Item(scrap), 1);
                itext.text = "Acquired ";
                itext.text += scrap.name;
                StartCoroutine(dispinv());
                scraps += 1;



            }

        }




        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("LeftTrigger") < 0)&& dashTimer <= 0)
        {
            dashKeyPressed = true;
        }

        if (canDamage < immunity)
        {
            canDamage += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("LBButton"))
        {
            //Debug.Log("skillUsable value: " + aoe.skillUsable);
            //if(aoe.skillUsable <= 0)
            {
                SoundManager.SoundManagerInstance.PlayFireSound(9);
                animator.SetTrigger("aoeAttack");
                UnityEngine.Rendering.Universal.Light2D intens = aoeLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
                intens.intensity = 3;
                //default light radius = 1.2, 2.2)
                intens.pointLightInnerRadius = 1.2f + ((AbilityManager.AbilityManagerInstance.getUnlockLevel(4,0)-1) * .75f);
                intens.pointLightOuterRadius = 2.2f + ((AbilityManager.AbilityManagerInstance.getUnlockLevel(4, 0)-1) * .75f);
                aoeLight.SetActive(true);
                //if(AbilityManager.AbilityManagerInstance.GetAbility(4) == 5)
                if (AbilityManager.AbilityManagerInstance.getUnlockAbility(4, 0) == true) 
                {
                    Debug.Log("placed aoeStunObject at: " + transform.position);
                    UnityEngine.Rendering.Universal.Light2D intens2 = aoeLightStun.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
                    intens2.intensity = 1.0f;
                    intens2.pointLightInnerRadius = 8.0f;
                    intens2.pointLightOuterRadius = 12.0f;
                    aoeLightStun.SetActive(true);
                    GameObject stunner = Instantiate(aoeStunObject, transform.position, Quaternion.identity) as GameObject;
                    Collider2D collider = stunner.GetComponent<CircleCollider2D>();
                    collider.enabled = false;
                    collider.enabled = true;
                }
            }
            
            
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("SelectButton"))
        {
            if(upgradeUIActive == false)
            {


                upgradeUIActive = true;
                upgradeUI.SetActive(true);
                UpgradeControllerNew ui = upgradeUI.GetComponent<UpgradeControllerNew>();

                ui.allButtonsUpdate();

                //ui.SetLevelText();
                tabbed = true;
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                upgradeUIActive = false;
                upgradeUI.SetActive(false);
            }
        }

        if(zeke == true)
        {
            AbilityManager.AbilityManagerInstance.unlockTurret();
        }
        if(freya == true)
        {
            AbilityManager.AbilityManagerInstance.unlockWeapons();
        }

        //Unlock everything cheat -- remove later
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("All abilities unlocked.");
            freya = true;
            zeke = true;
            norman = true;
            AbilityManager.AbilityManagerInstance.setFreyaStatus(freya);
            AbilityManager.AbilityManagerInstance.setNormanStatus(freya);
            AbilityManager.AbilityManagerInstance.setZekeStatus(freya);
            Scene currentScene = SceneManager.GetActiveScene();
            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                level3manager.strikes = 2;
            }

        }

        //Check persistent values -- remove later
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Health = " + health + " XP = " + currentXP);
            Debug.Log("AMHealth = " + AbilityManager.AbilityManagerInstance.GetHealth() + "   AM XP: " + AbilityManager.AbilityManagerInstance.GetXP());
            Debug.Log("HealthBar.fillamount: " + healthBar.fillAmount + "Max health: " + maxHealth);
        }

        //Gain one level cheat -- remove later
        if (Input.GetKeyDown(KeyCode.L))
        {
            AbilityManager.AbilityManagerInstance.GainLevel();
        }

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            dashIcon.fillAmount = 1- (dashTimer / dashCoolDown);
        }

        if(dashTimer <= 0)
        {
            //dashLevel = AbilityManager.AbilityManagerInstance.dashLevel;
            dashLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(5,0);
            dashCoolDown = 2f - (float)(0.45 * (dashLevel + 0));
            dashIcon.fillAmount = 1;
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    rangedWeapon.Fire();
        //    laserSoundEffect.Play();
        //}

        /* if(srange)
         {
             if (Input.GetKeyDown(KeyCode.I))
             {

                 SceneManager.LoadScene("Town");
                 if (item)
                 {

                     inventory.AddItem(new Item(item.item), 1);
                 }

             }

         }*/

        if (zeke == true && janna == false)
        {
            inventory.RemoveItemID(5);
            janna = true;
            if (AbilityManager.AbilityManagerInstance.questLevelUp(3) == true) addXP(100f); 
        }
        
    }




    public static Task statime = Task.Run(async delegate {
        await Task.Delay(6000);
    });


    public void slimekill()
    {
        slimes += 1;
        itext.text = slimes + "/15 slimes killed";
        StartCoroutine(dispinv());

    }

    IEnumerator dispinv()
    {
     itextgo.SetActive(true);
        yield return new WaitForSeconds(3f);
     itextgo.SetActive(false);
        itext.text = "";

    }

    IEnumerator dispinv2()
    {
        itextgo.SetActive(true);

        yield return new WaitForSeconds(6f);
        itextgo.SetActive(false);
        itext.text = "";

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);
    }




    private void FixedUpdate()
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        if (rb != null)
        {
            if (canMove)
            {
                //if (dashCoolDown != 0)
                //{
                //    dashCoolDown -= Time.deltaTime;
                //}

                if (movementInput != Vector2.zero)
                {
                    bool success = TryMove(movementInput);

                    if (!success)
                    {
                        success = TryMove(new Vector2(movementInput.x, 0));
                    }

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }

                    animator.SetBool("isMoving", success);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }

            flipPlayer();
        }
    }

    private void flipPlayer()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        if (controllerActive == false)
        {
            if (movementInput.x < 0 && mousePosition.x < 0)
            {
                meleeDamage.SwitchDirection(0);
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0 && mousePosition.x > 0)
            {
                meleeDamage.SwitchDirection(1);
                spriteRenderer.flipX = false;
            }

            if (mousePosition.x < rb.position.x)
            {
                meleeDamage.SwitchDirection(0);
                spriteRenderer.flipX = true;

            }
            else if (mousePosition.x > rb.position.x)
            {
                meleeDamage.SwitchDirection(1);
                spriteRenderer.flipX = false;

            }
        }
        else
        {
            if(movementInput.x < 0)
            {
                meleeDamage.SwitchDirection(0);
                spriteRenderer.flipX = true;
            }
            else
            {
                meleeDamage.SwitchDirection(1);
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            //if (count == 0 && dashKeyPressed && dashTimer <= 0)
            if (dashKeyPressed && dashTimer <= 0)
            {
                // dashSoundEffect.Play();
                animator.SetTrigger("isDashing");
                SoundManager.SoundManagerInstance.PlayDashSound(dashSound);
                rb.MovePosition(rb.position + direction * moveSpeed * dashSpeed * Time.fixedDeltaTime);
                dashKeyPressed = false;

                //dashLevel = AbilityManager.AbilityManagerInstance.dashLevel;
                dashLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(5, 0);
                dashCoolDown = 2f - (float)(0.45 * (dashLevel + 0));

                dashTimer = dashCoolDown;
                dashIcon.fillAmount = 0;
                return true;
            }
            //else if (count == 0)
            //{
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
           // }
            //else
            //{
            //   return false;
            //}
        }
        else return false;


    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnMelee()
    {
        animator.SetTrigger("meleeAttack");
    }

    public void MeleeAttack()
    {
        //LockMovement();
        blaster.enabled = false;
        playerCanShoot = false;

        if (spriteRenderer.flipX == true)
        {
            meleeAttack.MeleeLeft();
        }
        else
        {
            meleeAttack.MeleeRight();
        }
    }

    public void EndMeleeAttack()
    {
        //UnlockMovement();
        blaster.enabled = true;
        meleeAttack.StopMelee();
        playerCanShoot = true;
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void addXP(float xp)
    {
        currentXP += xp;
        if(currentXP >= xpToLevelUp)
        {
            levelUP();
            AbilityManager.AbilityManagerInstance.GainLevel();
        }
        xpBar.fillAmount = currentXP / xpToLevelUp;
        AbilityManager.AbilityManagerInstance.SetXP(currentXP);
    }

    public void levelUP()
    {
        currentLevel += 1;
        currentSkillPoints += 1;
        currentXP = 0;
        xpBar.fillAmount = 1;

        maxHealth = 15;
        health = maxHealth;
        healthBar.fillAmount = 1;
        //meleeDamage.damage += 2;

        print(currentLevel);

        AbilityManager.AbilityManagerInstance.SetHealth(health);
        AbilityManager.AbilityManagerInstance.SetXP(currentXP);
        SaveGameState();

    }

    public void superLevelUP(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AbilityManager.AbilityManagerInstance.GainLevel();
        }

        currentLevel += amount;
        currentSkillPoints += amount;
        currentXP = 0;
        xpBar.fillAmount = 1;

        maxHealth = 15;
        health = maxHealth;
        healthBar.fillAmount = 1;

        AbilityManager.AbilityManagerInstance.SetHealth(health);
        AbilityManager.AbilityManagerInstance.SetXP(currentXP);
    }

    public void TakeDamage(int damage)
    {
        if (canDamage >= immunity)
        {
            if (health > 0) { SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(0, 8)); }
            animator.SetBool("isHit", true);

            health = health - damage;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0 && !dead)
            {
                SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(0, 4));
                // Destroy(gameObject);
                animator.SetTrigger("isDead");
                dead = true;
                Time.timeScale = 0f;
                Thread.Sleep(1500); // in milisecond 
                Time.timeScale = 1f;
               
              
                Debug.Log("Menu called2");
                AbilityManager.AbilityManagerInstance.SetHealth(maxHealth);
                SaveGameState();
                Debug.Log("Hit");
                print("Hit");            
                deathMenuUI.ActivateDeathMenu();
                
            }
            else
            {
                AbilityManager.AbilityManagerInstance.SetHealth(health);
            }
            canDamage = 0f;
        }
       
    }

    public void gainHealth(int amount)
    {
        if (health >= maxHealth) return;
        
        health += amount;
        AbilityManager.AbilityManagerInstance.SetHealth(health);
        healthBar.fillAmount = health / maxHealth;
    }

    public void callDeathMenu()
    {
        Debug.Log("Hit");
        //AbilityManager.AbilityManagerInstance.Save();
        deathMenuUI.ActivateDeathMenu();
        
    
    }

    public void StopDamage()
    {
        animator.SetBool("isHit", false);
    }

    public void Restart()
    {
        SaveGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }

    public void Defeated()
    {
        //SoundManager.SoundManagerInstance.PlayDeathSound((int)Random.Range(0, 4));
      //  Destroy(gameObject);


    }

    //private bool srange = false;

    public void OnTriggerExit2D(Collider2D other)
    {
        sir = false;
    }



        public void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("hint") && norman)
        {

            itext.text = starr[other.GetComponent<hint1>().id];

            StartCoroutine(dispinv2());


        }

        if (other.CompareTag("Search"))
        {
            others = other;
            sir = true;

            if (shint < 2)
            {
                itext.text = "Press 'R' to inspect/search objects";

                StartCoroutine(dispinv());
                ++shint;
            }
        }
        else
        {

            var item = other.GetComponent<GroundItem>();
            if (item)
            {
                SoundManager.SoundManagerInstance.PlayMiscSound(2);
                if (item.item.Id == 2)
                {
                    if (health == 9)
                    {
                        health += 1;
                        healthBar.fillAmount = health / maxHealth;
                    }
                    else if (health == 10)
                        inventory.AddItem(new Item(item.item), 1);
                    else
                    {
                        health += 2;
                        healthBar.fillAmount = health / maxHealth;
                    }

                    Destroy(other.gameObject);
                    AbilityManager.AbilityManagerInstance.SetHealth(health);
                }
                else
                {

                    inventory.AddItem(new Item(item.item), 1);
                    Destroy(other.gameObject);
                    if (item.item.Id == 6)
                    {
                        fuel += 1;
                        itext.text = fuel.ToString() + "/10 Fuel Acquired";

                    }
                    else
                    {
                        if (item.item.name == "Scrap")
                        {
                            
                            scraps += 1;
                            itext.text += scraps + "/6";
                        }
                        itext.text = "Acquired ";
                        itext.text += item.item.name;
                        hubmanager.pbarr[other.gameObject.GetComponent<GroundItem>().id] = true;
                        other.gameObject.GetComponent<GroundItem>().used = true;
                    }
                    Destroy(other.gameObject);
                    StartCoroutine(dispinv());

                    Debug.Log("Item Picked Up!");
                }

            }
        }
      
    }

    IEnumerator Burn()
    {
        while(poisonTick.Count > 0)
        {
            for (int i = 0; i < poisonTick.Count; i++)
            {
                poisonTick[i]--;
            }
            if (health > 0) { SoundManager.SoundManagerInstance.PlayInjurySound((int)Random.Range(0, 8)); }
            animator.SetBool("isHit", true);
            health -= 1;
            poisonTick.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }

    public void ApplyBurn(int ticks)
    {
        if(poisonTick.Count<=0)
        {
            poisonTick.Add(ticks);
            StartCoroutine(Burn());
        }
        else
        {
            poisonTick.Add(ticks);
        }
    }





    public void ActivateAOECollider()
    {
        aoe.ActivateAOECollider();
    }

    public void DeactivateAOECollider()
    {
        aoe.DeactivateAOECollider();
    }


    public void OnApplicationQuit()
    {
        // Reset inventory on quit
        Debug.Log("Item Picked Up!");
        SaveGameState();
        inventory.Container.Items = new InventorySlot[12];
    }
    //public void SaveGameState()
    //{
    //    Debug.Log("Item Picked Up 5!");
    //    AbilityManager.AbilityManagerInstance.Save();
    //    filePath = "../gameSaveData0.json";
    //    // create a new dictionary to store the data
    //    Dictionary<string, object> data = new Dictionary<string, object>();
    //    Debug.Log("Saveing ....");
    //    // store the ability levels
    //    data["pistolLevel"] = pistolLevel;
    //    data["smgLevel"] = smgLevel;
    //    data["shotgunLevel"] = shotgunLevel;
    //    data["dashLevel"] = dashLevel;

    //    // store the upgrade points available and player level

    //    data["playerLevel"] = playerLevel;

    //    // store the player shoot mode, alt fire options, health, and experience
    //    data["playerShootMode"] = 1;
    //    data["playerPistolAltFire"] = 2;
    //    data["playerSMGAltFire"] = 2;
    //    data["playerShotgunAltFire"] = 0;
    //    data["playerHealth"] = health;
    //    data["playerMaxHealth"] = maxHealth;
    //    data["playerXP"] = currentXP;
    //    data["playerMaxXP"] = 5;

    //    // serialize the data to JSON
    //    string json = JsonUtility.ToJson(data);

    //    // write the JSON to the file
    //    System.IO.File.WriteAllText(filePath, json);


    //}
    public void SaveGameState()
    {
        try
        {
            Debug.Log("Item Picked Up 5!");

            // Call the Save method on an AbilityManager instance to save the ability levels.
            AbilityManager.AbilityManagerInstance.Save();

            // Set the file path for the JSON file to be saved.
            string filePath = "../gameSaveData0.json";

            // Create a new dictionary to store the game data.
            SaveDataP data = new SaveDataP();

            // Store the levels of four abilities (pistol, SMG, shotgun, and dash) into the dictionary.
            //data.pistolLevel = pistolLevel;
            //data.pistolLevel = pistolLevel;
            //data.smgLevel = smgLevel;
            //data.shotgunLevel = shotgunLevel;
            //data.dashLevel = dashLevel;
            data.playerLevel = playerLevel;
            data.playerShootMode = 1;
            data.playerPistolAltFire = 2;
            data.playerSMGAltFire = 2;
            data.playerShotgunAltFire = 0;
            data.playerHealth = 3;
            data.maxHealth = 2;
            data.currentXP = 3;
            data.maxXP = 5;

            // Serialize the dictionary into a JSON string using JsonUtility.ToJson method.
            string json = JsonUtility.ToJson(data);

            // Write the JSON to the file using System.IO.File.WriteAllText method.
            System.IO.File.WriteAllText(filePath, json);

            // Check if the file exists and has data.
            if (System.IO.File.Exists(filePath) && new System.IO.FileInfo(filePath).Length > 0)
            {
                Debug.Log("Data saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save data. The file is empty or does not exist.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving data: " + e.Message);
        }
    }
}

[System.Serializable]
public class SaveDataP
{
    public int pistolLevel;
    public int smgLevel;
    public int shotgunLevel;
    public int dashLevel;
    public int playerLevel;
    public int playerShootMode;
    public int playerPistolAltFire;
    public int playerSMGAltFire;
    public int playerShotgunAltFire;
    public int playerHealth;
    public int maxHealth;
    public int currentXP;
    public int maxXP;
}

