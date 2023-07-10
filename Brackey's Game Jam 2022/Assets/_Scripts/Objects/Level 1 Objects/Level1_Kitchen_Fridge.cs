using DialogueSystem;
using TaskSystem;
using UnityEngine;

public class Level1_Kitchen_Fridge : InteractableObject
{
    private bool canInteract = true;
    public int interactCount = 0;
    [SerializeField] private DialogueEvent dialogueUninteracted;

    [Header("Task")]
    [SerializeField] private TaskInfoSO taskInfoSO;
    //[SerializeField] private TaskProgress startingProgress;

    public override bool CheckInteractConditions() {
        if (Input.GetKeyDown(KeyCode.Space) &&
            isPlayerColliding &&
            GameManager.Instance.currentState == GameState.Exploration &&
            canInteract)
            return true;
        return false;
    }

    public override void Interact() {
        base.Interact();
        HandleDialogue();

        if (interactCount == 0) {
            TaskManager.Instance.FinishTask(taskInfoSO);
            canInteract = false;
        }
        interactCount++;
    }


    public void HandleDialogue() {
        if (interactCount == 0) {
            DialogueManager.Instance.StartDialogue(dialogueUninteracted);
        }
    }

}
