using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] private List<DialogueTrigger> dialogueSet;

    // public List<TaskObject> taskList;

    [SerializeField] private List<Task> firstLevelTaskList;
    // 0: interact with box

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetTasks(firstLevelTaskList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevelDialogue()
    {
        dialogueSet[0].StartDialogue();
    }

    //public bool CheckTasks(List<int> taskIndexList)
    //{
    //    foreach(int i in taskIndexList)
    //    {
    //        if(taskList[i].isFinished == false)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}
}

//public enum FirstLevelTasks
//{
//    Box
//}
