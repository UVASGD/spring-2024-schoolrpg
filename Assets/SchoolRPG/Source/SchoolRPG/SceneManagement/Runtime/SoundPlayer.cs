using SchoolRPG.Inventory.Runtime;
using SchoolRPG.Dialogue.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private DialogueEventChannel dialogueEventChannel;
    [SerializeField] private InventoryEventChannel inventoryEventChannel;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip dialogueClick;
    [SerializeField] private AudioClip itemPickupSound;
    private void OnEnable()
    {
        inventoryEventChannel.OnAddInventoryItem += PlayItemPickupSound;
        dialogueEventChannel.OnNextDialogueRequested += PlayDialogueClick;
    }

    private void OnDisable()
    {
        inventoryEventChannel.OnAddInventoryItem -= PlayItemPickupSound;
        dialogueEventChannel.OnNextDialogueRequested -= PlayDialogueClick;

    }
    private void PlayItemPickupSound(InventoryItem item)
    {
        sfxSource.PlayOneShot(itemPickupSound);
    }

    private void PlayDialogueClick()
    {
        sfxSource.PlayOneShot(dialogueClick);
    }

    void OnDestroy()
    {
        // Ensure the audio stops when the object is destroyed
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
