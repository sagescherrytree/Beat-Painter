using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    
    
    [SerializeField] public GameObject LossScreen;
    [SerializeField] public GameObject WinScreen;

    public static UI manager;
    public TextMeshProUGUI lvlScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public int score;

    private void Awake() {
        manager = this;
    }

    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void ReplayLevel() {
        SetScore(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApp() {
        Application.Quit();
        Debug.Log("Application has quit");
    }

    public void LoseLevel() {
        LossScreen.SetActive(true);
    }

    public void WinLevel() {
        WinScreen.SetActive(true);
        scoreText.text = score.ToString();
    }

    public void SetScore(int amnt) {
        score = amnt;
        lvlScoreText.text = score.ToString();
    }

    public void IncreaseScore(int amnt) {
        score += amnt;
        lvlScoreText.text = score.ToString();
    }
}
