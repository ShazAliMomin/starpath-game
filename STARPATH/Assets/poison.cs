using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poison : MonoBehaviour
{
    private bool ispoisoned = false; 
    public int damage = 1;
    public float spillTime = 2f;
    public int tickTime = 5; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spillTime -= Time.deltaTime;
        if (spillTime <= 0)
        {
            Destroy(gameObject);
        }
     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            PlayerController1 user = other.gameObject.GetComponent<PlayerController1>();
            user.ApplyBurn(tickTime);
            //user.setpoisonTick(tickTime);
            //user.setpDmg(damage);
           // user.setPoison(true);
        }
    }

   
}
