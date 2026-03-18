using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public static InventoryPage Instance;

    public InventorySaveData currentSaveData = new InventorySaveData();

    [SerializeField] private ItemSlot ItemSlotScript;
    [SerializeField] private RectTransform contentPanel;

    [SerializeField] private PlayerBait playerBaitScript;

    List<ItemSlot> listOfItems = new List<ItemSlot>();

    public bool inventoryFull = false;

    private void Awake()
    {
        Instance = this;
    }

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

    public bool IsFirstCatch(string fishID)
    {
        if (!currentSaveData.caughtFishIDs.Contains(fishID))
        {
            currentSaveData.caughtFishIDs.Add(fishID);            

            return true;
        }
        return false;
    }

    public string SaveInventory()
    {
        currentSaveData.fishes = new List<SavedFish>();

        foreach (var slot in listOfItems)
        {
            var fish = slot.GetFish();

            if (fish != null)
            {
                currentSaveData.fishes.Add(new SavedFish
                {
                    fishID = fish.data.fishID,
                    size = fish.size
                });
            }
            else
            {
                currentSaveData.fishes.Add(new SavedFish
                {
                    fishID = "",
                    size = 0
                });
            }
        }

        //Save money
        currentSaveData.money = MoneyManager.Instance.currentMoney;

        //Save bait amount and bait type
        currentSaveData.bait = playerBaitScript.currentBaitAmount;
        currentSaveData.currentBait = playerBaitScript.currentBait;

        return JsonUtility.ToJson(currentSaveData);
    }

    public void LoadInventory(string jsonSaveData)
    {
        currentSaveData = JsonUtility.FromJson<InventorySaveData>(jsonSaveData);

        //Load Money
        MoneyManager.Instance.SetMoney(currentSaveData.money);

        //Load Bait
        playerBaitScript.SetBait(currentSaveData.bait, currentSaveData.currentBait);

        //Only count slots with fish
        int count = Mathf.Min(listOfItems.Count, currentSaveData.fishes.Count);

        for (int index = 0; index < count; index++)
        {
            var savedFish = currentSaveData.fishes[index];

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