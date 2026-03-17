using UnityEngine;

public class MaxMoney : MonoBehaviour
{
    void Start()
    {
        MoneyManager.Instance.AddMoney(9999);
    }
}
