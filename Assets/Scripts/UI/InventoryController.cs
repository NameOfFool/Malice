using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventoryPage inventoryUI;
    public int inventorySize = 10;
    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    public void onInventoryCast(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(!inventoryUI.isActiveAndEnabled)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
