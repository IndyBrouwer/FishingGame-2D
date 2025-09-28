using System.Collections;
using UnityEngine;


public class PlayerFishing : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform bobber;

    [Header("Catch Timing Settings")]
    [SerializeField] private float minCatchDelay = 2f;
    [SerializeField] private float maxCatchDelay = 10f;

    private bool isFishing = false;
    private bool poleBack = false;
    private float timeTillCatch = 0f;
    private float catchDelay;


    [SerializeField] private GameObject fishingGame;

    [SerializeField] private PlayerCatched playerCatchedScript;
    [SerializeField] private PlayerBait playerBaitScript;

    private void FixedUpdate()
    {
        if (isFishing)
        {
            catchDelay = Random.Range(minCatchDelay, maxCatchDelay);

            //Start fishing game after random time from min and max time.
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= catchDelay)
            {
                fishingGame.SetActive(true);
            }
        }
    }

    public void Fishing()
    {
        if (!isFishing && playerBaitScript.currentBaitValue > 0)
        {
            Debug.Log("Entered fishing function");
            poleBack = true;
            isFishing = true;
            playerAnimator.SetTrigger("SwingBack");

            StartCoroutine(ThrowFishingPole());
        }
        else if (!isFishing && playerBaitScript.currentBaitValue == 0)
        {
            //Show text to get bait to fish
            playerBaitScript.TellToGetBait();
        }
        else if (isFishing)
        {
            //Reel bobber back in
            CancelFishing();
        }
    }

    public void CancelFishing()
    {
        Debug.Log("Canceled Fishing");

        isFishing = false;
        fishingGame.SetActive(false);
        playerAnimator.SetBool("isFishing", false);
        timeTillCatch = 0f;
    }

    public void CaughtFish()
    {
        isFishing = false;
        playerAnimator.SetBool("isFishing", false);
        playerAnimator.SetTrigger("CaughtFish");
        timeTillCatch = 0f;

        playerCatchedScript.DecideFish();
    }

    private IEnumerator ThrowFishingPole()
    {
        yield return new WaitForSeconds(2.5f);

        poleBack = false;

        playerAnimator.SetBool("isFishing", true);
    }
}
