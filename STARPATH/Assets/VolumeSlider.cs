using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private int type;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(val => SoundManager.SoundManagerInstance.ChangeVolume(type, val));
    }

    
}
