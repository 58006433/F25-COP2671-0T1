using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [Header("Item Data")]
    public ItemData itemData;

    [Header("Debug Mode")]
    public bool debug = true;

    public void Collect()
    {
        if (debug)
        {
            Debug.Log("=== HARVESTABLE COLLECT() START ===");
            Debug.Log($"Object: {gameObject.name}");
            Debug.Log($"ItemData assigned?   {(itemData != null ? "YES" : "NO")}");
        }

        // 1. Check if ItemData exists
        if (itemData == null)
        {
            if (debug)
            {
                Debug.LogError("ERROR: itemData is NULL on this Harvestable! Inventory will NOT be updated.");
                Debug.Log("=== HARVESTABLE COLLECT() END (FAILED) ===");
            }
            Destroy(gameObject);
            return;
        }

        // 2. Check InventorySystem.Instance
        if (InventorySystem.Instance == null)
        {
            if (debug)
            {
                Debug.LogError("ERROR: InventorySystem.Instance is NULL!");
            }
            Destroy(gameObject);
            return;
        }

        // 3. Add item to inventory
        if (debug)
        {
            Debug.Log($"Attempting to add item: {itemData.itemName}, qty: {itemData.startingQuantity}");
        }

        InventorySystem.Instance.AddItem(itemData, itemData.startingQuantity);

        if (debug)
        {
            Debug.Log("SUCCESS: Item added to inventory!");
        }

        // 4. Destroy object
        if (debug)
        {
            Debug.Log("Destroying Harvestable object...");
            Debug.Log("=== HARVESTABLE COLLECT() END (SUCCESS) ===");
        }

        Destroy(gameObject);
    }
}
