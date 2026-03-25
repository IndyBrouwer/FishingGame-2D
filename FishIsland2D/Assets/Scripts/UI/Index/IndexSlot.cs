using UnityEngine;
using UnityEngine.UI;

public class IndexSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private FishData currentFish;

    [SerializeField] private IndexDescription indexDescriptionScript;

    private Color fishColor;
    private string displayName;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);

        UpdateCaughtState();
    }

    private void HandleClick()
    {
        //Show fish name and fish icon in the index description panel
        
        //Check if fish from this slot has already been caught at some point
        bool isCaught = InventoryPage.Instance.currentSaveData.caughtFishIDs.Contains(currentFish.fishID);

        //Depending on if it's caught, pick a color for the icon
        if (isCaught == true)
        {
            fishColor = Color.white;
            fishColor.a = 1f;

            displayName = currentFish.fishName;
        }
        else
        {
            fishColor = Color.black;
            fishColor.a = 1f;

            displayName = "???";
        }

        //Send fish scriptable object and the picked color based on state to IndexDescription to display
        indexDescriptionScript.ShowFishInfo(currentFish, fishColor, displayName);
    }

    //Get child it's image component and change color to black if the fish is not caught yet, and white if the fish is caught
    private void SetCaught(bool caught)
    {
        if (caught)
        {
            icon.color = Color.white;
        }
        else
        {
            icon.color = Color.black;
        }
    }

    public void UpdateCaughtState()
    {
        var saveData = InventoryPage.Instance.currentSaveData;

        if (saveData == null)
        {
            SetCaught(false);
            return;
        }

        bool isCaught = saveData.caughtFishIDs.Contains(currentFish.fishID);

        SetCaught(isCaught);
    }
}
