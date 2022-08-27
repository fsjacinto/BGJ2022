using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;
    public GameState currentState;
    [SerializeField] private GameObject levelManager;
    public List<Task> levelTaskList = new List<Task>();
    public GameObject dialogueGO;
    public PlayerMovement playerMovement;
    public Animator playerAnimator;

    public LevelName currentLevel;
    private Level1 level1;
    //private Level2 level2;
    //private Level3 level3;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin virtualCameraPerlin;
    private float shakeTimer;

    [Header("Starting Panel System")]
    [SerializeField] private Image startingImage;
    [SerializeField] private TextMeshProUGUI startingText;

    [Header("Fade Transition System")]
    [SerializeField] private Image fadeTransitionImage;
    private float fadeTransitionTime;
    private float timeElapsed;

    [Header("DeathPanel")]
    [SerializeField] private Image deathImage;
    [SerializeField] private TextMeshProUGUI deathText;
    #endregion

    #region Gab Stuff
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

        if(currentLevel == LevelName.Level1)
        {
            level1 = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Level1>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindWithTag("LevelManager");
        GameObject canvas = GameObject.FindWithTag("Canvas").gameObject;
        dialogueGO = canvas.transform.Find("DialogueBox").gameObject;

        StartCoroutine(WaitTillFadeStartScreen());
    }

    private void Update()
    {
        CameraShakeTimer();
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

    public void TriggerFadeTransition(float _fadeTransitionTime)
    {
        fadeTransitionTime = _fadeTransitionTime;
        StartCoroutine(FadeInTransition());
    }
    #endregion

    #region Starting Panel Transition
    private IEnumerator WaitTillFadeStartScreen()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOutStartingPanel());
    }

    private IEnumerator FadeOutStartingPanel()
    {
        startingText.color = new Color(255f, 255f, 255f, 0);
        timeElapsed = 0f;
        float totalFadeTime = 2f;

        while (timeElapsed < totalFadeTime)
        {
            if (timeElapsed < 2f)
            {
                
                startingImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(1f, 0f, timeElapsed / totalFadeTime)));
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        level1.StartLevelDialogue();
    }
    #endregion

    #region Fade Transition
    private IEnumerator FadeInTransition()
    {
        UpdateGameState(GameState.Panel);

        timeElapsed = 0f;
        float totalFadeTime = fadeTransitionTime;

        while (timeElapsed < totalFadeTime)
        {
            if (timeElapsed < fadeTransitionTime)
            {
                fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(0f, 1f, timeElapsed / totalFadeTime)));
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(FadeOutTransition());
    }

    private IEnumerator FadeOutTransition()
    {
        timeElapsed = 0f;
        float totalFadeTime = fadeTransitionTime;

        while (timeElapsed < totalFadeTime)
        {
            if (timeElapsed < fadeTransitionTime)
            {
                fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(1f, 0f, timeElapsed / totalFadeTime)));
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        UpdateGameState(GameState.Exploration);
    }
    #endregion

    #region Death System
    public void GameOver()
    {
        UpdateGameState(GameState.Panel);
        ShakeCamera(1.5f, 3f);
        StartCoroutine(WaitToDeathPanel());
    }

    private IEnumerator WaitToDeathPanel()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeInDeathPanel());
    }

    private IEnumerator FadeInDeathPanel()
    {
        UpdateGameState(GameState.Panel);

        timeElapsed = 0f;
        float totalFadeTime = 3f;

        while (timeElapsed < totalFadeTime)
        {
            if (timeElapsed < 3f)
            {
                fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(0f, 1f, timeElapsed / totalFadeTime)));
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        deathText.color = new Color(255f, 255f, 255f, 1);
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Camera Shake
    public void ShakeCamera(float intensity, float time)
    {
        //Shake camera depending on intensity and how long to shake
        virtualCameraPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCameraPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void CameraShakeTimer()
    {
        //If camera shakeTimer is greater than 0, shake camera
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                virtualCameraPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
    #endregion

    #region Win Panel
    public void TransitionToNextLevel()
    {
        StartCoroutine(LevelTransition());
    }

    private IEnumerator LevelTransition()
    {
        UpdateGameState(GameState.Panel);

        timeElapsed = 0f;
        float totalFadeTime = 3f;

        while (timeElapsed < totalFadeTime)
        {
            if (timeElapsed < 3f)
            {
                fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(0f, 1f, timeElapsed / totalFadeTime)));
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        SceneManager.LoadScene((currentLevel+1).ToString());
    }
    #endregion
}

public enum GameState
{
    Exploration,
    Dialogue,
    Hiding,
    Panel
}

[System.Serializable]
public class Task
{
    //public int index;
    public string taskName;
    public bool isFinished;
}

public enum LevelName
{
    Level1,
    Level2,
    Level3
}