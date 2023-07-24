using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class containerlevelmanager : MonoBehaviour
{
    public GameObject exit, e1, e2, e3;
    public PlayerController1 p;
    // Start is called before the first frame update
    void Awake()
    {
        hubmanager.sbarr[17] = false;
        exit.SetActive(false);
        e1.SetActive(false);
        e2.SetActive(false);
        e3.SetActive(false);
        p.inventory.RemoveItemID(7);



    }

    // Update is called once per frame
    void Update()
    {
        if(hubmanager.sbarr[17] == true)
        {

            exit.SetActive(true);
            if(e1 != null)
            {
                e1.SetActive(true);
            }

            if (e2 != null)
            {
                e2.SetActive(true);
            }
            if (e3 != null)
            {
                e3.SetActive(true);
            }
           
        }



        
    }
}
