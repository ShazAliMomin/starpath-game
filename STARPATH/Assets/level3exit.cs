using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level3exit : MonoBehaviour
{
    // Update is called once per frame
    /*void Update()
    {



    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
                SceneManager.LoadScene("LaserLevel");
           
        }
    }
}