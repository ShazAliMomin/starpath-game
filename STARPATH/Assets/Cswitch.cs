using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Cswitch : MonoBehaviour
{

    [SerializeField] private GameObject cue, flip;

    private bool playerinRange;
    bool ud = true;

    private void Awake()
    {
        playerinRange = false;
        cue.SetActive(false);
        flip.SetActive(false);
    }

    public static Task timer = Task.Run(async delegate {
        await Task.Delay(2000);
    });



    private void Update()
    {
        if (playerinRange)
        {
            cue.SetActive(true);
            EGLevel2.ActivateShieldCamera();

            if (Input.GetKeyDown(KeyCode.R))
            {

                EGLevel2.e3 *= -1;
                EGLevel2.e6 *= -1;
                EGLevel2.e5 *= -1;

                SoundManager.SoundManagerInstance.PlayMiscSound(3);

                if (ud)
                {
                    ud = false;
                    flip.SetActive(true);

                }
                else
                {
                    ud = true;
                    flip.SetActive(false);
                }
                timer.Wait();
            }


        }
        else
        {
            cue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            playerinRange = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
            playerinRange = false;

    }
}

