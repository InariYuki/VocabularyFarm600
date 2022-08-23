using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BGMSlider;
    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGMVolume", BGMSlider.value * 80 - 80);
    }
}
