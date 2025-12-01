using UnityEngine;

[CreateAssetMenu(fileName = "New Seed Packet", menuName = "Farming/Seed Packet")]
public class SeedPacket : ScriptableObject
{
    public string cropName;
    public float timePerStage = 20f; // seconds per growth stage

    [Header("Growth Stages")]
    public Sprite[] growthSprites; // stage 0 → 4

    [Header("UI Image")]
    public Sprite coverImage;

    [Header("Harvestable Prefab")]
    public Harvestable harvestablePrefab;
}
