using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollider : MonoBehaviour
{

    private bool canAccess = false;
    private bool isTriggered = false;
    [SerializeField] private DialogueTrigger dialogue;
    [SerializeField] private bool isColliding = false;

    // Requirements
    [SerializeField] private List<int> prereqList;

    // Update is called once per frame
    void Update()
    {
        if (isColliding && GameManager.instance.currentState == GameState.Exploration)
        {
            if (canAccess && !isTriggered)
            {
                if (dialogue != null)
                {
                    isTriggered = true;
                    dialogue.StartDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isColliding = true;

            canAccess = GameManager.instance.CheckPrereqTasks(prereqList);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
