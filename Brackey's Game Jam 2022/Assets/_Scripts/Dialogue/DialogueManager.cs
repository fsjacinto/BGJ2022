using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

namespace DialogueSystem {
    public class DialogueManager : Singleton<DialogueManager> {
        [SerializeField] private DialogueUI dialogueUI;
        MessageInfo[] currentMessages;
        int activeMessage = 0;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.currentState == GameState.Dialogue) {
                NextMessage();
            }
        }

        public void StartDialogue(DialogueEvent dialogueEvent) {
            GameManager.Instance.UpdateGameState(GameState.Dialogue);

            SetDialogueInfo(dialogueEvent);
            dialogueUI.ToggleUI(true);
            dialogueUI.DisplayMessage(currentMessages[activeMessage]);
        }

        public void SetDialogueInfo(DialogueEvent dialogueEvent) {      
            currentMessages = dialogueEvent.messages;
            activeMessage = 0;
        }

        public async void NextMessage() {
            activeMessage++;

            if (activeMessage < currentMessages.Length) { // if there are more messages
                dialogueUI.DisplayMessage(currentMessages[activeMessage]);
            }
            else {
                dialogueUI.ToggleUI(false);

                await Task.Delay(200);
                GameManager.Instance.UpdateGameState(GameState.Exploration);
            }
        }
    }
}
