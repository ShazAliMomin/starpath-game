using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] GameObject g;

    private bool playerInRange;
    public bool force = false;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);

    }
     
    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if ((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ButtonA")) || force)
            {
                force = false;
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}