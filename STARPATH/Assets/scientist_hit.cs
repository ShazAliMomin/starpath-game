using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scientist_hit : MonoBehaviour
{
    private float liveTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
     
    }
}
