using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Scriptable Objects/FishData")]
public class FishData : ScriptableObject
{
    public string fishName;
    public GameObject fishPrefab;
    public Sprite fishSprite;
    public string fishTier; //Legendary, Epic, Rare, Uncommon, Common
    [Range(0f, 100f)] public float catchChance = 10f; //Higher = more likely the be caught


    public float fishMaxSize;
    public float fishMinSize;
}
