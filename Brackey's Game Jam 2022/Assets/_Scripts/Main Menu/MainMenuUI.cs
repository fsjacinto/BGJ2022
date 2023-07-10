using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    
    [SerializeField] private Button quitBtn;

    private void Start() {
        playBtn.onClick.AddListener(() => { 
            SceneManager.LoadScene("Level1"); 
        });

        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
