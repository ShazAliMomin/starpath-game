using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skycorpentrance : MonoBehaviour
{ 

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        //SoundManager.SoundManagerInstance.ChangeMusicTrack(8);
        SceneManager.LoadScene("Level 1");
    }
}

}
