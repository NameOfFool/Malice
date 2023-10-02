using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    List<InventoryItem> listOfItems = new List<InventoryItem>();
    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentPanel.transform);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
        }
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
