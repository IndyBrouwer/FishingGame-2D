using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] private PlayerFishing playerFishingScript;

    private void FixedUpdate()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && FishGame.IsFishingActive == false)
        {
            //Convert mouse position to world point
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            //Perform 2D raycast
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("PosSwitch"))
                {
                    // Switch player position to other side of island
                }
                else
                {
                    //Start fishing
                    playerFishingScript.Fishing();
                }
            }
        }
    }
}
