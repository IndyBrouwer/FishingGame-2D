using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBait : MonoBehaviour
{
    [Header("Bait UI")]
    [SerializeField] private Slider baitSlider;
    [SerializeField] private Image baitFillImage;
    [SerializeField] private Image baitIcon;
    [SerializeField] private TextMeshProUGUI baitText;

    [Header("Bait Scriptable Objects")]
    [SerializeField] private BaitData defaultBait;
    [SerializeField] private BaitData premiumBait;
    public BaitData currentBait;

    [Header("Bait Amount")]
    [SerializeField] private int maxBaitValue = 10;
    [SerializeField] private int minBaitValue = 0;
    public int defaultBaitAmount;
    public int premiumBaitAmount;
    public int currentBaitAmount;

    //private void Start()
    //{
    //    currentBaitAmount = maxBaitValue;
    //    currentBait = defaultBait;
    //}

    //public void IncreaseBait()
    //{
    //    baitSlider.value = baitSlider.maxValue;
    //    currentBaitAmount = maxBaitValue;

    //    //Make fill image fully visible
    //    baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 1f);
    //}

    public void FillDefaultBait()
    {
        currentBait = defaultBait;

        baitSlider.value = baitSlider.maxValue;
        currentBaitAmount = maxBaitValue;

        //Make fill image fully visible
        baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 1f);
    }

    public void FillPremiumBait()
    {
        //Remove cost premium bait of player wallet
        if (MoneyManager.Instance.SpendMoney(premiumBait.buyPrice) == true)
        {
            currentBait = premiumBait;

            baitSlider.value = baitSlider.maxValue;
            currentBaitAmount = maxBaitValue;

            //Make fill image fully visible
            baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 1f);
        }
        else
        {
            //Show issue UI text that you dont have enough money to buy premium bait and make it red so its readable.
            //Show issue UI text above the current menu (as of now displayed below it)

            return;
        }
    }

    public void SetBait(int bait, BaitData baitType)
    {
        if (baitType == null)
        {
            baitType = defaultBait;
            bait = maxBaitValue;
            currentBaitAmount = bait;

            baitSlider.value = currentBaitAmount;

            return;
        }

        currentBaitAmount = bait;
        currentBait = baitType;
        baitSlider.value = currentBaitAmount;

        CheckOutOfBait();
    }

    public void ConsumeBait()
    {
        if (currentBait == premiumBait)
        {
            currentBaitAmount--;
        }
        else
        {
            currentBaitAmount--;
        }

        UpdateUI();

        CheckOutOfBait();
    }

    public void UpdateUI()
    {
        //Update bait slider value to current bait amount
        baitSlider.value = currentBaitAmount;

        if (currentBait == premiumBait)
        {
            //Edit color of icon and slider fill to gold

            //Bait icon to gold
            baitIcon.color = new Color(1f, 0.84f, 0f);

            //Bait fill image to gold and not see through
            baitFillImage.color = new Color(1f, 0.84f, 0f);
        }
        else
        {
            //Edit color of icon and slider fill to default color if not already

            //Bait icon to white (standard)
            baitIcon.color = Color.white;

            //Bait fill image to pink (standard) and not see through
            //baitFillImage.color = new Color(226, 114, 133);

            baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 1f);
        }
    }

    public void TellToGetBait()
    {
        AudioManager.Instance.sfxManager.PlayWarningSound();

        baitText.gameObject.SetActive(true);

        baitText.text = "You need bait to fish!";

        StartCoroutine(WaitBaitMessage());
    }

    public void CheckOutOfBait()
    {
        if (currentBaitAmount <= minBaitValue)
        {
            currentBaitAmount = minBaitValue;

            //Make fill image transparent
            baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 0f);
        }
    }

    private IEnumerator WaitBaitMessage()
    {
        yield return new WaitForSeconds(1.5f);
        baitText.gameObject.SetActive(false);
    }
}