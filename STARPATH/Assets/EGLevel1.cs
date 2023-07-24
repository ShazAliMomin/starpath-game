using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EGLevel1 : MonoBehaviour
{

    public static bool ev = false;
    public PlayerController1 p;
    System.Random r = new System.Random();
    public static int chosen;

    // Start is called before the first frame update
    void Awake()
    {
        chosen = Random.Range(10, 16);

        Debug.Log("AWAKENED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        ev = false;
        p.inventory.RemoveItemID(0);
        Debug.Log("AFTER REMOVE ITEM ID!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        hubmanager.sbarr[10] = false;
        hubmanager.sbarr[11] = false;
        hubmanager.sbarr[12] = false;
        hubmanager.sbarr[13] = false;
        hubmanager.sbarr[14] = false;
        hubmanager.sbarr[15] = false;
        hubmanager.sbarr[16] = false;

        Debug.Log("sbarr[10] == " + hubmanager.sbarr[10].ToString());
        Debug.Log("sbarr[11] == " + hubmanager.sbarr[11].ToString());
        Debug.Log("sbarr[12] ==" + hubmanager.sbarr[12].ToString());
        Debug.Log("sbarr[13] ==" + hubmanager.sbarr[13].ToString());
        Debug.Log("sbarr[14] ==" + hubmanager.sbarr[14].ToString());
        Debug.Log("sbarr[15] ==" + hubmanager.sbarr[15].ToString());

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("sbarr[10] == " + hubmanager.sbarr[10].ToString());

        if ( hubmanager.sbarr[10] == true)
        {

            ev = true;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //SoundManager.SoundManagerInstance.ChangeMusicTrack(2);
            SceneManager.LoadScene("Town");
        }
    }*/


    public void FailSafe()
    {
        ev = true;
    }

}
