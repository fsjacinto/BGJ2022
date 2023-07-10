using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TaskSystem;
using UnityEngine;

public class TaskObject_v2 : InteractableObject, IDialogue
{
    public int interactCount = 0;
    [SerializeField] private DialogueEvent dialogueUninteracted;
    [SerializeField] private DialogueEvent dialogueInteracted;
    [SerializeField] private DialogueEvent dialogueFew;
    [SerializeField] private DialogueEvent dialogueMany;

    [Header("Task")]
    [SerializeField] private TaskInfoSO taskInfoSO; 

    public override void Interact() {
        base.Interact();
        HandleDialogue();

        if (interactCount == 0) {
            TaskManager.Instance.FinishTask(taskInfoSO);
        }
        interactCount++;
    }

    public void HandleDialogue() {
        if (interactCount == 0) {
            DialogueManager.Instance.StartDialogue(dialogueUninteracted);
        }
        else if (interactCount == 1) {
            DialogueManager.Instance.StartDialogue(dialogueInteracted);
        }
        else if (interactCount >= 2 && interactCount <= 4) {
            DialogueManager.Instance.StartDialogue(dialogueFew);
        }
        else {
            DialogueManager.Instance.StartDialogue(dialogueMany);
        }
    }

}
