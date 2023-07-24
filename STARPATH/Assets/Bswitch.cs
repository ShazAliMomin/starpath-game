using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Bswitch : MonoBehaviour
{

    [SerializeField] private GameObject cue, flip, shield;

    private bool playerinRange;
    bool ud = true;

    private void Awake()
    {
        playerinRange = false;
        cue.SetActive(false);
        flip.SetActive(false);
        shield.SetActive(true);
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

                EGLevel2.e1 *= -1;
                EGLevel2.e7 *= -1;
                SoundManager.SoundManagerInstance.PlayMiscSound(3);

                if (ud)
                {
                    ud = false;
                    flip.SetActive(true);
                    shield.SetActive(false);
                    SoundManager.SoundManagerInstance.PlayMiscSound(5);
                }
                else
                {
                    ud = true;
                    flip.SetActive(false);
                    shield.SetActive(true);
                    SoundManager.SoundManagerInstance.PlayMiscSound(4);
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

