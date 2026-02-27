using UnityEngine;

[System.Serializable]
public class CaughtFish
{
    public FishData data;
    public float size;

    public CaughtFish(FishData fishData)
    {
        data = fishData;
        size = Random.Range(fishData.fishMinSize, fishData.fishMaxSize);
    }
}