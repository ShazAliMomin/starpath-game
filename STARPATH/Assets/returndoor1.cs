using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class returndoor1 : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hubmanager.ps = 1;
            SceneManager.LoadScene("Town");
        }
    }
}