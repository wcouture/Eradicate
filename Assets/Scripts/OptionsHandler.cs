using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    public void MusicVolume()
    {
        Slider slider = GameObject.Find("Music Volume").GetComponent<Slider>();
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
        GameObject.Find("Music").GetComponent<GameMusic>().changeVolume();
    }

    public void AnnouncerVolume()
    {
        Slider slider = GameObject.Find("Announcer Volume").GetComponent<Slider>();
        AudioSource announceExample = GameObject.Find("Announcer Volume").GetComponent<AudioSource>();
        PlayerPrefs.SetFloat("AnnouncerVolume", slider.value);
        announceExample.volume = slider.value;
        announceExample.Play();

    }

    public void FXvolume()
    {
        Slider slider = GameObject.Find("FX Volume").GetComponent<Slider>();
        AudioSource fxExample = GameObject.Find("FX Volume").GetComponent<AudioSource>();
        PlayerPrefs.SetFloat("FXvolume", slider.value);
        fxExample.volume = slider.value;
        fxExample.Play();
    }
}
