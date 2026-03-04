using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryPageScript;
    [SerializeField] private GameObject InGameMenu;
    [SerializeField] private GameObject Item;

    public bool inventoryIsActive = false;
    [SerializeField] private int inventorySlots = 16;

    private void Awake()
    {
        inventoryPageScript.CreateInventoryLayout(inventorySlots);
    }

    void Update()
    {
        //Inventory hardcoded controls
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventoryIsActive)
            {
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