using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool pauseMenuIsActive = false;

    void Update()
    {
        //Pause Menu hardcoded controls
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuIsActive)
            {
                pauseMenuIsActive = true;
                pauseMenu.SetActive(true);
            }
            else if (pauseMenuIsActive)
            {
                pauseMenuIsActive = false;
                pauseMenu.SetActive(false);
            }
        }
    }

    public void HideThisMenu()
    {
        pauseMenu.SetActive(false);
        pauseMenuIsActive = false;
    }

    public void ShowThisMenu()
    {
        pauseMenuIsActive = true;
        pauseMenu.SetActive(true);
    }
}