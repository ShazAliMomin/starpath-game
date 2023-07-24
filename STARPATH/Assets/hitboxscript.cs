using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxscript : MonoBehaviour

{
    bool playerhit = false;
    public GameObject player;
    public int id;
    bool touched;
    // Start is called before the first frame update
    void Start()
    {
        touched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bosslevelmanager.rn == true)
        {
            touched = false;
        }
     




        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && bosslevelmanager.nxt != id && bosslevelmanager.fact == true && !touched)
        {
            player.GetComponent<PlayerController1>().TakeDamage(5);

            bosslevelmanager.rn = true;
            print("wrong tile touched");
        }
        else if(other.gameObject.tag == "Player" && bosslevelmanager.nxt == id && bosslevelmanager.fact == true)
        {

            bosslevelmanager.nxtrig = true;
            print("correct tile touched");
            touched = true;

        }

 
    }


}
