using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestReturn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hubmanager.ps = 3;
            //SceneManager.LoadScene("Town");
        }
    }
}
