using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1SceneTrans : MonoBehaviour
{ 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && EGLevel1.ev == true)
        {
            SceneManager.LoadScene("Level 2");
        }
    }


}
