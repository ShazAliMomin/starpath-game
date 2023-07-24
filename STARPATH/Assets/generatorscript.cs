using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;


public class generatorscript : MonoBehaviour

    
{

    public static Task statime = Task.Run(async delegate {
        await Task.Delay(5000);
    });

    public float health = 30;
    float ht; 
    public GameObject s;
    Animator animator;
    [SerializeField] GameObject blastLighting;
   // bool ps;
    // Start is called before the first frame update
    void Start()
    {
        ht = health;
        //ps = false;
       
        animator = GetComponent<Animator>();

        

        s.SetActive(true);
    }

    // Update is called once per frame
    void Update() 
    {

        if (animator.GetBool("isded"))
        {
            s.SetActive(false);
            

        }


    }

    public void ExplosionSound(int numberOfExplosions)
    {
        for(int i=0; i < numberOfExplosions; i++)
        {
            SoundManager.SoundManagerInstance.PlayExplosionSound((int)UnityEngine.Random.Range(0, 2));
            GameObject explodeLight = Instantiate(blastLighting, transform.position, Quaternion.identity);
            if(numberOfExplosions >= 5){
                explodeLight.GetComponent<Light2D>().intensity = 80;
                explodeLight.GetComponent<Light2D>().pointLightOuterRadius = 8.0f;
            }
        }
    }

    public void DamageOverflow()
    {
        animator.SetTrigger("1 to 2");
        animator.SetTrigger("2 to 3");
        animator.SetTrigger("3 to dead");
        ExplosionSound(3);
    }

    public void TakeDamage(float d)
    {
        
        if(health >= (ht/3)*2)
        {


            if ((health - d) < ht / 3 * 2 && (health - d) >= ht / 3)
            {
                animator.SetTrigger("1 to 2");
            }
            else
            {
                animator.SetTrigger("1ishit");
            }
            health -= d;




        }
        else if((health < ht / 3 * 2 && health >= ht / 3))
        {
        
            if ((health - d) < (ht / 3) && (health - d) > 0)
            {
                animator.SetTrigger("2 to 3");
            }
            else
            {
                animator.SetTrigger("2ishit");
            }
            health -= d;

        }
        else if(health < ht / 3 && health > 0)
        {    
            if ((health - d) <= 0)
            {
                animator.SetTrigger("3 to dead");
            }
            else
            {
                animator.SetTrigger("3ishit");
            }
            health -= d;



        }

        

    }
}
