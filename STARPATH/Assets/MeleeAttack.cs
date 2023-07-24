using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Collider2D meleeCollider;
    public float damage = 2;
    Vector2 rightMeleeOffset;
    public float flipOffset = 0.10f;
    [SerializeField] private PlayerController1 player;

    private void Start()
    {
        
        rightMeleeOffset = transform.localPosition;
    }

    public void MeleeRight()
    {
        meleeCollider.enabled = true;
        transform.localPosition = rightMeleeOffset;
        SoundManager.SoundManagerInstance.PlayMeleeSound((int)Random.Range(0, 2));
    }

    public void MeleeLeft()
    {
        meleeCollider.enabled = true;
        transform.localPosition = new Vector3((rightMeleeOffset.x * -1)-flipOffset, rightMeleeOffset.y);
        SoundManager.SoundManagerInstance.PlayMeleeSound((int)Random.Range(0, 2));
    }

    public void SwitchDirection(int i)
    {
        //0 = left
        if(i == 0)
        {
            transform.localPosition = new Vector3((rightMeleeOffset.x * -1)-flipOffset, rightMeleeOffset.y);
        }
        else if(i == 1)
        {
            transform.localPosition = rightMeleeOffset;
        }
    }

    public void StopMelee()
    {
        meleeCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damage = 2 + ((AbilityManager.AbilityManagerInstance.getUnlockLevel(7, 0)-1)*2);

        if (other.tag == "Enemy")
        {
            MeleeEnemy enemy = other.GetComponent<MeleeEnemy>();
                
            if (enemy != null)
            {
                SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(0, 2));

                enemy.health -= damage;
                enemy.animator.SetBool("isHit", true);
                if (enemy.health > 0)
                {
                    enemy.TakeDamage(true);
                }
                else
                {
                    player.gainHealth(1);
                }
                enemy.Stun(10f);
          
            }
        }

        else if (other.gameObject.tag == "Slime")
        {
            SlimeScript slime = other.gameObject.GetComponent<SlimeScript>();
            if (slime != null)
            {
                slime.health -= damage;

                if (slime.health >=0)
                {
                    slime.TakeDamage();
                }
                else
                {
                    player.gainHealth(1);
                }
            }
        }

        else if(other.tag == "BlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.GetComponent<BlasterEnemy>();
            ScientistEnemy sciEnemy = other.GetComponent<ScientistEnemy>();
            SpreadEnemy shotEnemy = other.GetComponent<SpreadEnemy>();
            if (blastEnemy != null)
            {
                SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(3, 4));
                blastEnemy.health -= damage*2.5f;

                if (blastEnemy.health > 0)
                {
                    blastEnemy.TakeDamage(true);
                }
                else
                {
                    player.gainHealth(1);
                }

                blastEnemy.Stun(10f);
            }
            if(sciEnemy != null)
            {
                SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(3, 4));
                sciEnemy.health -= damage * 2.5f;

                if (sciEnemy.health > 0)
                {
                    sciEnemy.TakeDamage(true);
                }
                else
                {
                    player.gainHealth(1);
                }

                sciEnemy.Stun(10f);
            }
            if (shotEnemy != null)
            {
                SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(3, 4));
                shotEnemy.health -= damage * 2.5f;

                if (shotEnemy.health > 0)
                {
                    shotEnemy.TakeDamage();
                }
                else
                {
                    player.gainHealth(1);
                }

                shotEnemy.Stun(10f);
            }
        }

        else if(other.tag == "ShieldedBlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                SoundManager.SoundManagerInstance.PlayImpactSound((int)Random.Range(3, 4));
                blastEnemy.health -= damage * 2.5f;

                if (blastEnemy.health > 0)
                {
                    blastEnemy.TakeDamage(true);
                }
                else
                {
                    player.gainHealth(2);
                }

                blastEnemy.Stun(10f);

            }
        }

        else if (other.tag == "Turret")
        {
            Turret turret = other.GetComponent<Turret>();
            turret_rocket rTurret = other.GetComponent<turret_rocket>();
            if (turret != null)
            {
                turret.health -= damage;
                turret.TakeDamage();
                if(turret.health <= 0)
                {
                    player.gainHealth(1);
                }
            }
            if (rTurret != null)
            {
                rTurret.health -= damage;
                rTurret.TakeDamage();
                if(rTurret.health <= 0)
                {
                    player.gainHealth(1);
                }
            }
        }

        else if(other.tag == "TurretBullet" && AbilityManager.AbilityManagerInstance.getUnlockAbility(7,0))
        {
            Projectile bullet = other.GetComponent<Projectile>();
            bullet.Deflect();
        }
    }
}
