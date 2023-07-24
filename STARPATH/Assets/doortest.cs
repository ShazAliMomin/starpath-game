using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class doortest : MonoBehaviour
{


    // Update is called once per frame
    /*void Update()
    {



    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool fcomp = (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("freyacomp")).value);
            hubmanager.ps = 1;
            if (fcomp)
            {
                SceneManager.LoadScene("tavernnormal2");
            }
            else
            {
               
                //SoundManager.SoundManagerInstance.ChangeMusicTrack(2);
                SceneManager.LoadScene("tavern");


            }
        }
    }
}
