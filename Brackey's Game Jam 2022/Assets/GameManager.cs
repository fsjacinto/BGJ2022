using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentState;
    [SerializeField] private GameObject levelManager;
    public List<Task> levelTaskList = new List<Task>();
    public GameObject dialogueGO;
    public PlayerMovement playerMovement;
    public Animator playerAnimator;


    private void Awake()
    {
        // Initialize GameManager Singleton
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindWithTag("LevelManager");
        GameObject canvas = GameObject.FindWithTag("Canvas").gameObject;
        dialogueGO = canvas.transform.Find("DialogueBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;

        if(currentState == GameState.Exploration)
        {

        }

        else if(currentState == GameState.Dialogue)
        {
            //playerMovement.playerAnimator.SetTrigger("Idle");
              playerAnimator.SetTrigger("Idle");
            //playerMovement.PlayerFaceTo(PlayerFace.Right);
            // Stop Enemy Movement
            // Stop Player Movement
        }
    }

    public void CallDialogue(DialogueTrigger dialogue)
    {
        dialogue.StartDialogue();
    }

    public void SetTasks(List<Task> tasks)
    {
        int index = 0;
        foreach(Task taskItem in tasks)
        {
            levelTaskList.Add(taskItem);
            levelTaskList[index].isFinished = false;
            index++;
        }
    }

    public bool CheckPrereqTasks(List<int> taskIndexList)
    {
        foreach(int i in taskIndexList)
        {
            if(levelTaskList[i].isFinished == false)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateTaskBool(int taskIndex)
    {
        levelTaskList[taskIndex].isFinished = true;
    }

}

public enum GameState
{
    Exploration,
    Dialogue,
    Hiding,
}

[System.Serializable]
public class Task
{
    //public int index;
    public string taskName;
    public bool isFinished;
}
