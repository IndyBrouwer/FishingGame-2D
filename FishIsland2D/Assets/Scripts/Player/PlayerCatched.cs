using System.Collections;
using UnityEngine;

public class PlayerCatched : MonoBehaviour
{
    [Header("Caught Fish Settings")]
    [SerializeField] private Transform caughtSlot;
    [SerializeField] private FishData[] fishPool;

    [SerializeField] private InventoryPage InventoryPageScript;
    [SerializeField] private CaughtFish caughtFishScript;

    public void DecideFish()
    {
        //Check if I even assigned fish to the list in inspector
        if (fishPool.Length == 0)
        {
            Debug.LogWarning("No fish in fishpool!");
            return;
        }

        float totalChance = 0f;
        foreach (var fish in fishPool)
        {
            totalChance += fish.catchChance;
        }

        //Pick a random value
        float randomPoint = Random.value * totalChance;

        //Create new var to store fish that is about to be caught
        FishData selectedFish = null;

        //The to be total amount of all fish their catch rates
        float cumulative = 0f;

        foreach (var fish in fishPool)
        {
            //Add the current fish from the list it's catch rate to the total list
            cumulative += fish.catchChance;

            //Check if the random chosen point from a catch rate falls in the min and max range of the total catch rates
            if (randomPoint <= cumulative)
            {
                //Assign randomly chosen fish to be the caught fish
                selectedFish = fish;

                //Stop loop as fish has been chosen
                break;
            }
        }

        if (selectedFish == null)
        {
            selectedFish = fishPool[0]; //If failed catch most common/standard fish
        }

        SpriteRenderer caughtSlotSpriteRenderer = caughtSlot.GetComponent<SpriteRenderer>();
        caughtSlotSpriteRenderer.sprite = selectedFish.fishSprite;

        //Add to inventory once
        CaughtFish caughtFish = new(selectedFish);
        InventoryPageScript.AddFish(caughtFish);

        StartCoroutine(ShowFishTime(caughtSlotSpriteRenderer));
    }

    private IEnumerator ShowFishTime(SpriteRenderer caughtSlotSpriteRenderer)
    {
        AudioManager.Instance.sfxManager.PlayCaughtSound();

        yield return new WaitForSeconds(1.5f);

        caughtSlotSpriteRenderer.sprite = null;
    }
}