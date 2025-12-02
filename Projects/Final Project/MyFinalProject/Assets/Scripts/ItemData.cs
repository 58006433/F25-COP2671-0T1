using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Identity")]
    public string itemName;

    [Header("Visual")]
    public Sprite itemIcon;

    [Header("Defaults")]
    public int startingQuantity = 1;
}
