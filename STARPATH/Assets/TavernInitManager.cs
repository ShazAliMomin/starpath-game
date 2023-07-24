using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernInitManager : MonoBehaviour
{
    [SerializeField] GameObject g;
    private IEnumerator waiter()
    {

        yield return new WaitForSeconds(2f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        bool finit =  (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("freyainit")).value );

        if (finit)
        {

            Destroy(g);
        }
        
        
        if (finit && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
           
               
                SceneManager.LoadScene("tavernfight");
            

        }


    }
}
