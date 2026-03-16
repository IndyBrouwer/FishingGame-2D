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

    private bool isFishing = false; //Animation related
    private bool isWaitingForBite = false; //Timing logic related
    private bool isInMinigame = false; //Avoid fish sessions from carrying things over

    private float timeTillCatch = 0f;
    private float catchDelay;


    [SerializeField] private GameObject fishingGame;

    [Header("Other Scripts")]
    [SerializeField] private PlayerCatched playerCatchedScript;
    [SerializeField] private PlayerBait playerBaitScript;

    public void Fishing()
    {
        if (!isFishing)
        {
            if (playerBaitScript.currentBaitAmount <= 0)
            {
                playerBaitScript.TellToGetBait();
                return;
            }

            //Start fishing attempt
            isFishing = true;
            isWaitingForBite = true;
            timeTillCatch = 0f;

            catchDelay = Random.Range(minCatchDelay, maxCatchDelay);

            StartCoroutine(ThrowFishingPole());
        }
        else
        {
            //Player wants to cancel fishing before mini-game starts
            if (!isInMinigame)
            {
                CancelFishing();
            }
        }
    }

    private void FixedUpdate()
    {
        //Only count down for bite if waiting and not in mini-game
        if (isWaitingForBite && !isInMinigame)
        {
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= catchDelay)
            {
                //Play fishbite/splashing sound
                AudioManager.Instance.sfxManager.PlaySplashSound();

                //PlayerFishing state
                isWaitingForBite = false;
                isInMinigame = true;
                timeTillCatch = 0f;

                //Activate mini-game
                fishingGame.gameObject.SetActive(true);
            }
        }
    }

    public void StartMiniGame()
    {
        isInMinigame = true;
    }

    public void CancelFishing()
    {
        Debug.Log("Canceled Fishing");

        isFishing = false;
        isWaitingForBite = false;
        isInMinigame = false;

        fishingGame.SetActive(false);
        playerAnimator.SetBool("isFishing", false);
        timeTillCatch = 0f;
    }

    public void FishingFailed()
    {
        isFishing = false;
        isWaitingForBite = false;
        isInMinigame = false;

        playerAnimator.SetBool("isFishing", false);
    }

    public void CaughtFish()
    {
        playerAnimator.SetBool("isFishing", false);

        isFishing = false;
        isWaitingForBite = false;
        isInMinigame = false;

        timeTillCatch = 0f;

        //Play Catch anim
        //playerAnimator.SetTrigger("CaughtFish");

        playerCatchedScript.DecideFish();
    }

    private IEnumerator ThrowFishingPole()
    {
        playerAnimator.SetTrigger("ThrowRod");

        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("isFishing", true);
    }
}