using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TaskSystem;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] private List<DialogueEvent> dialogueSet;
    [SerializeField] private List<TaskItem> secondLevelTaskList;

    void Start()
    {
        Debug.Log("Level2.cs Start Method - SetTasks() has been commented out");
        //GameManager.instance.SetTasks(secondLevelTaskList);
    }

    public void StartLevelDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueSet[0]);
        Debug.Log("Level2.cs StartLevelDialogue Method - StartPatrol() has been commented out");
        //GameManager.Instance.enemy.StartPatrol();
    }
}
