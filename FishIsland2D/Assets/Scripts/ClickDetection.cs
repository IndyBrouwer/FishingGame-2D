using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] private PlayerController playerControlScript;

    private void FixedUpdate()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray CamRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit CamHit;

            if (Physics.Raycast(CamRay, out CamHit))
            {
                if (CamHit.collider.CompareTag("PosSwitch"))
                {
                    //Switch player position to other side of island
                }
                else
                {
                    //Start fishing
                    playerControlScript.Fishing();
                }
            }
        }
    }
}
