using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserhitbox : MonoBehaviour
{
    public GameObject player;
    bool playerhit;
    // Start is called before the first frame update
    void Start()
    {
        playerhit = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerhit)
        {
            player.GetComponent<PlayerController1>().TakeDamage(5);
            playerhit = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerhit = true;

        }









    }
}
