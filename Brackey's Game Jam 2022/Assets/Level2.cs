using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] private List<DialogueTrigger> dialogueSet;
    [SerializeField] private List<Task> secondLevelTaskList;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetTasks(secondLevelTaskList);
    }

    public void StartLevelDialogue()
    {
        dialogueSet[0].StartDialogue();
        GameManager.instance.EnemyStart();
    }
}
