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

    private void Start()
    {
        IsFishingActive = true;

        //Apply start position to fish and player
        isWaiting = true;
        StartCoroutine(WaitToMove());
    }

    private void Update()
    {
        if (isWaiting)
        {
            return;
        }
        else if (progressSlider.value == progressSlider.maxValue)
        {
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
        //Decide new move direction for fish
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
        //stop fishing game and fishing anim.
        //Show off fish
        //Back to default/idle mode
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(StartWaitTime);

        SetNewTarget();
        isWaiting = false;
    }
}
