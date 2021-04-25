using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;

    public void SetLevel()
    {
        float sound = slider.value;
        if (sound == -40f) mixer.SetFloat("Music", -80);
        else mixer.SetFloat("Music", sound);
    }
}