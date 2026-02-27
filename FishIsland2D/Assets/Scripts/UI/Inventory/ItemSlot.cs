using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    private CaughtFish fishInfo;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        InventoryDescription.Instance.ShowFishInfo(fishInfo);
    }

    public void SetFish(CaughtFish fish)
    {
        fishInfo = fish;

        if (fish != null)
        {
            icon.sprite = fish.data.fishSprite;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public CaughtFish GetFish()
    {
        return fishInfo;
    }

    public void OnClick()
    {
        InventoryDescription.Instance.ShowFishInfo(fishInfo);
    }
}
