using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void ReplayLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApp() {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
