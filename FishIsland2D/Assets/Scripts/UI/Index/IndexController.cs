using UnityEngine;

public class IndexController : MonoBehaviour
{
    public bool IndexMenuActive = false;

    [SerializeField] private InventoryController inventoryControllerScript;
    [SerializeField] private BaitMenuController baitMenuControllerScript;

    public void OpenIndexMenu()
    {
        //Disable inventory and bait menu if they were active
        if (inventoryControllerScript.inventoryIsActive == true)
        {
            inventoryControllerScript.DisableInventory();
        }

        if (baitMenuControllerScript.baitMenuActive == true)
        {
            baitMenuControllerScript.CloseBaitMenu();
        }

        IndexMenuActive = true;
        this.gameObject.SetActive(true);
    }

    public void CloseIndexMenu()
    {
        IndexMenuActive = false;
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //Update the caught state of all index slots when the index menu is opened
        IndexSlot[] indexSlots = GetComponentsInChildren<IndexSlot>();
        for (int index = 0; index < indexSlots.Length; index++)
        {
            indexSlots[index].UpdateCaughtState();
        }
    }
}