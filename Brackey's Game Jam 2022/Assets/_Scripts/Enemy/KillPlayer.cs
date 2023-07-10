using DialogueSystem;
using System.Collections.Generic;
using TaskSystem;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    //private bool gotKnife = false;
    //[SerializeField] DialogueEvent dialogue;

    //private void OnEnable() {
    //    TaskManager.OnNewTaskStarted += () => { gotKnife = true; };
    //}

    //private void OnDisable() {
    //    TaskManager.OnNewTaskStarted -= () => { gotKnife = true; };
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.currentState == GameState.Exploration)
        {
            GameManager.Instance.GameOver();
            //if (gotKnife) 
            //    DialogueManager.Instance.StartDialogue(dialogue);
            //else
            //    GameManager.Instance.GameOver();
        }
    }
}
