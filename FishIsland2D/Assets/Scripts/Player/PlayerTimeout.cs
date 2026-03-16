using System.Collections;
using UnityEngine;

public class PlayerTimeout : MonoBehaviour
{
    public bool HasCatchTimeout = false;
    [SerializeField] private GameObject FishGameObject;

    public IEnumerator FishTimeOut()
    {
        HasCatchTimeout = true;

        //Disable fish game here so coroutine works
        FishGameObject.SetActive(false);

        Debug.Log("before wait time");

        yield return new WaitForSeconds(2f);

        Debug.Log("after wait time");

        HasCatchTimeout = false;
    }
}