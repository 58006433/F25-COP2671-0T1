using UnityEngine;

public class CropBlock : MonoBehaviour
{
    public SeedPacket seedPacket;
    public int currentStage = 0;
    public float growthTimer = 0f;
    public float growthDuration = 5f;
    public bool isWatered = false;
    public bool isPlanted = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isPlanted && isWatered)
        {
            growthTimer += Time.deltaTime;

            if (growthTimer >= growthDuration)
            {
                AdvanceGrowthStage();
                growthTimer = 0f;
                isWatered = false;
            }
        }
    }

    public void TillSoil()
    {
        Debug.Log("Soil is tilled and ready!");
    }

    public void WaterSoil()
    {
        isWatered = true;
        Debug.Log("Soil watered!");
    }

    public void PlantSeed(SeedPacket seed)
    {
        seedPacket = seed;
        isPlanted = true;
        currentStage = 0;
        spriteRenderer.sprite = seed.growthSprites[currentStage];
    }

    public void AdvanceGrowthStage()
    {
        if (seedPacket == null) return;

        currentStage++;
        if (currentStage < seedPacket.growthSprites.Length)
            spriteRenderer.sprite = seedPacket.growthSprites[currentStage];
        else
            Debug.Log($"{seedPacket.cropName} is ready to harvest!");
    }

    public void HarvestPlants()
    {
        if (currentStage >= seedPacket.growthSprites.Length - 1)
        {
            //Instantiate(seedPacket.harvestablePrefab, transform.position, Quaternion.identity);
            ResetBlock();
        }
    }

    private void ResetBlock()
    {
        isPlanted = false;
        isWatered = false;
        currentStage = 0;
        seedPacket = null;
        spriteRenderer.sprite = null;
    }
}
