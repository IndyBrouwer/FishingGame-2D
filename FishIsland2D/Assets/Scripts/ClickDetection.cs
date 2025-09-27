using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] private PlayerFishing playerFishingScript;

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && FishGame.IsFishingActive == false)
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
                else if (hit.CompareTag("Button"))
                {
                    return;
                }
                else
                {
                    //Start fishing
                    playerFishingScript.Fishing();
                }
            }
            else
            {
                //If clicked in empty space start fishing
                playerFishingScript.Fishing();
            }
        }
    }
}
