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

    public string SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();
        saveData.fishes = new List<SavedFish>();

        foreach (var slot in listOfItems)
        {
            var fish = slot.GetFish();

            if (fish != null)
            {
                saveData.fishes.Add(new SavedFish
                {
                    fishID = fish.data.fishID,
                    size = fish.size
                });
            }
            else
            {
                saveData.fishes.Add(new SavedFish
                {
                    fishID = "",
                    size = 0
                });
            }
        }

        //Save money
        saveData.money = MoneyManager.Instance.currentMoney;

        return JsonUtility.ToJson(saveData);
    }

    public void LoadInventory(string jsonSaveData)
    {
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(jsonSaveData);

        //Load Money
        MoneyManager.Instance.SetMoney(saveData.money);

        //Only count slots with fish
        int count = Mathf.Min(listOfItems.Count, saveData.fishes.Count);

        for (int index = 0; index < count; index++)
        {
            var savedFish = saveData.fishes[index];

            if (savedFish == null || string.IsNullOrEmpty(savedFish.fishID))
            {
                listOfItems[index].SetFish(null);
                continue;
            }

            FishData data = FishDatabase.Instance.GetFishByID(savedFish.fishID);

            if (data == null)
            {
                Debug.LogWarning($"Fish not found for ID: {savedFish.fishID}");
                listOfItems[index].SetFish(null);
                continue;
            }

            CaughtFish fish = new CaughtFish(data);
            fish.size = savedFish.size;

            listOfItems[index].SetFish(fish);
        }
    }
}