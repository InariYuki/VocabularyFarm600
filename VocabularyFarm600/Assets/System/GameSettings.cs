using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Transform animalContainer;
    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGMVolume", BGMSlider.value * 80 - 80);
    }
    public void ToggleNameOn()
    {
        for(int i = 0; i < animalContainer.childCount; i++)
        {
            animalContainer.GetChild(i).GetComponent<GiantAnimal>().animalNameOn();
        }
    }
    public void ToggleNameOff()
    {
        for (int i = 0; i < animalContainer.childCount; i++)
        {
            animalContainer.GetChild(i).GetComponent<GiantAnimal>().animalNameOff();
        }
    }
}
