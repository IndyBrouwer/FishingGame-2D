using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    private FishData fishInfo;
    
    public void SetFish(FishData fish)
    {
        fishInfo = fish;

        if (fish != null)
        {
            icon.sprite = fish.fishSprite;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public FishData GetFish()
    {
        return fishInfo;
    }
}
