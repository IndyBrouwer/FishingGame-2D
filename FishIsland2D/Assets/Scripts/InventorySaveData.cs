using System.Collections.Generic;

[System.Serializable]
public class InventorySaveData
{
    public List<SavedFish> fishes = new List<SavedFish>();
    public int money;
    public int bait;
    public BaitData currentBait;
}