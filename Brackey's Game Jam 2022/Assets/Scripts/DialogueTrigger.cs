using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;
    public Message[] messages;
    public Actor[] actors;

    public UnityEvent Functions;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(Functions, messages, actors);
    }
}

[System.Serializable]
public class Message
{
    public int actorID;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
