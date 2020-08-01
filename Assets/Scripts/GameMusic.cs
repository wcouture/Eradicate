using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    AudioSource AuSo;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        AuSo = GetComponent<AudioSource>();
        AuSo.Play();
    }

    public void changeVolume()
    {
        AuSo.volume = PlayerPrefs.GetFloat("MusicVolume");
        AuSo.Play();
    }
}
