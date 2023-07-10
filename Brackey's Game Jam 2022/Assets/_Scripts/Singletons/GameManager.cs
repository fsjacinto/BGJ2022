using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Threading.Tasks;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    [field:SerializeField] public GameState currentState { get; private set; }
    public Animator playerAnimator;
    [SerializeField] private string nextLevel;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin virtualCameraPerlin;
    private float shakeTimer;

    #endregion

    async void Start()
    {
        await TransitionManager.Instance.FadeOutLevelStartTransition();
    }

    private void Update()
    {
        CameraShakeTimer();
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;

        if(currentState == GameState.Dialogue)
        {
              playerAnimator.SetTrigger("Idle");
        }
    }

    #region Game Over
    public async void GameOver()
    {
        if (!AudioManager.Instance.GetSource("Scream").isPlaying)
        {
            AudioManager.Instance.Play("Scream");
        }
        ShakeCamera(1.5f, 3f);

        await TransitionManager.Instance.FadeInDeathTransition();
        await Task.Delay(2000);
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
    //public void TransitionToNextLevel()
    //{
    //    StartCoroutine(LevelTransition());
    //}

    //private IEnumerator LevelTransition()
    //{
    //    UpdateGameState(GameState.Transition);

    //    timeElapsed = 0f;
    //    float totalFadeTime = 3f;

    //    while (timeElapsed < totalFadeTime)
    //    {
    //        if (timeElapsed < 3f)
    //        {
    //            fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(0f, 1f, timeElapsed / totalFadeTime)));
    //        }

    //        timeElapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    SceneManager.LoadScene(nextLevel);
    //}
    #endregion
}