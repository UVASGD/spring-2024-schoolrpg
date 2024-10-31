using SchoolRPG.Inventory.Runtime;
using SchoolRPG.Dialogue.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HospitalSoundPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip music;

    public void PlaySecondMusic()
    {
        musicSource.PlayOneShot(music);
    }
}