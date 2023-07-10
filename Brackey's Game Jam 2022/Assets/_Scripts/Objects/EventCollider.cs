using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueSystem;
using TaskSystem;

public class EventCollider : MonoBehaviour
{

    private bool canTrigger;
    private bool isTriggered = false;
    [SerializeField] private DialogueEvent dialogueEvent;
    [SerializeField] private bool isColliding = false;

    // Requirements
    [SerializeField] private TaskInfoSO[] requiredTaskList;

    public UnityEvent OtherFunctions;

    private void Update()
    {
        if (isColliding && GameManager.Instance.currentState == GameState.Exploration)
        {
            if (canTrigger && !isTriggered)
            {
                if (dialogueEvent != null)
                {
                    isTriggered = true;
                    DialogueManager.Instance.StartDialogue(dialogueEvent);
                    DoFunctions();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isColliding = true;

            CheckRequiredTasks();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private void CheckRequiredTasks() {
        canTrigger = true;
        foreach (TaskInfoSO taskItemSO in requiredTaskList) {
            if (TaskManager.Instance.GetTaskItem(taskItemSO).taskProgress != TaskProgress.FINISHED)
                canTrigger = false;
        }
    }

    private void DoFunctions() {
        OtherFunctions.Invoke();
    }
}
