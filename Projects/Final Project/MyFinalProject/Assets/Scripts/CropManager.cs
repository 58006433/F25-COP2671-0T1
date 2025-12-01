using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    [Header("References")]
    public Tilemap farmingTilemap;
    public GameObject cropPrefab;

    // 2D grid storing crop blocks for each tile
    private CropBlock[,] cropGrid;

    // Track planted crops (easy update + growth)
    public List<CropBlock> plantedCrops = new List<CropBlock>();

    // Grid size info
    private int width;
    private int height;
    private Vector3Int origin;

    void Start()
    {
        CreateGridUsingTilemap(farmingTilemap);
    }

    // create grid from tilemap
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

    // create invividual grid blocks
    public void CreateGridBlock(Tilemap tilemap, Vector3Int location, Vector3 position, CropBlock gridBlock)
    {
        int x = location.x - origin.x;
        int y = location.y - origin.y;

        // Create new block
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

    public void PlantCrop(Vector3Int cellPos, SeedPacket seed)
{
    CropBlock block = GetBlockAtCell(cellPos);
    if (block == null || block.isOccupied) return;

    Vector3 worldPos = farmingTilemap.GetCellCenterWorld(cellPos);

    GameObject obj = Instantiate(cropPrefab, worldPos, Quaternion.identity);
    block.cropObject = obj;
    block.seed = seed;
    block.isOccupied = true;

    // Initialize CropRenderer
    CropRenderer renderer = obj.GetComponent<CropRenderer>();
    renderer.Initialize(seed, block);

    AddToPlantedCrops(block);
}

    // add planted crop
    public void AddToPlantedCrops(CropBlock cropBlock)
    {
        if (!plantedCrops.Contains(cropBlock))
            plantedCrops.Add(cropBlock);
    }

    // remove planted crop
    public void RemoveFromPlantedCrops(CropBlock cropBlock)
    {
        if (plantedCrops.Contains(cropBlock))
            plantedCrops.Remove(cropBlock);
    }
}
