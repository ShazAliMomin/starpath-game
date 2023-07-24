using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float minIntensity, maxIntensity, minRate, maxRate, currentRate;

    private float time, startIntensity;
    private Light2D lightSource;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light2D>();
        startIntensity = lightSource.intensity;
        currentRate = Random.Range(minRate, maxRate);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //time += Time.deltaTime * (1 - Random.Range(minIntensity, maxIntensity));

        lightSource.intensity += (Time.deltaTime * currentRate);

        if(lightSource.intensity >= maxIntensity)
        {
            currentRate = -1 * Random.Range(minRate, maxRate);
            lightSource.intensity = maxIntensity;
        }
        if(lightSource.intensity <= minIntensity)
        {
            currentRate =Random.Range(minRate, maxRate);
            lightSource.intensity = minIntensity;
        }
    }
}
