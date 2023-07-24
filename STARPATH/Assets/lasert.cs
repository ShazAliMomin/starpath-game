using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lasert : MonoBehaviour
{

    Animator animator;
    public float health;
    float shealth;
    public int id;
    bool isImmune;
    bool blasting;
    bool destroyed;
    bool settling;
    public GameObject laserbox;
    public Image healthbar;
    bool p1, p2, p3;
    int tb;
   

    // Start is called before the first frame update

    IEnumerator blast()
    {
        if (tb == 0)
        {

            ++tb;
            print(id + " blasting");
            
             ++LaserLevelManager.sl;
            // isImmune = true;

            blasting = true;
            yield return new WaitForSeconds(1f);

            if (LaserLevelManager.blasts[id] == 1)
            {
                //SoundManager.SoundManagerInstance.PlayFireSound(14);
                SoundManager.SoundManagerInstance.LaserLevelChargeSound();
                animator.SetTrigger("charge");
            }

            yield return new WaitForSeconds(LaserLevelManager.chargetime);

            if (LaserLevelManager.blasts[id] == 1)
            {
                SoundManager.SoundManagerInstance.LaserLevelFireSound();

                animator.SetTrigger("blast");
            }

            yield return new WaitForSeconds(.25f);

            if (LaserLevelManager.blasts[id] == 1)
            {
                laserbox.SetActive(true);
            }

            yield return new WaitForSeconds(LaserLevelManager.blastime);
            print("blast time: " + LaserLevelManager.blastime);
            if (LaserLevelManager.blasts[id] == 1)
            {
                laserbox.SetActive(false);
                animator.SetTrigger("settle");
            }


            


            blasting = false;
            ++LaserLevelManager.blasting;
           
            LaserLevelManager.p2d += 1;
            print("p2d: " + LaserLevelManager.p2d);
        }
    }

    IEnumerator settle()
    {

        p3 = true;
        settling = true;
           //  isImmune = false;

             yield return new WaitForSeconds(LaserLevelManager.settletime);

             settling = false;
             
     ++LaserLevelManager.fl;
     
     LaserLevelManager.lhealth[id] = health;
     ++LaserLevelManager.setl;

        p1 = false;
        p2 = false;
       
        LaserLevelManager.p3d += 1;

    }


        void Start()
    {
        laserbox.SetActive(false);
        animator = GetComponent<Animator>();
        health = 50f;
        isImmune = true;
        blasting = false;
        destroyed = false;
        shealth = health;
        tb = 0;
          }

 


        // Update is called once per frame
        void Update()
    {

        healthbar.fillAmount = health / shealth;

        if (LaserLevelManager.p == 1 && p1 == false)
        {
            p3 = false;
            health = LaserLevelManager.lhealth[id];
            
            p1 = true;
            if (health <= 0 && !destroyed)
            {
                
                destroyed = true;
                animator.SetTrigger("kill");
            }

            if(destroyed == true && health > 0)
            {
                destroyed = false;
                animator.SetTrigger("revive");
            }

            LaserLevelManager.p1d += 1;
            

        }

        if (LaserLevelManager.p == 2 && p2 == false && tb == 0)
        {

            print("id: " + id + " phase: " + LaserLevelManager.p + " p2: " + p2);


            p2 = true;
            if (tb == 0)
            {
                StartCoroutine(blast());
            }
            print(id + " times blasted: " + tb);

        }

        if (LaserLevelManager.p == 3  && p3 == false)
        {
            tb = 0;
            p3 = true;
            StartCoroutine(settle());

        }

         if (LaserLevelManager.p == 3)
        {
            isImmune = false;
        }
         else
        {
            isImmune = true;
        }


    }

    public void TakeDamage(float d)
    {
        if (isImmune == false)
        {
            animator.SetTrigger("hit");
            health -= d;
            if (health <= 0)
            {
                SoundManager.SoundManagerInstance.PlayExplosionSound(Random.Range(0, 2));
                animator.SetTrigger("destroy");
                print("TURRET DESTROYED");
               // LaserLevelManager.blasts[id] = -1;
                destroyed = true;
                ++LaserLevelManager.dl;

            }

        }
    }
}
