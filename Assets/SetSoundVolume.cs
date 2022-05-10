using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetSoundVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("SoundsVol", Mathf.Log10(sliderValue) * 20);
    }
}
