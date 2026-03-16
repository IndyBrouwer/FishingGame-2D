using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Scriptable Objects/FishData")]
public class FishData : ScriptableObject
{
    public string fishName;
    public string fishID; //No spaces and no uppercase!
    public GameObject fishPrefab;
    public Sprite fishSprite;
    public FishTier fishTier; //Legendary, Epic, Rare, Uncommon, Common

    [Range(0f, 100f)] public float catchChance = 10f; //Higher = more likely to be caught

    public int fishMaxSize;
    public int fishMinSize;

    public int sellPrice;
}