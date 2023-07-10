using DialogueSystem;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private Image actorImage;
    [SerializeField] private TextMeshProUGUI actorName;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private float wordSpeed;

    public void ToggleUI(bool show) {
        dialogueContainer.SetActive(show);
    }

    public void DisplayMessage(MessageInfo messageInfo) {
        messageText.text = messageInfo.message;

        actorName.text = messageInfo.actorInfoSO.actorName;
        actorImage.sprite = messageInfo.actorInfoSO.actorSprite;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(messageInfo.message));

    }

    public IEnumerator TypeSentence(string sentence) {
        messageText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            messageText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
}
