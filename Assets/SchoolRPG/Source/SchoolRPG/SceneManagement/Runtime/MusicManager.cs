using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnDestroy()
    {
        // Ensure the audio stops when the object is destroyed
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
