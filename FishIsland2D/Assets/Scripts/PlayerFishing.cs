using System.Collections;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject bobber;
    [SerializeField] private Transform fishingPoint;

    private bool isFishing = false;
    private bool poleBack = false;
    private float timeTillCatch = 0f;


    [SerializeField] private GameObject fishingGame;

    private void FixedUpdate()
    {
        if (isFishing)
        {
            //Start fishing game after random time from min and max time.
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= 3)
            {
                fishingGame.SetActive(true);
            }
        }
    }

    public void Fishing()
    {
        if (!isFishing)
        {
            Debug.Log("Entered fishing function");
            poleBack = true;
            playerAnimator.SetTrigger("SwingBack");

            StartCoroutine(ThrowFishingPole());
        }
        else if (isFishing)
        {
            //Reel bobber back in
            StopFishing();
        }
    }

    private void StopFishing()
    {
        Debug.Log("Stopped Fishing");

        isFishing = false;
        playerAnimator.SetBool("isFishing", false);
        fishingGame.SetActive(false);
        timeTillCatch = 0f;
    }

    private IEnumerator ThrowFishingPole()
    {
        yield return new WaitForSeconds(2.5f);

        poleBack = false;

        playerAnimator.SetBool("isFishing", true);
        isFishing = true;
    }
}
