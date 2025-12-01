using UnityEngine;

public class CropRenderer : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer spriteRenderer;     // Assigned in inspector
    public SeedPacket seed;                   // Assigned when planting

    [Header("Crop State")]
    public int growthStage = 0;               // Current stage
    public bool fullyGrown => 
        seed != null && growthStage >= seed.growthSprites.Length - 1;

    private CropBlock block;                  // Reference for CropManager

    // Called right after planting
    public void Initialize(SeedPacket seedPacket, CropBlock blockRef)
    {
        seed = seedPacket;
        block = blockRef;
        growthStage = 0;

        UpdateSprite();
    }

    public void Grow()
    {
        if (seed == null) return;

        growthStage = Mathf.Clamp(
            growthStage + 1, 
            0, 
            seed.growthSprites.Length - 1
        );

        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (seed == null || seed.growthSprites.Length == 0) return;

        spriteRenderer.sprite = seed.growthSprites[growthStage];
    }

    public void Harvest()
    {
        if (!fullyGrown) return;

        // Spawn harvestable
        Instantiate(
            seed.harvestablePrefab,
            transform.position,
            Quaternion.identity
        );

        // Tell CropManager this tile is now empty
        block.isOccupied = false;
        block.seed = null;

        Destroy(gameObject);
    }
}
