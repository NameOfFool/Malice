using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventoryPage inventoryUI;
    private PlayerInput playerInput;
    public int inventorySize = 10;
    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        playerInput = GetComponent<PlayerInput>();
    }

    public void onInventoryCast(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(!inventoryUI.isActiveAndEnabled)
            {
                inventoryUI.Show();
                playerInput.SwitchCurrentActionMap("UI");
            }
            else
            {
                inventoryUI.Hide();
                playerInput.SwitchCurrentActionMap("PlayerM");
            }
        }
    }
}
