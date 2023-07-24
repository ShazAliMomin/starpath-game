using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class exitcontainerscript : MonoBehaviour
{


    // Update is called once per frame
    /*void Update()
    {



    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hubmanager.sbarr[17] == true)
        {

            
            hubmanager.ps = 2;

            // convert the variable into a Ink.Runtime.Object value
            bool b = true;
            Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(b);
            // call the DialogueManager to set the variable in the globals dictionary
            DialogueManager.GetInstance().SetVariableState("dockmish", obj);

            bool b2 = false;
            Ink.Runtime.Object obj2 = new Ink.Runtime.BoolValue(b2);
            DialogueManager.GetInstance().SetVariableState("dockstart", obj2);

            SceneManager.LoadScene("Town");
         }
            
        }
    }

