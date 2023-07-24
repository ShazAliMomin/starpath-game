using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest1SpawnPoints : MonoBehaviour
{
    public PlayerController1 player;
    // Start is called before the first frame update
    void Start()
    {
        if(SoundManager.SoundManagerInstance.getPreviousScene().name == "Forest 2")
        {
            player.transform.position = new Vector2(60f, 60f);
        }
        else
        {
            player.transform.position = new Vector2(-1.79f, -8.63f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
