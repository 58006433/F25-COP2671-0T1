using UnityEngine;

[CreateAssetMenu(fileName = "NewSeedPacket", menuName = "Farming/Seed Packet")]
public class SeedPacket : ScriptableObject
{
    public string cropName;
    public Sprite[] growthSprites; // 0: seed, 1–3: growth stages
    public Sprite coverImage;
    //public Harvestable harvestablePrefab;
}
