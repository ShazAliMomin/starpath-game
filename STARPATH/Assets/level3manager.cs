using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3manager : MonoBehaviour
{
    public static int strikes;
    // Start is called before the first frame update
    void Start()
    {
        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("shiprepaired")).value == true)
        {
            strikes = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
