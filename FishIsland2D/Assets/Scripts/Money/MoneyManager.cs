using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField] private int currentMoney = 0;
    [SerializeField] private TextMeshProUGUI moneyText;

    private int maxMoney = 9999;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;

        AudioManager.Instance.sfxManager.PlaySoldSound();

        if (currentMoney > maxMoney)
        {
            //Cap the money amount so it doesnt trip out
            currentMoney = maxMoney;
        }

        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney < amount)
        {
            return false;
        }

        currentMoney -= amount;
        UpdateMoneyUI();

        return true;
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = $"{currentMoney}";
    }

    public int GetMoney()
    {
        return currentMoney;
    }
}