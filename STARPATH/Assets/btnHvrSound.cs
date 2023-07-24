using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnHvrSound : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip hvrSound;
    public AudioClip clkSound;

    public void hoverSound()
    {
        myAudio.PlayOneShot (hvrSound);
    }
    public void lickSound()
    {
        myAudio.PlayOneShot (clkSound);
    }

}
