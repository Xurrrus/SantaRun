using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame() {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        Application.Quit();
    }
}