using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBait : MonoBehaviour
{
    [SerializeField] private Slider baitSlider;
    [SerializeField] private Image baitFillImage;

    [SerializeField] private TextMeshProUGUI baitText;

    [SerializeField] private int maxBaitValue = 10;
    [SerializeField] private int minBaitValue = 0;
    public int currentBaitValue;

    private void Start()
    {
        currentBaitValue = 10;
    }

    public void IncreaseBait()
    {
        baitSlider.value = baitSlider.maxValue;
        currentBaitValue = maxBaitValue;

        //Make fill image fully visible
        baitFillImage.color = new Color(baitFillImage.color.r, baitFillImage.color.g, baitFillImage.color.b, 1f);

        //Save bait

    }

    public void SetBait(int bait)
    {
        currentBaitValue = bait;
        baitSlider.value = currentBaitValue;

        CheckOutOfBait();
    }

    public void ConsumeBait()
    {
        currentBaitValue--;
        baitSlider.value = currentBaitValue;

        CheckOutOfBait();
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
        if (currentBaitValue <= minBaitValue)
        {
            currentBaitValue = minBaitValue;

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