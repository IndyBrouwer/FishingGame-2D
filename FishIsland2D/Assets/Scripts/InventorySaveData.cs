using System.Collections.Generic;

[System.Serializable]
public class InventorySaveData
{
    public List<SavedFish> fishes = new List<SavedFish>();  //For Inventory
    public List<string> caughtFishIDs = new List<string>(); //For Index

    public int money;
    public int bait;
    public BaitData currentBait;
}