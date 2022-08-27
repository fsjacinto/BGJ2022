using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour
{
    [SerializeField] private int taskIndex;
    private bool isfinished = false;
    private bool canAccess;
    [SerializeField] private bool isColliding = false;

    // Dialogues
    //[SerializeField] private List<DialogueTrigger> dialogueList;
    [SerializeField] private DialogueTrigger noAccessDialogue;
    [SerializeField] private DialogueTrigger yesAccessDialogue;
    [SerializeField] private DialogueTrigger finishedDialogue;

    // Requirements
    //[SerializeField] private List<TaskObject> prerequisiteTasks;
    [SerializeField] private List<int> prereqList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isColliding && GameManager.instance.currentState == GameState.Exploration)
        {
            if (canAccess)
            {
                if (isfinished)
                {
                    // finished dialogue
                    if (finishedDialogue != null)
                        finishedDialogue.StartDialogue();



                }
                else
                {
                    isfinished = true;
                    // yes access
                    if (yesAccessDialogue != null)
                        yesAccessDialogue.StartDialogue();

                    // update bool in Game Manager
                    GameManager.instance.UpdateTaskBool(taskIndex);
                }

            }
            else 
            {
                if(noAccessDialogue != null)
                    noAccessDialogue.StartDialogue();
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

    private bool CheckDoability()
    {
        return true;
    }
}
