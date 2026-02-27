using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private ItemSlot ItemSlotScript;
    [SerializeField] private RectTransform contentPanel;

    List<ItemSlot> listOfItems = new List<ItemSlot>();

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
                return;
            }
        }

        Debug.Log("Inventory full!");
    }
}