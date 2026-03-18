using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndexDescription : MonoBehaviour
{
    [Header("Inventory UI Elements")]
    public Sprite defaultIcon;
    public Image fishIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;

    public void ShowFishInfo(FishData currentFish, Color caughtColor)
    {
        //Set image
        fishIcon.sprite = currentFish.fishSprite;

        //Set color of image
        caughtColor.a = 1f;
        fishIcon.color = caughtColor;

        //Set written info
        nameText.text = currentFish.fishName;
        rarityText.text = currentFish.fishTier.ToString();
    }
}