using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryPageScript;
    [SerializeField] private GameObject InGameMenu;
    [SerializeField] private GameObject Item;

    private bool inventoryIsActive = false;
    [SerializeField] private int inventorySlots = 10;

    void Start()
    {
        inventoryPageScript.CreateInventoryLayout(inventorySlots);
    }

    void Update()
    {
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
}
