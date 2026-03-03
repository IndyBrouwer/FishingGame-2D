using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySaveData
{
    public List<SavedFish> fishes = new List<SavedFish>();
    public int money;
}