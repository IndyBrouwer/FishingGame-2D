using UnityEngine;

public class DisableMenus : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryControllerScript;
    [SerializeField] private PauseMenuController pauseMenuControllerScript;
    [SerializeField] private BaitMenuController baitMenuControllerScript;

    public void DisableAllMenus()
    {
        inventoryControllerScript.DisableInventory();
        pauseMenuControllerScript.HideThisMenu();
        baitMenuControllerScript.CloseBaitMenu();
    }
}