using UnityEngine;

[CreateAssetMenu(fileName = "BaitData", menuName = "Scriptable Objects/BaitData")]
public class BaitData : ScriptableObject
{
    public string baitName;
    public string baitID;
    public string baitTier;

    public int buyPrice;

    public float rareMultiplier = 1f;
    public float epicMultiplier = 1f;
    public float legendaryMultiplier = 1f;
}