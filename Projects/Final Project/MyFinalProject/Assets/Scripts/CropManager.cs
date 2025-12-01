using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    [Header("References")]
    public Tilemap farmingTilemap;
    public GameObject cropPrefab;
    public TimeManager timeManager;

    // 2D grid storing crop blocks for each tile
    private CropBlock[,] cropGrid;

    // Track planted crops (easy update + growth)
    public List<CropBlock> plantedCrops = new List<CropBlock>();

    // Grid size info
    private int width;
    private int height;
    private Vector3Int origin;

    // Sunrise growth trigger control
    private bool sunriseGrowthApplied = false;   // NEW

    void Start()
    {
        CreateGridUsingTilemap(farmingTilemap);
    }

    void Update()   // NEW
    {
        HandleDailyGrowth();
    }

    // day night growth cycle
    private void HandleDailyGrowth()   // NEW
    {
        float t = timeManager.timeOfDay;

        // Sunrise is at 6:00
        if (t >= 6f && t < 6.2f)
        {
            if (!sunriseGrowthApplied)
            {
                ApplySunriseGrowth();
                sunriseGrowthApplied = true;
            }
        }
        else if (t > 6.2f)
        {
            // Reset for next day
            sunriseGrowthApplied = false;
        }
    }

    private void ApplySunriseGrowth() 
    {
        foreach (CropBlock block in plantedCrops)
        {
            // Tell each crop block to try advancing one growth cycle
            block.UpdateGrowth(1f);   
        }

        Debug.Log("Applied daily crop growth at sunrise.");
    }

    // creating the grid
    public void CreateGridUsingTilemap(Tilemap tilemap)
    {
        tilemap.CompressBounds();

        origin = tilemap.cellBounds.min;
        width = tilemap.cellBounds.size.x;
        height = tilemap.cellBounds.size.y;

        cropGrid = new CropBlock[width, height];

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            Vector3 worldPos = tilemap.CellToWorld(pos) + tilemap.tileAnchor;

            CreateGridBlock(tilemap, pos, worldPos, null);
        }

        Debug.Log("Crop grid successfully generated.");
    }

    // create individual grid blocks
    public void CreateGridBlock(Tilemap tilemap, Vector3Int location, Vector3 position, CropBlock gridBlock)
    {
        int x = location.x - origin.x;
        int y = location.y - origin.y;

        CropBlock newBlock = new CropBlock(location, position);

        cropGrid[x, y] = newBlock;
    }

    // get block from tile position
    public CropBlock GetBlockAtCell(Vector3Int cellPos)
    {
        int x = cellPos.x - origin.x;
        int y = cellPos.y - origin.y;

        if (x < 0 || y < 0 || x >= width || y >= height)
            return null;

        return cropGrid[x, y];
    }

    //planting section
    public void PlantCrop(Vector3Int cellPos, SeedPacket seed)
    {
        CropBlock block = GetBlockAtCell(cellPos);
        if (block == null || block.isOccupied) return;

        Vector3 worldPos = farmingTilemap.GetCellCenterWorld(cellPos);

        GameObject obj = Instantiate(cropPrefab, worldPos, Quaternion.identity);
        block.cropObject = obj;
        block.seed = seed;
        block.isOccupied = true;

        CropRenderer renderer = obj.GetComponent<CropRenderer>();
        renderer.Initialize(seed, block);

        AddToPlantedCrops(block);
    }

    //add or remove crops
    public void AddToPlantedCrops(CropBlock cropBlock)
    {
        if (!plantedCrops.Contains(cropBlock))
            plantedCrops.Add(cropBlock);
    }

    public void RemoveFromPlantedCrops(CropBlock cropBlock)
    {
        if (plantedCrops.Contains(cropBlock))
            plantedCrops.Remove(cropBlock);
    }
}
