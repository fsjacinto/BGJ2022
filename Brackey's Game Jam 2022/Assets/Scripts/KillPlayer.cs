using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private bool canWin = false;
    [SerializeField] private List<int> prereqList;
    private bool gotKnife = false;
    [SerializeField] DialogueTrigger dialogue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && GameManager.instance.currentState == GameState.Exploration)
        {
            if(!canWin)
                GameManager.instance.GameOver();
            else
            {
                gotKnife = GameManager.instance.CheckPrereqTasks(prereqList);

                if (gotKnife)
                {
                    dialogue.StartDialogue();
                 //   GameManager.instance.TransitionToNextLevel();
                }
                else
                {
                    GameManager.instance.GameOver();
                }
            }
        }
    }
}
