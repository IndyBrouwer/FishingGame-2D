using System.Collections;
using UnityEngine;

public class PlayerCatched : MonoBehaviour
{
    [Header("Caught Fish Settings")]
    [SerializeField] private Transform caughtSlot;
    [SerializeField] private FishDatabase fishDatabaseScript;
    private FishData selectedFish;

    [Header("Other Scripts")]
    [SerializeField] private InventoryPage InventoryPageScript;
    [SerializeField] private CaughtFish caughtFishScript;
    [SerializeField] private PlayerBait playerBaitScript;
    [SerializeField] private FishScaleHelper fishScaleHelperScript;

    public void DecideFish()
    {
        //Check if I even assigned fish to the list in inspector
        if (fishDatabaseScript.allFish.Count == 0)
        {
            Debug.LogWarning("No fish in fishpool!");
            return;
        }

        float totalChance = 0f;
        foreach (var fish in fishDatabaseScript.allFish)
        {
            totalChance += GetBoostedChance(fish);
        }

        //Pick a random value
        float randomPoint = Random.value * totalChance;

        //Set current fish to null, so it can be assigned to the randomly chosen fish in the loop
        selectedFish = null;

        //The to be total amount of all fish their catch rates
        float cumulative = 0f;

        foreach (var fish in fishDatabaseScript.allFish)
        {
            //Add the current fish from the list it's catch rate to the total list. Add bait boost if player has it.
            cumulative += GetBoostedChance(fish);

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
            selectedFish = fishDatabaseScript.allFish[0]; //If failed catch most common/standard fish
        }

        //Check if fish caught is the first of its type for index
        InventoryPage.Instance.IsFirstCatch(selectedFish.fishID);

        CaughtFish caughtFish = new(selectedFish);

        //Set the scale of the caught slot based on the size of the caught fish
        CalculateScale(caughtFish);

        SpriteRenderer caughtSlotSpriteRenderer = caughtSlot.GetComponent<SpriteRenderer>();

        //Set the sprite of the caught slot to the caught fish sprite
        caughtSlotSpriteRenderer.sprite = caughtFish.data.fishSprite;

        //Add to inventory
        InventoryPageScript.AddFish(caughtFish);
        
        SaveManager.Instance.SaveGame();

        StartCoroutine(ShowFishTime(caughtSlotSpriteRenderer));
    }

    private float GetBoostedChance(FishData fish)
    {
        float chance = fish.catchChance;

        if (playerBaitScript.currentBait == null)
        {
            Debug.Log("Player has no bait equipped, using base catch chance.");
            return chance;
        }

        Debug.Log("Premium bait equipped, applying catch chance boost.");

        switch (fish.fishTier)
        {
            case FishTier.Rare:
                chance *= playerBaitScript.currentBait.rareMultiplier;
                break;

            case FishTier.Epic:
                chance *= playerBaitScript.currentBait.epicMultiplier;
                break;

            case FishTier.Legendary:
                chance *= playerBaitScript.currentBait.legendaryMultiplier;
                break;
        }

        return chance;
    }

    private void CalculateScale(CaughtFish caughtFish)
    {
        float spriteScale = fishScaleHelperScript.GetSpriteScale(caughtFish);

        //Change scale from the sprite in the caught slot based on the size of the caught fish
        caughtSlot.localScale = new Vector3(spriteScale, spriteScale, 1f);
    }

    private IEnumerator ShowFishTime(SpriteRenderer caughtSlotSpriteRenderer)
    {
        AudioManager.Instance.sfxManager.PlayCaughtSound();

        yield return new WaitForSeconds(1.5f);

        caughtSlotSpriteRenderer.sprite = null;
    }
}