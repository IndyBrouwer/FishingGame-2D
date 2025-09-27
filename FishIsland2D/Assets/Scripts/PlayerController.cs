using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject bobber;
    [SerializeField] private Transform fishingPoint;

    private bool isFishing = false;
    private bool poleBack = false;
    private float timeTillCatch = 0f;


    [SerializeField] private GameObject fishingGame;

    public void Fishing()
    {
        if (!isFishing)
        {
            poleBack = true;
            playerAnimator.SetTrigger("SwingBack");

            StartCoroutine(ThrowFishingPole());

            playerAnimator.SetBool("isFishing", true);
        }
        else if (isFishing)
        {
            //Reel bobber back in
        }

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

    private IEnumerator ThrowFishingPole()
    {
        yield return new WaitForSeconds(2.5f);

        poleBack = false;
        isFishing = true;
    }
}
