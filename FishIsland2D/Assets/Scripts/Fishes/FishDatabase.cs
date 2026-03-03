using System.Collections.Generic;
using UnityEngine;

public class FishDatabase : MonoBehaviour
{
    public static FishDatabase Instance;

    public List<FishData> allFish;

    private Dictionary<string, FishData> fishLookUp;

    private void Awake()
    {
        Instance = this;

        fishLookUp = new Dictionary<string, FishData>();

        foreach (var fish in allFish)
        {
            fishLookUp[fish.fishID] = fish;
        }
    }

    public FishData GetFishByID(string fishID)
    {
        return fishLookUp[fishID];
    }
}