using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool fs = false;
    bool b = false;
    public GameObject blast;
    void Start()
    {
      
        blast.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       // print("laserhit value: " + ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("laserhit2")).value);

        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("laserhit2")).value == true)
        {

            blast.SetActive(true);
            b= true;
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying && b)
        {
            fs = true;
        }

    }
}
