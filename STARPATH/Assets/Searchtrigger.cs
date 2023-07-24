using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchtrigger : MonoBehaviour
{
    [Header("Cue")]
    [SerializeField] private GameObject cue;

    private bool playerinRange;

    private void Awake()
    {
        playerinRange = false;
        cue.SetActive(false);
    }

    private void Update()
    {
        if(playerinRange)
        {
            cue.SetActive(true);
          
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
