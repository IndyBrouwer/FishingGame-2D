using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool pauseMenuIsActive = false;

    [SerializeField] private InventoryController inventoryControllerScript;
    [SerializeField] private BaitMenuController baitMenuControllerScript;
    [SerializeField] private IndexController indexControllerScript;

    [SerializeField] private InventoryDescription inventoryDescriptionScript;

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseMenuIsActive = false;
    }

    void Update()
    {
        //Pause Menu hardcoded controls
        if (Input.GetKeyDown(KeyCode.Escape) && inventoryDescriptionScript.showingFish != true)
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
        if (inventoryDescriptionScript.showingFish == true)
        {
            return;
        }

        //Disable inventory menu and bait menu if they were active
        baitMenuControllerScript.CloseBaitMenu();
        inventoryControllerScript.DisableInventory();
        indexControllerScript.CloseIndexMenu();

        pauseMenuIsActive = true;
        pauseMenu.SetActive(true);
    }
}