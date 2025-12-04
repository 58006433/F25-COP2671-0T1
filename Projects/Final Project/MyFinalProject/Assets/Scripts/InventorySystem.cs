using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    // Key = ItemData, Value = InventoryItem
    private Dictionary<ItemData, InventoryItem> items =
        new Dictionary<ItemData, InventoryItem>();

    public UnityEvent OnInventoryChanged = new UnityEvent();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(ItemData item, int quantity = 1)
    {
        if (item == null) return;

        // Item already exists → increase quantity
        if (items.ContainsKey(item))
        {
            items[item].quantity += quantity;
        }
        else
        {
            items[item] = new InventoryItem(item, quantity);
        }

        Debug.Log($"Inventory: Added {quantity}x {item.itemName}");

        OnInventoryChanged.Invoke();
    }

    public Dictionary<ItemData, InventoryItem> GetAllItems()
    {
        return items;
    }
}
