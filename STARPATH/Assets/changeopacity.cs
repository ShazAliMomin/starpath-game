using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeopacity : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    [SerializeField] private Renderer myModel;
    // Start is called before the first frame update
    private void Start()
    {

    }

    private void AlpgaSlider(float sliderValue)
    {
        Color color = myModel.material.color;
        color.a = sliderValue;
        myModel.material.color = color;
    }



    // Update is called once per frame
   
}
