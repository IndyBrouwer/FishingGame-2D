using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] private InventoryPage inventoryPageScript;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        string jsonSaveData = inventoryPageScript.SaveInventory();
        PlayerPrefs.SetString("inventory", jsonSaveData);
        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("inventory"))
        {
            string jsonSaveData = PlayerPrefs.GetString("inventory");
            inventoryPageScript.LoadInventory(jsonSaveData);

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No save found");

            //Do whatever it does change on load/save for empty slots
            SaveGame();

            //Load again to update the inventory but now with the correct slot colors
            LoadGame();
        }
    }

    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteKey("inventory");
        PlayerPrefs.Save();
    }
}