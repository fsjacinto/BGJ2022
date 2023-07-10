using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class TransitionUI : MonoBehaviour {
//    [Header("Room Traverse")]
//    [SerializeField] private Image fadeTransitionImage;

//    private IEnumerator FadeInTransition() {
//        //GameManager.Instance.UpdateGameState(GameState.Transition);

//        timeElapsed = 0f;
//        float totalFadeTime = fadeTransitionTime;

//        while (timeElapsed < totalFadeTime) {
//            if (timeElapsed < fadeTransitionTime) {
//                fadeTransitionImage.color = new Color(0f, 0f, 0f, (Mathf.Lerp(0f, 1f, timeElapsed / totalFadeTime)));
//            }

//            timeElapsed += Time.deltaTime;

//            yield return null;
//        }

//        StartCoroutine(FadeOutTransition());
//    }
//}
