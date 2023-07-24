using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2ventscript : MonoBehaviour


{

    [SerializeField] private GameObject cue, escblock;

    private bool playerinRange;
    // Start is called before the first frame update
    void Start()
    {
        playerinRange = false;
        cue.SetActive(false);
        escblock.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerinRange)
        {
            cue.SetActive(true);
            if (( Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ButtonA") ) && EGLevel2.esc)
            {
                escblock.SetActive(true);
                


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
