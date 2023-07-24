using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingmanager : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        SoundManager.SoundManagerInstance.CheckScene(true);

    }

    void updateoncomplete()
    {

        SceneManager.LoadScene("credits");
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying && ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("enddia")).value == true)
        {

            animator.SetTrigger("fadeout");
        }









    }
}