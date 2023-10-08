using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private Image borderImage;
    public event Action<InventoryItem> OnItemClicked, onItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnItemRightClicked;
    private bool empty = true;
    private void Awake()
    {

    }
    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int quantity)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        this.quantity.text = quantity + "";
        empty = false;
    }
    public void Select()
    {
        borderImage.enabled = true;
    }
    public void OnBeginDrag()
    {
        if (!empty)
        {
            OnItemBeginDrag?.Invoke(this);
        }
    }
    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }
    public void OnDrop()
    {
        onItemDroppedOn?.Invoke(this);
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnItemClicked?.Invoke(this);
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnItemRightClicked?.Invoke(this);
        }
    }
}
