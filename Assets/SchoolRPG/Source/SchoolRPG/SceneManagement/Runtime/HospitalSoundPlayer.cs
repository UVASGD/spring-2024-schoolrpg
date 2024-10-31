using SchoolRPG.Inventory.Runtime;
using SchoolRPG.Dialogue.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HospitalSoundPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource bgSFXSource;

    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip bgSFX;

    public void PlaySecondMusic()
    {
        musicSource.PlayOneShot(music);
    }

    public void PlayBGSFX()
    {
        bgSFXSource.PlayOneShot(bgSFX);
    }
}