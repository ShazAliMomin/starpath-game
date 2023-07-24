using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level2ventescape : MonoBehaviour
{


    // Update is called once per frame
    /*void Update()
    {



    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
                SceneManager.LoadScene("Level 2 and a half");
            
        }
    }
}