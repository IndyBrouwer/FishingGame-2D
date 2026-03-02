using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private ItemSlot ItemSlotScript;
    [SerializeField] private RectTransform contentPanel;

    List<ItemSlot> listOfItems = new List<ItemSlot>();

    public bool inventoryFull = false;

    public bool InventoryIsFull()
    {
        for (int index = 0; index < listOfItems.Count; index++)
        {
            if (listOfItems[index].GetFish() == null)
            {
                return false;
            }
        }
        return true;
    }

    public void CreateInventoryLayout(int slotsAmount)
    {
        for (int index = 0; index < slotsAmount; index++)
        {
            ItemSlot uiItem = Instantiate(ItemSlotScript, contentPanel);
            listOfItems.Add(uiItem);
        }
    }

    public void AddFish(CaughtFish fish)
    {
        for (int index = 0; index < listOfItems.Count; index++)
        {
            if (listOfItems[index].GetFish() == null)
            {
                listOfItems[index].SetFish(fish);

                //Check if the inventory is full after adding the fish
                if (InventoryIsFull())
                {
                    inventoryFull = true;
                    Debug.Log("Inventory is now full!");
                }

                return;
            }
        }

        Debug.Log("Inventory full!");
    }
}