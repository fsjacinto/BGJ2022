using UnityEngine;
using DialogueSystem;
using TaskSystem;

public class Level1_Basement_Door : ExitDoor, IDialogue
{
    [Header("Dialogue")]
    [SerializeField] private DialogueEvent dialogueLocked;

    private void Start() {
        isLocked = true;
    }

    private void OnEnable() {
        TaskManager.OnNewTaskStarted += () => { IsLocked(false); };
    }

    private void OnDisable() {
        TaskManager.OnNewTaskStarted -= () => { IsLocked(false); };
    }

    public override void Interact() {
        base.Interact();
        HandleDialogue();
    }

    public void HandleDialogue()
    {
        if(isLocked)
            DialogueManager.Instance.StartDialogue(dialogueLocked);
    }
}
