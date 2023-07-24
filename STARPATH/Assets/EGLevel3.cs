using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EGLevel3 : MonoBehaviour
{

    private void Update()
    {
      
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //SoundManager.SoundManagerInstance.ChangeMusicTrack(2);
            SceneManager.LoadScene("Town");
        }
    }
 
}
