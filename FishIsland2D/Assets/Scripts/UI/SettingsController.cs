using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject warningMenu;
    [SerializeField] private GameObject defaultMenu;

    [Header("Warning Menu Parts")]
    [SerializeField] private GameObject warningTitle;
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private GameObject warningButtons;

    private void Awake()
    {
        LeaveWarningMenu();
    }

    public void EnableWarningMenu()
    {
        //Turn default menu off
        defaultMenu.SetActive(false);

        //Set Context Message
        explanationText.text = "You are about to delete your save file!\n\n" + "If you do not want to lose your game it's progress, please press CANCEL.";

        //Turn warning menu on
        warningMenu.SetActive(true);

        warningTitle.SetActive(true);
        warningButtons.SetActive(true);
    }

    public void LeaveWarningMenu()
    {
        //Turn warning menu off
        warningMenu.SetActive(false);

        //Turn default menu back on
        defaultMenu.SetActive(true);
    }

    public void DeleteSaveFile()
    {
        if (PlayerPrefs.HasKey("inventory")) {

            //Call for the deleteSaveData function
            SaveManager.Instance.DeleteSaveData();

            //Disable the no longer needed UI
            warningTitle.SetActive(false);
            warningButtons.SetActive(false);

            //Confirmation text
            explanationText.text = "Save file has been successfully deleted.";

            //Wait few seconds so player can read it
            StartCoroutine(ReadingTime());
        }
        else
        {
            warningTitle.SetActive(false);
            warningButtons.SetActive(false);

            //Let player know no save file was found.
            explanationText.text = "No save file found to delete.";

            //Wait few seconds so player can read it
            StartCoroutine(ReadingTime());
        }
    }

    IEnumerator ReadingTime()
    {
        yield return new WaitForSeconds(3.5f);

        //Load start scene so the player has a visual that something happened.
        SceneManager.LoadScene("Start");
    }
}
