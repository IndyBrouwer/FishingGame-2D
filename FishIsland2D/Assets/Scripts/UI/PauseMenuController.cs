using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool pauseMenuIsActive = false;

    [SerializeField] private InventoryController inventoryControllerScript;
    [SerializeField] private BaitMenuController baitMenuControllerScript;
    [SerializeField] private IndexController indexControllerScript;

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseMenuIsActive = false;
    }

    void Update()
    {
        //Pause Menu hardcoded controls
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuIsActive)
            {
                //Disable inventory menu and bait menu if they were active
                baitMenuControllerScript.CloseBaitMenu();
                inventoryControllerScript.DisableInventory();
                indexControllerScript.CloseIndexMenu();

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
        //Disable inventory menu and bait menu if they were active
        baitMenuControllerScript.CloseBaitMenu();
        inventoryControllerScript.DisableInventory();
        indexControllerScript.CloseIndexMenu();

        pauseMenuIsActive = true;
        pauseMenu.SetActive(true);
    }
}