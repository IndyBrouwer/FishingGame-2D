using System.Collections;
using UnityEngine;


public class PlayerFishing : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform bobber;
    [SerializeField] private Transform waterSplash;

    [Header("Bite Timing Settings")]
    [SerializeField] private float minBiteDelay = 2f;
    [SerializeField] private float maxBiteDelay = 10f;
    private float timeTillBite = 0f;
    private float biteDelay;

    [SerializeField] private float minCatchWindow = 0.5f;
    [SerializeField] private float maxCatchWindow = 2f;
    private float catchWindowTimer = 0f;
    private float catchWindowDuration = 0f;
    private bool isInCatchWindow = false;

    [Header("Fishing State")]
    private bool isFishing = false; //Animation related
    private bool isWaitingForBite = false; //Timing logic related
    private bool isInMinigame = false; //Avoid fish sessions from carrying things over

    [Header("UI Elements")]
    private SpriteRenderer slotSprite;
    [SerializeField] private Sprite fishBiteSprite;
    [SerializeField] private GameObject fishingGame;
    public GameObject clickText;

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
            timeTillBite = 0f;

            biteDelay = Random.Range(minBiteDelay, maxBiteDelay);

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

    private void Update()
    {
        WaitForBite();

        CheckForCatch();
    }

    private void WaitForBite()
    {
        //Only count down for bite if waiting and not in mini-game
        if (isWaitingForBite && !isInMinigame)
        {
            timeTillBite += Time.deltaTime;
            if (timeTillBite >= biteDelay)
            {
                //Play fishbite/splashing sound
                AudioManager.Instance.sfxManager.PlaySplashSound();

                //PlayerFishing state
                isWaitingForBite = false;
                isInCatchWindow = true;

                //Random catch window
                catchWindowDuration = Random.Range(minCatchWindow, maxCatchWindow);
                catchWindowTimer = 0f;

                slotSprite = playerCatchedScript.caughtSlot.GetComponent<SpriteRenderer>();

                //Reset scale in case a fish has been caught before, so that the exclamation mark doesn't get scaled weirdly
                slotSprite.transform.localScale = new Vector3(1f, 1f, 1f);
                //Change caughtslot to an exclamation mark to indicate a bite of a fish
                slotSprite.sprite = fishBiteSprite;

                //Show CLICK text
                clickText.SetActive(true);
            }
        }
    }

    private void CheckForCatch()
    {
        if (isInCatchWindow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Caught in time!");

                isInCatchWindow = false;
                catchWindowTimer = 0f;

                isInMinigame = true;

                StartMiniGame();
            }

            catchWindowTimer += Time.deltaTime;

            if (catchWindowTimer >= catchWindowDuration)
            {
                Debug.Log("Missed the bite!");

                slotSprite.sprite = null;

                AudioManager.Instance.sfxManager.PlayFailedCatchSound();

                CancelFishing();
                return;
            }
        }
    }

    public void StartWaterSplashEffect()
    {
        waterSplash.gameObject.SetActive(true);
    }

    public void StartMiniGame()
    {
        isInMinigame = true;
        fishingGame.SetActive(true);
        slotSprite.sprite = null;
    }

    public void CancelFishing()
    {
        Debug.Log("Canceled Fishing");

        isFishing = false;
        isWaitingForBite = false;
        isInMinigame = false;
        isInCatchWindow = false;

        clickText.SetActive(false);
        waterSplash.gameObject.SetActive(false);

        fishingGame.SetActive(false);
        playerAnimator.SetBool("isFishing", false);
        timeTillBite = 0f;
        catchWindowTimer = 0f;
    }

    public void FishingFailed()
    {
        isFishing = false;
        isWaitingForBite = false;
        isInMinigame = false;
        isInCatchWindow = false;

        clickText.SetActive(false);
        waterSplash.gameObject.SetActive(false);

        playerAnimator.SetBool("isFishing", false);
        catchWindowTimer = 0f;
    }

    public void CaughtFish()
    {
        playerAnimator.SetBool("isFishing", false);

        isFishing = false;
        isWaitingForBite = false;
        isInMinigame = false;
        isInCatchWindow = false;

        waterSplash.gameObject.SetActive(false);

        timeTillBite = 0f;
        catchWindowTimer = 0f;

        playerCatchedScript.DecideFish();
    }

    private IEnumerator ThrowFishingPole()
    {
        playerAnimator.SetTrigger("ThrowRod");

        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("isFishing", true);
    }
}