using UnityEngine;

[System.Serializable]
public class CropBlock
{
    public Vector3Int cellPosition;
    public Vector3 worldPosition;

    public bool isTilled = false;
    public bool isWatered = false;

    public SeedPacket seed;
    public int growthStage = 0;
    public float growthTimer = 0f;
    public bool isOccupied = false;

    public GameObject cropObject;

    public CropBlock(Vector3Int cell, Vector3 world)
    {
        cellPosition = cell;
        worldPosition = world;
    }

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
        if (isTilled)
        {
            isWatered = true;
            Debug.Log("Soil watered at " + cellPosition);
        }
    }

    public void PlantSeed(SeedPacket packet, GameObject cropPrefab)
    {
        Debug.Log("PlantSeed() called on block " + cellPosition);

        if (!isTilled || isOccupied)
        {
            Debug.Log("Cannot plant here.");
            return;
        }

        seed = packet;
        isOccupied = true;
        growthStage = 0;
        growthTimer = packet.timePerStage;

        cropObject = Object.Instantiate(cropPrefab, worldPosition, Quaternion.identity);

        CropRenderer r = cropObject.GetComponent<CropRenderer>();
        r.Initialize(seed, this);

        Debug.Log("Planted " + seed.cropName + " at " + cellPosition);
    }

    public void HarvestPlants()
    {
        if (seed == null || growthStage < seed.growthSprites.Length - 1)
        {
            Debug.Log("Crop not ready!");
            return;
        }

        cropObject.GetComponent<CropRenderer>().Harvest();

        seed = null;
        cropObject = null;
        growthStage = 0;
        growthTimer = 0f;
        isWatered = false;
        isOccupied = false;

        Debug.Log("Crop harvested at " + cellPosition);
    }

    public void UpdateGrowth(float deltaTime)
    {
        if (seed == null) return;
        if (!isWatered) return;

        growthTimer -= deltaTime;

        if (growthTimer <= 0f)
        {
            AdvanceGrowthStage();
            isWatered = false;
        }
    }

    private void AdvanceGrowthStage()
    {
        if (growthStage < seed.growthSprites.Length - 1)
        {
            growthStage++;
            growthTimer = seed.timePerStage;

            if (cropObject != null)
            {
                CropRenderer r = cropObject.GetComponent<CropRenderer>();
                r.growthStage = growthStage;
                r.UpdateSprite();
            }

            Debug.Log($"Crop at {cellPosition} grew to stage {growthStage}");
        }
        else
        {
            Debug.Log("Crop fully grown.");
        }
    }
}
