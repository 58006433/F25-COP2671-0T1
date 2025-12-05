using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // Prefab with Image + Text
    public Transform contentParent; // Panel with GridLayoutGroup

    private Dictionary<ItemData, GameObject> slotObjects = new Dictionary<ItemData, GameObject>();

    void Start()
    {
        if (InventorySystem.Instance != null)
            InventorySystem.Instance.OnInventoryChanged.AddListener(UpdateUI);

        UpdateUI();
    }

    void UpdateUI()
    {
        var items = InventorySystem.Instance.GetAllItems();

        // Add or update slots
        foreach (var kvp in items)
        {
            ItemData item = kvp.Key;
            InventoryItem invItem = kvp.Value;

            if (!slotObjects.ContainsKey(item))
            {
                GameObject slot = Instantiate(slotPrefab, contentParent);
                slotObjects[item] = slot;
            }

            GameObject s = slotObjects[item];
            Image icon = s.transform.Find("Icon").GetComponent<Image>();
            Text qtyText = s.transform.Find("Quantity").GetComponent<Text>();

            icon.sprite = item.itemIcon;
            qtyText.text = invItem.quantity.ToString();
        }
    }
}
