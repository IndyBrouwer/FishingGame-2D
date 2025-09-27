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

    private float targetY;

    [SerializeField] private PlayerFishing playerFishingScript;
    [SerializeField] private PlayerBait playerBaitScript;

    private void OnEnable()
    {
        // Reset progress each time the mini-game starts
        progressSlider.value = 0;

        // Reset waiting state
        isWaiting = true;
        StartCoroutine(WaitToMove());

        // Reset fish + player start positions if needed
        gameFish.anchoredPosition = new Vector2(gameFish.anchoredPosition.x, minHeight.anchoredPosition.y);
        gamePlayer.anchoredPosition = new Vector2(gamePlayer.anchoredPosition.x, minHeight.anchoredPosition.y);

        // Consume bait each time fishing starts
        playerBaitScript.ConsumeBait();

        IsFishingActive = true;
    }

    private void Update()
    {
        if (isWaiting)
        {
            return;
        }
        else if (progressSlider.value == progressSlider.maxValue)
        {
            //Reset so you dont catch a fish without doing the game next time
            progressSlider.value = 1; //Does not help, fishgame does show up now with this but the player loses as the game was still running instead of reset
            CaughtFish();
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

    private void CaughtFish()
    {
        IsFishingActive = false;

        //Connect back to playerFishing script
        playerFishingScript.CaughtFish();

        //Deactivate the fishGame (so OnEnable runs next time and player can keep playing)
        gameObject.SetActive(false);
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(StartWaitTime);

        SetNewTarget();
        isWaiting = false;
    }
}
