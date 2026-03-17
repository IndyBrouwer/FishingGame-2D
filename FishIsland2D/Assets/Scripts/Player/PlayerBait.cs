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

    public void FillDefaultBait()
    {
        currentBait = defaultBait;

        baitSlider.value = baitSlider.maxValue;
        currentBaitAmount = maxBaitValue;

        //Make fill image fully visible
        baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 1f);

        UpdateUI();
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

            UpdateUI();
        }
        else
        {
            //Show issue UI text that you dont have enough money to buy premium bait and make it red so its readable.
            //Show issue UI text above the current menu (as of now displayed below it)

            //Warning sound
            AudioManager.Instance.sfxManager.PlayWarningSound();

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
        
        currentBait = baitType;
        currentBaitAmount = bait;

        baitSlider.value = currentBaitAmount;

        UpdateUI();

        CheckOutOfBait();
    }

    public void ConsumeBait()
    {
        currentBaitAmount--;

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

            //Bait fill image to orange and not see through
            baitFillImage.color = new Color(1f, 0.52f, 0f, 1f);
        }
        else
        {
            //Edit color of icon and slider fill to default color if not already

            //Bait icon to white (standard)
            baitIcon.color = Color.white;

            //Go back to default color and make sure its not see through
            baitFillImage.color = new Color(0.88f, 0.45f, 0.52f, 1f);
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