using UnityEngine;

[System.Serializable]
public class CropBlock
{
    // Tile information
    public Vector3Int cellPosition;
    public Vector3 worldPosition;

    // Soil status
    public bool isTilled = false;
    public bool isWatered = false;

    // Crop data
    public SeedPacket seed;
    public int growthStage = 0;            // Stage 0–3
    public float growthTimer = 0f;         // Time until next stage
    public bool isOccupied;

    // Reference to crop GameObject in the world
    public GameObject cropObject;

    // constructor
    public CropBlock(Vector3Int cell, Vector3 world)
    {
        cellPosition = cell;
        worldPosition = world;
    }

    // actions
    public void TillSoil()
    {
        if (!isTilled)
        {
            isTilled = true;
            Debug.Log("Soil tilled at " + cellPosition);
        }
    }

    public void WaterSoil()
    {
        isWatered = true;
        Debug.Log("Soil watered at " + cellPosition);
    }

    public void PlantSeed(SeedPacket packet, GameObject cropPrefab)
    {
        if (!isTilled || seed != null)
        {
            Debug.Log("Cannot plant here.");
            return;
        }

        seed = packet;
        growthStage = 0;
        growthTimer = packet.timePerStage;

        // spawn crop prefab in world
        cropObject = Object.Instantiate(
            cropPrefab,
            worldPosition,
            Quaternion.identity
        );

        CropRenderer renderer = cropObject.GetComponent<CropRenderer>();
        renderer.Initialize(seed, this);

        Debug.Log("Planted " + seed.cropName + " at " + cellPosition);
    }

    public void HarvestPlants()
    {
        if (seed == null || growthStage < seed.growthSprites.Length - 1)
        {
            Debug.Log("Crop not ready!");
            return;
        }

        // call harvest on CropRenderer (spawns Harvestable prefab)
        cropObject.GetComponent<CropRenderer>().Harvest();

        // reset tile
        seed = null;
        cropObject = null;
        growthStage = 0;
        growthTimer = 0f;
        isWatered = false;

        Debug.Log("Crop harvested at " + cellPosition);
    }

    // growth logic
    public void UpdateGrowth(float deltaTime)
    {
        if (seed == null) return;
        if (!isWatered) return; // no water = no growth

        growthTimer -= deltaTime;

        if (growthTimer <= 0f)
        {
            AdvanceGrowthStage();
            isWatered = false; // Requires new watering each stage
        }
    }

    private void AdvanceGrowthStage()
    {
        if (growthStage < seed.growthSprites.Length - 1)
        {
            growthStage++;
            growthTimer = seed.timePerStage;

            // Update crop sprite
            if (cropObject != null)
            {
                CropRenderer renderer = cropObject.GetComponent<CropRenderer>();
                renderer.growthStage = growthStage;
                renderer.UpdateSprite();
            }

            Debug.Log($"Crop at {cellPosition} grew to stage {growthStage}");
        }
        else
        {
            Debug.Log("Crop fully grown!");
        }
    }
}
