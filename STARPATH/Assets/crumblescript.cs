using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Threading;
using System.Threading.Tasks;



public class crumblescript : MonoBehaviour
{
    int playerinRange;
   [SerializeField] private GameObject db;
   Animator myAnimator;
  const string PRESS_ANIM = "CT";
    bool timerReached = false;
    float timer;
    System.Random r = new System.Random();
    double k;

    public static Task statime = Task.Run(async delegate {
        await Task.Delay(6000);
    });


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && playerinRange != 2)
            playerinRange = 1;
        //db.SetActive(true);
    }
    
    private void Awake()
    {
        playerinRange = 0;
        myAnimator = GetComponent<Animator>();
        k = r.NextDouble() * (11.0 - 7.0) + 7.0;
       db.SetActive(false);
        timer = 0;
        myAnimator.SetBool("ded", false);
    }

    private void Update()
    {
            if(playerinRange == 2)
        {
          //  db.SetActive(true);
        }
            
        
        if(myAnimator.GetBool("ded"))
        {
            db.SetActive(true);
            //Debug.Log("ded is in fact  true");

        }
        
        if (playerinRange == 1)
        {
            
            /*UnityEngine.Random.seed = System.DateTime.Now.Millisecond;
            float k = UnityEngine.Random.Range(1, 3);
            */
            //Debug.Log("Random number generated:  " + k);


            if (!timerReached)
                timer += Time.deltaTime;

            if (!timerReached && timer > k)
            {
                myAnimator.SetTrigger(PRESS_ANIM);
                statime.Wait();
               // db.SetActive(true) ;
                //Set to false so that We don't run this again
                timerReached = false;
                playerinRange = 2;
            }

        }

    }

}



