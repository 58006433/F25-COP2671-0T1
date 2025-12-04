using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CropManager : MonoBehaviour
{
    [Header("References")]
    public Tilemap farmingTilemap;
    public GameObject cropPrefab;

    private CropBlock[,] cropGrid;
    public List<CropBlock> plantedCrops = new List<CropBlock>();

    private int width;
    private int height;
    private Vector3Int origin;

    void Start()
{
    CreateGridUsingTilemap(farmingTilemap);

    DayNightEvents evt = FindFirstObjectByType<DayNightEvents>();
    if (evt != null)
        evt.OnSunrise.AddListener(ApplySunriseGrowth);
    else
        Debug.LogWarning("No DayNightEvents found in the scene.");
}

    public void CreateGridUsingTilemap(Tilemap tilemap)
    {
        tilemap.CompressBounds();

        origin = tilemap.cellBounds.min;
        width = tilemap.cellBounds.size.x;
        height = tilemap.cellBounds.size.y;

        cropGrid = new CropBlock[width, height];

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            Vector3 worldPos = tilemap.GetCellCenterWorld(pos);
            CreateGridBlock(tilemap, pos, worldPos, null);
        }

        Debug.Log("Crop grid successfully generated.");
    }

    public void CreateGridBlock(Tilemap tilemap, Vector3Int location, Vector3 position, CropBlock block)
    {
        int x = location.x - origin.x;
        int y = location.y - origin.y;

        cropGrid[x, y] = new CropBlock(location, position);
    }

    public CropBlock GetBlockAtCell(Vector3Int cellPos)
    {
        int x = cellPos.x - origin.x;
        int y = cellPos.y - origin.y;

        if (x < 0 || y < 0 || x >= width || y >= height)
            return null;

        return cropGrid[x, y];
    }

    // Growth at sunrise
    private void ApplySunriseGrowth()
    {
        foreach (CropBlock block in plantedCrops)
        {
            block.UpdateGrowth(1f);
        }
    }

    public void AddToPlantedCrops(CropBlock block)
    {
        Debug.Log("Planted crops count: " + plantedCrops.Count);
        if (!plantedCrops.Contains(block))
            plantedCrops.Add(block);
    }

    public void RemoveFromPlantedCrops(CropBlock block)
    {
        plantedCrops.Remove(block);
    }
}
