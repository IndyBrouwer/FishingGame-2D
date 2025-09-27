using UnityEngine;
using UnityEngine.UI;

public class PlayerBait : MonoBehaviour
{
    [SerializeField] private Slider baitSlider;

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
}
