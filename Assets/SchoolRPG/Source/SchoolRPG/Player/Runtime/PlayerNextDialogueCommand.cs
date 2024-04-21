using SchoolRPG.CommandPattern.Runtime;
using SchoolRPG.Dialogue.Runtime;

namespace SchoolRPG.Player.Runtime
{
    /// <summary>
    /// Tries to progress the current open dialogue.
    /// </summary>
    public readonly struct PlayerNextDialogueCommand : ICommand
    {
        private readonly DialogueEventChannel dialogueEventChannel;

        public PlayerNextDialogueCommand(DialogueEventChannel dialogueEventChannel)
        {
            this.dialogueEventChannel = dialogueEventChannel;
        }
        
        public void Execute()
        {
            dialogueEventChannel.RaiseOnNextDialogueRequested();
        }
    }
}