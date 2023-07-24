using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaOfEffectSkill : MonoBehaviour
{
    public Image skillIcon;
    public CircleCollider2D areaCircle;
    //public CircleCollider2D stunAreaCircle;
    private float aoedamage = 10f;
    //private float damage = 2f;
    private float stunTime = 90f;

    public float skillCooldown = 15f;
    public float skillUsable;

    private bool aoeInUse;

    private int aoeLevel;

    // Start is called before the first frame update
    void Start()
    {
        aoeInUse = false;
        //aoeLevel = AbilityManager.AbilityManagerInstance.aoeLevel;
        aoeLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(4, 1);

        areaCircle.enabled = false;
        //stunAreaCircle.enabled = false;
        skillIcon.fillAmount = 1;

        skillCooldown = 12f - (3 * (aoeLevel - 1));
        if (aoeLevel == 5) skillCooldown = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //aoeLevel = AbilityManager.AbilityManagerInstance.aoeLevel;
        aoeLevel = AbilityManager.AbilityManagerInstance.getUnlockLevel(4, 1);

        if (skillUsable > 0)
        {
            skillUsable -= Time.deltaTime;
            skillIcon.fillAmount = 1- (skillUsable / skillCooldown);
        }

        if(skillUsable <= 0)
        {
            skillIcon.fillAmount = 1;
        }

        if (Input.GetKeyDown(KeyCode.X)) //&& skillUsable <= 0)
        {
            skillCooldown = 12f - (3 * (aoeLevel - 1));
            if (aoeLevel == 5) skillCooldown = 3f;

            skillUsable = skillCooldown;
            skillIcon.fillAmount = 0;
        }

        
    }


    public void ActivateAOECollider()
    {
        areaCircle.enabled = true;
        aoeInUse = true;

        //default radius = 1.5
        areaCircle.radius = 1.5f + ((AbilityManager.AbilityManagerInstance.getUnlockLevel(4, 0)-1) * .75f);

        //if (aoeLevel == 5) stunAreaCircle.enabled = true;
    }

    public void DeactivateAOECollider()
    {
        areaCircle.enabled = false;
        aoeInUse = false;
        //stunAreaCircle.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (aoeInUse == false) return;

         if (other.tag == "Enemy")
         {
             MeleeEnemy enemy = other.GetComponent<MeleeEnemy>();

             if (enemy != null)
             {
                 SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(0, 2));

                 enemy.health -= aoedamage;
                 enemy.animator.SetBool("isHit", true);
                 if (enemy.health > 0)
                 {
                     enemy.TakeDamage();
                 }

             }
         }
        else if (other.gameObject.tag == "BlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                blastEnemy.health -= aoedamage;

                if (blastEnemy.health == 0)
                {

                }
                else
                {
                    blastEnemy.TakeDamage();
                }

                //if(aoeLevel == 5)
                if(AbilityManager.AbilityManagerInstance.getUnlockAbility(4,0) == true)
                {
                    Debug.Log("Stun");
                    blastEnemy.Stun(stunTime);
                }
                else { Debug.Log("no stun"); }
            }
           
        }

        else if (other.gameObject.tag == "ShieldedBlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                blastEnemy.health -= aoedamage;

                if (blastEnemy.health == 0)
                {

                }
                else
                {
                    blastEnemy.TakeDamage();
                }

                //if(aoeLevel == 5)
                if (AbilityManager.AbilityManagerInstance.getUnlockAbility(4, 0) == true)
                {
                    Debug.Log("Stun");
                    blastEnemy.Stun(stunTime);
                }
                else { Debug.Log("no stun"); }
            }

        }

        else if (other.gameObject.tag == "Slime")
        {
            SlimeScript slime = other.gameObject.GetComponent<SlimeScript>();
            if (slime != null)
            {
                slime.health -= aoedamage;

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
            if (turret != null)
            {
                turret.health -= aoedamage;


                turret.TakeDamage();


            }
        }
    }
}
