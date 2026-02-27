using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    public static InventoryDescription Instance;

    public Image fishIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI rarityText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFishInfo(CaughtFish fish)
    {
        if (fish == null)
        {
            return;
        }

        fishIcon.sprite = fish.data.fishSprite;

        nameText.text = fish.data.fishName;

        //F1 - Round size to 1 number after "."
        sizeText.text = fish.size.ToString("F1") + " cm";
        rarityText.text = fish.data.fishTier;
    }
}
