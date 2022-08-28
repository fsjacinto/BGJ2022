using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;

    
    public float wordSpeed;

    private Queue<string> sentences;
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    /// ///////////////////////////////////////////////////////////
    [SerializeField] GameObject dialogueBox;
    public UnityEvent DialogueFunctions;
    //[SerializeField] DialogueTrigger lastDialogue;
    /// ///////////////////////////////////////////////////////////

    public void OpenDialogue(UnityEvent functions, Message[] messages, Actor[] actors) 
    {
        /// ///////////////////////////////////////////////////////////
        GameManager.instance.UpdateGameState(GameState.Dialogue);
        dialogueBox.SetActive(true);
        /// ///////////////////////////////////////////////////////////
        DialogueFunctions = functions;
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started conversation. Loaded messages: " + messages.Length);

        DisplayMessage();

        /*charSprite.sprite = dialogue.dialogueSprite;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }*/
    }

    public void DisplayMessage()
    {
        /*if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();*/

        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(messageToDisplay.message));
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended.");
            StartFunctions();
            //isActive = false;
            ////////////////////////////////////////////////////////////////////////////////
            dialogueBox.SetActive(false);
            StartCoroutine(DelayExploration());
            ////////////////////////////////////////////////////////////////////////////////
        }
    }

    IEnumerator DelayExploration()
    {
        yield return new WaitForSeconds(0.2f);
        isActive = false;
        GameManager.instance.currentState = GameState.Exploration;
    }

    IEnumerator TypeSentence(string sentence)
    {
        messageText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void StartFunctions()
    {
        DialogueFunctions.Invoke();
    }

    private void Update()
    {
        Debug.Log(GameManager.instance.currentState);
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true && GameManager.instance.currentState == GameState.Dialogue)
        {
            Debug.Log("Next");
            NextMessage();
        }
    }
}
