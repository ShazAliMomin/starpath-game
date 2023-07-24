using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hubmanager : MonoBehaviour
{

    [SerializeField] private GameObject zekei, zeken, janna, norman, gate, beac, tommy, tommyf, slimes;
    public static bool start;
    public static int ps = 0;
    public Transform player;
    public static bool sb1 = false;
    public static bool[] sbarr = new bool[40];
    public static bool[] pbarr = new bool[40];
    public static bool[] slarr = new bool[40];
    public static string ns;




    // Start is called before the first frame update
    void Awake()
    {


        if ( ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("zekeack")).value == false )
        {

            beac.SetActive(false);
        }
        else
        {
            beac.SetActive(true);
            zeken.SetActive(false);
        }

        bool start = (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("freyainit")).value);


        if (!start)
        {
            zekei.SetActive(true);
            zeken.SetActive(false);
            janna.SetActive(false);
            norman.SetActive(false);
            slimes.SetActive(false);
            tommyf.SetActive(false);
            tommy.SetActive(false);
        }
        else
        {
            if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("zekeack")).value == false)
            {
                zeken.SetActive(true);
            }
            else
            {
                zeken.SetActive(false);
            }

            tommyf.SetActive(false);
            tommy.SetActive(true);
            zekei.SetActive(false);
            janna.SetActive(true);
            norman.SetActive(true);
            slimes.SetActive(true);
        }

        if (ps == 0)
        {
            player.position = new Vector2(-34.79f, -2.33f);
        }
        else if (ps == 1)
        {
            player.position = new Vector2(3.14f, -6.3f);
            ps = 0;
        }

        else if (ps == 2)
        {
            bool b = true;
            Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(b);
            // call the DialogueManager to set the variable in the globals dictionary
            DialogueManager.GetInstance().SetVariableState("dockmish", obj);
            player.position = new Vector2(-7.47f, -23.29f);
            ps = 0;
        }

        else if (ps == 3)
        {
            player.position = new Vector2(-53f, 24f);
            ps = 0;
        }

    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

        // Update is called once per frame
    void Update()
    {

        if ( ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("dockstart")).value && ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("dockmish")).value == false)
        {
            SceneManager.LoadScene("dock level");
            bool b = false;
            Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(b);
            DialogueManager.GetInstance().SetVariableState("dockstart", obj);
            SceneManager.LoadScene("dock level");
        }


        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("zekeack")).value) 
        {
            zeken.SetActive(false);
        }

        bool gopen = (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("gateopen")).value);

        if (gopen == true)
        {

            gate.SetActive(false);
        }

        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("zekeack")).value == true)
        {

            beac.SetActive(true);
        }

        if (
            
            ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("dockmish")).value == true 
            &&
            ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("dockdone")).value == false
        )
        {
            tommyf.SetActive(true);
            tommy.SetActive(false);

        }

        if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("dockdone")).value == true &&
             !DialogueManager.GetInstance().dialogueIsPlaying )
        {
            tommyf.SetActive(false);
            tommy.SetActive(true);
        }
      


    }

    
}
