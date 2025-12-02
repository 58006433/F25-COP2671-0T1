using UnityEngine;

[CreateAssetMenu(fileName = "New Seed Packet", menuName = "Farming/Seed Packet")]
public class SeedPacket : ScriptableObject
{
    public string cropName;
    public float timePerStage = 20f;

    [Header("Growth Stages")]
    public Sprite[] growthSprites;

    [Header("UI Image")]
    public Sprite coverImage;

    [Header("Harvestable Prefab")]
    public Harvestable harvestablePrefab;

    [Header("Item Produced")]
    public ItemData producedItem; 
}
