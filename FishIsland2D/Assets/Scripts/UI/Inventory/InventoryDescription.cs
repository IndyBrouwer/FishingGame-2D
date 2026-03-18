using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    public static InventoryDescription Instance;

    [Header("Caught Slot Settings")]
    [SerializeField] private Transform caughtSlot;
    [SerializeField] private float clearCaughtSlotDelay = 2.5f;

    [SerializeField] private InventoryController inventoryControllerScript;

    [Header("Inventory UI Elements")]
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
            ClearDescription();
            return;
        }

        selectedFish = fish;
        selectedSlot = slot;

        fishIcon.sprite = fish.data.fishSprite;

        nameText.text = fish.data.fishName;

        //F1 - Round size to 1 number after "."
        sizeText.text = fish.size.ToString("F1") + " cm";
        rarityText.text = fish.data.fishTier.ToString();

        valueText.text = $"{fish.data.sellPrice}";
    }

    public void ShowSelectedFish()
    {
        if (selectedFish == null)
        {
            return;
        }

        //Close inventory so the player can see the caught fish in the caught slot
        inventoryControllerScript.DisableInventory();

        SpriteRenderer caughtSlotSpriteRenderer = caughtSlot.GetComponent<SpriteRenderer>();
        caughtSlotSpriteRenderer.sprite = fishIcon.sprite;

        //Play sound effect for showing the fish
        AudioManager.Instance.sfxManager.PlayShowFishSound();

        //Wait time before clearing the caught slot
        StartCoroutine(WaitAndClearCaughtSlot());
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
        SaveManager.Instance.SaveGame();
    }

    private void ClearDescription()
    {
        nameText.text = "";
        sizeText.text = "";
        rarityText.text = "";
        valueText.text = "";
        fishIcon.sprite = defaultIcon;
    }

    IEnumerator WaitAndClearCaughtSlot()
    {
        yield return new WaitForSeconds(clearCaughtSlotDelay);

        SpriteRenderer caughtSlotSpriteRenderer = caughtSlot.GetComponent<SpriteRenderer>();
        caughtSlotSpriteRenderer.sprite = defaultIcon;
    }
}
