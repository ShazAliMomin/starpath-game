using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [Header("Cue")]
    [SerializeField] private GameObject cue;
    [Header("doors")]
    [SerializeField] private GameObject doors;
    private bool playerinRange;

    private void Awake()
    {
        playerinRange = false;
        cue.SetActive(false);
    }


    private void Update()
    {
        if (playerinRange)
        {
            cue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ButtonA"))
            {

                if (EGLevel1.ev == true)
                {
                    SoundManager.SoundManagerInstance.PlayMiscSound(1);
                    Destroy(doors.gameObject);
                }

                else
                {
                    SoundManager.SoundManagerInstance.PlayMiscSound(7);
                    //display fail message
                }

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

