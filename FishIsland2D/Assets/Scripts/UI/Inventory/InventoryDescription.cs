using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    public static InventoryDescription Instance;

    public Sprite defaultIcon;
    public Image fishIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI valueText;

    private CaughtFish selectedFish;
    private ItemSlot selectedSlot;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFishInfo(CaughtFish fish, ItemSlot slot)
    {
        if (fish == null)
        {
            return;
        }

        selectedFish = fish;
        selectedSlot = slot;

        fishIcon.sprite = fish.data.fishSprite;

        nameText.text = fish.data.fishName;

        //F1 - Round size to 1 number after "."
        sizeText.text = fish.size.ToString("F1") + " cm";
        rarityText.text = fish.data.fishTier;
        
        valueText.text = $"{fish.data.sellPrice}";
    }

    public void SellSelectedFish()
    {
        if (selectedFish == null)
        {
            return;
        }

        int value = selectedFish.data.sellPrice;

        MoneyManager.Instance.AddMoney(value);

        selectedSlot.SetFish(null);

        selectedFish = null;
        selectedSlot = null;

        ClearDescription();
    }

    private void ClearDescription()
    {
        nameText.text = "";
        sizeText.text = "";
        rarityText.text = "";
        valueText.text = "";
        fishIcon.sprite = defaultIcon;
    }
}
