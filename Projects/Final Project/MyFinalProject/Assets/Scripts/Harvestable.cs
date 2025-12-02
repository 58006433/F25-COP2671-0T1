using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [Header("Item Data")]
    public ItemData itemData;

    public void Collect()
    {
        // Later: Add to inventory system
        Debug.Log($"Collected: {itemData.itemName}");

        Destroy(gameObject);
    }
}
