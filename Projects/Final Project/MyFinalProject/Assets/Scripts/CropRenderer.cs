using UnityEngine;

public class CropRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SeedPacket seed;

    public int growthStage = 0;

    private CropBlock block;

    public bool fullyGrown =>
        seed != null && growthStage >= seed.growthSprites.Length - 1;

    public void Initialize(SeedPacket seedPacket, CropBlock blockRef)
    {
        seed = seedPacket;
        block = blockRef;
        growthStage = 0;
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        spriteRenderer.sprite = seed.growthSprites[growthStage];
    }

    public void Harvest()
    {
        if (!fullyGrown) return;

        Instantiate(seed.harvestablePrefab, transform.position, Quaternion.identity);

        block.isOccupied = false;
        block.seed = null;

        Destroy(gameObject);
    }
}
