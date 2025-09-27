using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private Slider baitSlider;

    [SerializeField] private float maxBaitValue = 10f;
    [SerializeField] private float minBaitValue = 0f;
    private float currentBaitValue;

    public void IncreaseBait()
    {
        baitSlider.value = baitSlider.maxValue;
        currentBaitValue = maxBaitValue;
    }
}
