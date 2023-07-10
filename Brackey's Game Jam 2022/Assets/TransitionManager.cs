using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager> {
    //[SerializeField] private Image fadeTransitionImage;
    private float fadeTransitionTime = 0.5f;
    private float timeElapsed;
    //[SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private CanvasGroup levelStartTransitionGroup;
    [SerializeField] private CanvasGroup roomTransitionGroup;
    [SerializeField] private CanvasGroup deathTransitionGroup;

    //public void TriggerFadeTransition(float _fadeTransitionTime) {
    //    GameManager.Instance.UpdateGameState(GameState.Transition);
    //    fadeTransitionTime = _fadeTransitionTime;
    //    StartCoroutine(FadeInTransition());
    //}

    #region Room Transition
    public async void TriggerRoomTransition() {
        GameManager.Instance.UpdateGameState(GameState.Transition);
        await FadeInRoomTransition();
        await FadeOutRoomTransition();
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }

    public async Task FadeInRoomTransition() {
        //await fadeCanvasGroup.DOFade(1, fadeTransitionTime).AsyncWaitForCompletion();
        await roomTransitionGroup.DOFade(1, fadeTransitionTime).AsyncWaitForCompletion();
    }

    public async Task FadeOutRoomTransition() {
        //await fadeCanvasGroup.DOFade(1, fadeTransitionTime).AsyncWaitForCompletion();
        await roomTransitionGroup.DOFade(0, fadeTransitionTime).AsyncWaitForCompletion();
    }
    #endregion

    #region Level Start Transition
    public async Task FadeOutLevelStartTransition() {
        await Task.Delay(3000);
        await levelStartTransitionGroup.DOFade(0, fadeTransitionTime).AsyncWaitForCompletion();
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }
    #endregion

    #region Death Transition
    public async Task FadeInDeathTransition() {
        GameManager.Instance.UpdateGameState(GameState.Transition);
        await Task.Delay(2000);
        await deathTransitionGroup.DOFade(1, fadeTransitionTime).AsyncWaitForCompletion();
    }
    #endregion




    //private IEnumerator FadeInTransition() {
    //    //GameManager.Instance.UpdateGameState(GameState.Transition);

    //    timeElapsed = 0f;
    //    float totalFadeTime = fadeTransitionTime;

    //    while (timeElapsed < totalFadeTime) {
    //        if (timeElapsed < fadeTransitionTime) {
    //            fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(0f, 1f, timeElapsed / totalFadeTime)));
    //        }

    //        timeElapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    StartCoroutine(FadeOutTransition());
    //}

    //private IEnumerator FadeOutTransition() {
    //    timeElapsed = 0f;
    //    float totalFadeTime = fadeTransitionTime;

    //    while (timeElapsed < totalFadeTime) {
    //        if (timeElapsed < fadeTransitionTime) {
    //            fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(1f, 0f, timeElapsed / totalFadeTime)));
    //        }

    //        timeElapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    GameManager.Instance.UpdateGameState(GameState.Exploration);
    //}
}
