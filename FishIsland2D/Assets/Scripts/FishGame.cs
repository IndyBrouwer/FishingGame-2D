using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishGame : MonoBehaviour
{
    public static bool IsFishingActive = false; //When true nothing in the scene will respond except if its part of the fishing game

    [SerializeField] private RectTransform gameFish;
    [SerializeField] private RectTransform gamePlayer;

    [SerializeField] private Slider progressSlider;

    [SerializeField] private RectTransform maxHeight;
    [SerializeField] private RectTransform minHeight;

    [SerializeField] private float fishMoveSpeed;
    [SerializeField] private float fishMinSpeed = 50f;
    [SerializeField] private float fishMaxSpeed = 100f;

    [SerializeField] private float playerMoveSpeed = 100f;

    [SerializeField] private float StartWaitTime = 1.5f;
    private bool isWaiting = false;

    [Header("Fail Fishing Settings")]
    private float failOnZeroTimer = 0f;
    [SerializeField] private float failTime = 3f; //Seconds

    private float targetY;
    public bool startedPlaying = false;

    [Header("Other Scripts")]
    [SerializeField] private PlayerFishing playerFishingScript;
    [SerializeField] private PlayerBait playerBaitScript;
    [SerializeField] private PlayerTimeout playerTimeoutScript;

    private void OnEnable()
    {
        //Stop all coroutines to avoid old ones still running when game was reanabled quickly
        StopAllCoroutines();

        startedPlaying = false;

        //Reset waiting state
        isWaiting = true;

        //Reset fail timer
        failOnZeroTimer = 0f;

        progressSlider.value = 0f;

        //Reset Y target from the fish
        targetY = minHeight.anchoredPosition.y;

        //Reset fish + player start positions if needed
        gameFish.anchoredPosition = new Vector2(gameFish.anchoredPosition.x, minHeight.anchoredPosition.y);
        gamePlayer.anchoredPosition = new Vector2(gamePlayer.anchoredPosition.x, minHeight.anchoredPosition.y);

        //Consume bait each time fishing starts
        playerBaitScript.ConsumeBait();

        IsFishingActive = true;

        StartCoroutine(WaitToMove());
    }

    private void OnDisable()
    {
        isWaiting = false;
        startedPlaying = false;
        failOnZeroTimer = 0f;
    }

    private void Update()
    {
        if (isWaiting || !startedPlaying)
        {
            return;
        }

        //Move the fish towards the target position
        Vector3 fishPosition = gameFish.anchoredPosition;
        fishPosition.y = Mathf.MoveTowards(fishPosition.y, targetY, fishMoveSpeed * Time.deltaTime);
        gameFish.anchoredPosition = fishPosition;

        //If close enough to target, pick a new move direction (targetY)
        if (Mathf.Abs(fishPosition.y - targetY) < 1f)
        {
            SetNewTarget();
        }

        //Connect player movement
        PlayerMovement();

        if (startedPlaying)
        {
            //Overlap Check and handling
            if (IsOverlapping())
            {
                progressSlider.value += Time.deltaTime; //Increase cuz overlapping
            }
            else
            {
                progressSlider.value -= Time.deltaTime; //Decrease cuz not overlapping
            }

            //Make it so the value cant be outside of the set min and max values of slider
            progressSlider.value = Mathf.Clamp(progressSlider.value, 0, progressSlider.maxValue);

            if (progressSlider.value <= 0f)
            {
                failOnZeroTimer += Time.deltaTime;

                if (failOnZeroTimer >= failTime)
                {
                    Debug.Log("Failed fishing game due to slider staying at 0 too long");
                    FailedGame();
                    return;
                }
            }
            else
            {
                failOnZeroTimer = 0f;
            }

            if (progressSlider.value >= progressSlider.maxValue)
            {
                //Reset so you dont catch a fish without doing the game next time
                FinishedGame();
                return;
            }
        }
    }

    private void SetNewTarget()
    {
        //Fish picks random direction within the height limits
        targetY = Random.Range(minHeight.anchoredPosition.y, maxHeight.anchoredPosition.y);


        //Pick a random speed between min and max fish speed each time fish changes direction
        fishMoveSpeed = Random.Range(fishMinSpeed, fishMaxSpeed);
    }

    private bool IsOverlapping()
    {
        //Centers
        float fishCenter = gameFish.anchoredPosition.y;
        float playerCenter = gamePlayer.anchoredPosition.y;

        //HalfHeights
        float fishHalfHeight = gameFish.rect.height / 2f;
        float playerHalfHeight = gamePlayer.rect.height / 2f;

        //Overlap is true if distance between centers is less than half heights together
        return Mathf.Abs(fishCenter - playerCenter) < (fishHalfHeight + playerHalfHeight);
    }

    private void PlayerMovement()
    {
        Vector3 playerPosition = gamePlayer.anchoredPosition;

        if (Input.GetMouseButton(0)) // Left mouse button held
        {
            // Move player up at fishMoveSpeed
            playerPosition.y += playerMoveSpeed * Time.deltaTime;

            // Clamp so it doesn't go above maxHeight
            playerPosition.y = Mathf.Clamp(playerPosition.y, minHeight.anchoredPosition.y, maxHeight.anchoredPosition.y);
        }
        else // Mouse released
        {
            // Move player back down toward minHeight
            playerPosition.y = Mathf.MoveTowards(playerPosition.y, minHeight.anchoredPosition.y, playerMoveSpeed * Time.deltaTime);
        }

        gamePlayer.anchoredPosition = playerPosition;
    }

    private void FinishedGame()
    {
        IsFishingActive = false;
        startedPlaying = false;
        isWaiting = false;
        failOnZeroTimer = 0f;

        //Reset progress
        progressSlider.value = 0;
        progressSlider.value = Mathf.Clamp(progressSlider.value, 0, progressSlider.maxValue);

        //Connect back to playerFishing script
        playerFishingScript.CaughtFish();

        //Enable catch timeout to prevent immediately starting another game
        playerTimeoutScript.StartCoroutine(playerTimeoutScript.FishTimeOut());
    }

    private void FailedGame()
    {
        IsFishingActive = false;
        startedPlaying = false;
        isWaiting = false;

        failOnZeroTimer = 0f;

        //Reset progress
        progressSlider.value = 0;
        progressSlider.value = Mathf.Clamp(progressSlider.value, 0, progressSlider.maxValue);

        //Play fail sound
        AudioManager.Instance.sfxManager.PlayFailedCatchSound();

        //Cancel fishing anim
        playerFishingScript.FishingFailed();

        //Enable catch timeout to prevent immediately starting another game
        playerTimeoutScript.StartCoroutine(playerTimeoutScript.FishTimeOut());
    }  

    IEnumerator WaitToMove()
    {
        yield return null;

        progressSlider.value = 0f;

        yield return new WaitForSeconds(StartWaitTime);

        SetNewTarget();
        isWaiting = false;

        startedPlaying = true;

        playerFishingScript.StartMiniGame();
    }
}