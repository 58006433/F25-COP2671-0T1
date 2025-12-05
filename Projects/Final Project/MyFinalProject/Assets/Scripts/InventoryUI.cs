using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;          // Prefab with Image + Text
    public Transform contentParent;        // Panel with GridLayoutGroup

    private Dictionary<ItemData, GameObject> slotObjects = new Dictionary<ItemData, GameObject>();

    void Start()
    {
        if (InventorySystem.Instance != null)
            InventorySystem.Instance.OnInventoryChanged.AddListener(UpdateUI);

        UpdateUI();
    }

    void UpdateUI()
    {
        if (InventorySystem.Instance == null) return;

        var currentItems = InventorySystem.Instance.GetAllItems();

        var keysToRemove = new List<ItemData>();
        foreach (var kvp in slotObjects)
        {
            if (!currentItems.ContainsKey(kvp.Key))
            {
                Destroy(kvp.Value);
                keysToRemove.Add(kvp.Key);
            }
        }
        foreach (var key in keysToRemove)
            slotObjects.Remove(key);

        foreach (var kvp in currentItems)
        {
            ItemData item = kvp.Key;
            InventoryItem invItem = kvp.Value;

            GameObject slot;
            if (!slotObjects.ContainsKey(item))
            {
                slot = Instantiate(slotPrefab, contentParent);
                slotObjects[item] = slot;
            }
            else
            {
                slot = slotObjects[item];
            }

            Image icon = slot.transform.Find("Icon").GetComponent<Image>();
            Text qtyText = slot.transform.Find("Quantity").GetComponent<Text>();

            if (icon != null) icon.sprite = item.itemIcon;
            if (qtyText != null) qtyText.text = invItem.quantity.ToString();
        }

        Debug.Log("Inventory UI updated. Total items: " + currentItems.Count);
    }
}
