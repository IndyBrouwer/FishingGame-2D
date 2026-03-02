using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBait : MonoBehaviour
{
    [SerializeField] private Slider baitSlider;

    [SerializeField] private TextMeshProUGUI baitText;

    [SerializeField] private float maxBaitValue = 10f;
    [SerializeField] private float minBaitValue = 0f;
    public float currentBaitValue;

    private void Start()
    {
        currentBaitValue = 10f;
    }

    public void IncreaseBait()
    {
        baitSlider.value = baitSlider.maxValue;
        currentBaitValue = maxBaitValue;
    }

    public void ConsumeBait()
    {
        currentBaitValue--;
        baitSlider.value = currentBaitValue;
    }

    public void TellToGetBait()
    {
        baitText.gameObject.SetActive(true);

        baitText.text = "You need bait to fish!";

        StartCoroutine(WaitBaitMessage());
    }

    private IEnumerator WaitBaitMessage()
    {
        yield return new WaitForSeconds(1.5f);
        baitText.gameObject.SetActive(false);
    }
}