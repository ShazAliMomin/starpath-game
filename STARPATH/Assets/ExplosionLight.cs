using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExplosionLight : MonoBehaviour
{
    private Light2D lightSource;
    [SerializeField] private float fadeRate;
    [SerializeField] private int behavior;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lightSource.intensity -= fadeRate;
        if (lightSource.intensity <= 0)
        {

            switch (behavior)
            {
                case 1: gameObject.SetActive(false);
                        
                    break;
                case 0:
                default:
                    Destroy(gameObject);
                    break;
            }

        }
    }
}
