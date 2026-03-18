using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryPageScript;
    [SerializeField] private GameObject InGameMenu;
    [SerializeField] private GameObject Item;

    public bool inventoryIsActive = false;
    [SerializeField] private int inventorySlots = 16;

    [SerializeField] private PauseMenuController pauseMenuControllerScript;
    [SerializeField] private BaitMenuController baitMenuControllerScript;
    [SerializeField] private IndexController indexControllerScript;

    private void Awake()
    {
        inventoryPageScript.CreateInventoryLayout(inventorySlots);
    }

    private void Update()
    {
        //Inventory hardcoded controls
        if (Input.GetKeyDown(KeyCode.Tab) && pauseMenuControllerScript.pauseMenuIsActive != true)
        {
            if (!inventoryIsActive)
            {
                //Disable bait and index menus if they were active
                if (baitMenuControllerScript.baitMenuActive == true)
                {
                    baitMenuControllerScript.CloseBaitMenu();
                }
                if (indexControllerScript.IndexMenuActive == true)
                {
                    indexControllerScript.CloseIndexMenu();
                }

                inventoryIsActive = true;
                InGameMenu.SetActive(true);
            }
            else if (inventoryIsActive)
            {
                inventoryIsActive = false;
                InGameMenu.SetActive(false);
            }
        }
    }

    public void DisableInventory()
    {
        inventoryIsActive = false;
        InGameMenu.SetActive(false);
    }
}