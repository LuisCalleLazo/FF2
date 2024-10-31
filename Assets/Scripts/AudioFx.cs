using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFx : MonoBehaviour
{
    public AudioClip [] fxs;
    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void FXSonidoCoche()
    {
        audioS.clip = fxs[0];
        audioS.Play();
    }

    public void FXMusic()
    {
        audioS.clip = fxs[1];
        audioS.Play();
    }
    
}
