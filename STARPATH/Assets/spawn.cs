using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static int ps = 0;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (ps == 1)
        {
            player.position = new Vector2(0, 0);


        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
