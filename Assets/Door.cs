using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using SchoolRPG.Dialogue.Runtime;
using UnityEngine;

namespace SchoolRPG.Interaction.Runtime
{
    /// <summary>
    /// Just raises dialogue event I'm not naming this rn
    /// </summary>
    public class DoorScript : Interactable
    {

        [SerializeField]
        private SceneEventChannel sceneEventChannel;

        public double doorNumber;

        private void OnEnable()
        {
            dialogueEventChannel.OnDoorEnterRequested += DisableInteraction;
            dialogueEventChannel.OnDoorEnterCompleted += EnableInteraction;
        }

        private void OnDisable()
        {
            dialogueEventChannel.OnDoorEnterRequested -= DisableInteraction;
            dialogueEventChannel.OnDoorEnterCompleted -= EnableInteraction;
        }

        private void Start()
        {
            EnableInteraction();
        }

        public override void OnInteract()
        {
            if (!isInteractable) return;
            sceneEventChannel.RaiseOnDoorEnterRequested(dialogue);
            DisableInteraction();
        }

        private void EnableInteraction()
        {
            isInteractable = true;
        }

        private void DisableInteraction(IList<string> _ = null)
        {
            isInteractable = false;
        }
    }
}
