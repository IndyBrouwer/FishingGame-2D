using UnityEngine;

public class BaitMenuController : MonoBehaviour
{
    public bool baitMenuActive = false;

    [SerializeField] private InventoryController inventoryControllerScript;
    [SerializeField] private IndexController indexControllerScript;

    public void OpenBaitMenu()
    {
        //Disable inventory if it was active
        if (inventoryControllerScript.inventoryIsActive == true)
        {
            inventoryControllerScript.DisableInventory();
        }

        if (indexControllerScript.IndexMenuActive == true)
        {
            indexControllerScript.CloseIndexMenu();
        }

        baitMenuActive = true;
        this.gameObject.SetActive(true);
    }

    public void CloseBaitMenu()
    {
        baitMenuActive = false;
        this.gameObject.SetActive(false);
    }
}