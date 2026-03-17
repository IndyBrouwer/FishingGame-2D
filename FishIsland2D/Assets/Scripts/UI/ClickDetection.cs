using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickDetection : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private PlayerFishing playerFishingScript;
    [SerializeField] private InventoryController inventoryControllerScript;
    [SerializeField] private InventoryPage inventoryPageScript;
    [SerializeField] private PlayerTimeout playerTimeoutScript;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private TextMeshProUGUI notifyText;
    [SerializeField] private float notifyTextDuration = 2f;

    private void Update()
    {
        //If inventory is opened ignore controls
        if (inventoryControllerScript.inventoryIsActive || settingsMenu.activeSelf)
        {
            return;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            //If player is already fishing, ignore
            if (FishGame.IsFishingActive == true)
            {
                return;
            }
            //If player is on catch timeout, ignore
            else if (playerTimeoutScript.HasCatchTimeout == true)
            {
                return;
            }
            //Check if UI was clicked
            else if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            DecideClickAction();
        }
    }

    private void DecideClickAction()
    {
        //Convert mouse position to world point
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //Perform 2D raycast
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null)
        {
            if (hit.CompareTag("PosSwitch"))
            {
                // Switch player position to other side of island
            }
            else
            {
                if (inventoryPageScript.InventoryIsFull() == true)
                {
                    AudioManager.Instance.sfxManager.PlayWarningSound();

                    //Show text inventory full
                    notifyText.gameObject.SetActive(true);
                    notifyText.text = "Your inventory is full!";

                    StartCoroutine(HideNotifyText());
                }
                else
                {
                    //Start fishing
                    playerFishingScript.Fishing();
                }
            }
        }
        else
        {
            if (inventoryPageScript.InventoryIsFull() == true)
            {
                //Show text inventory full
                notifyText.gameObject.SetActive(true);
                notifyText.text = "Your inventory is full!";

                StartCoroutine(HideNotifyText());
            }
            else
            {
                //If clicked in empty space start fishing
                playerFishingScript.Fishing();
            }
        }
    }
    
    private IEnumerator HideNotifyText()
    {
        yield return new WaitForSeconds(notifyTextDuration);

        notifyText.gameObject.SetActive(false);
    }
}