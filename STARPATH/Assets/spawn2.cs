using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn2 : MonoBehaviour
{
    public static int ps;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (ps == 1)
        {
            player.position = new Vector2(16.82206f, -3.536f);


        }
        ps = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
