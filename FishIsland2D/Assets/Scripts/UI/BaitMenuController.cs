using UnityEngine;

public class BaitMenuController : MonoBehaviour
{
    public void OpenBaitMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseBaitMenu()
    {
        this.gameObject.SetActive(false);
    }
}