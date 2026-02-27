using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        #if UNITY_WEBGL
                        // For WebGL builds, just show a message or redirect to a main menu.
                        Debug.Log("Quit not supported in WebGL. Returning to main menu...");
                        // Example: return to main menu instead of quitting.
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        #else
                Application.Quit();
        #endif
    }
}