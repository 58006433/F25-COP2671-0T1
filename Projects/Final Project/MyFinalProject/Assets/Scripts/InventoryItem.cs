using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ItemData data;
    public int quantity;

    public InventoryItem(ItemData item, int qty)
    {
        data = item;
        quantity = qty;
    }
}
