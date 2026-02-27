using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private ItemSlot ItemSlotScript;
    [SerializeField] private RectTransform contentPanel;

    List<ItemSlot> listOfItems = new List<ItemSlot>();
    private int currentIndex = 0;

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
        if (currentIndex < listOfItems.Count)
        {
            listOfItems[currentIndex].SetFish(fish);
            Debug.Log("Fish set in slot " + currentIndex);
            currentIndex++;
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }
}
