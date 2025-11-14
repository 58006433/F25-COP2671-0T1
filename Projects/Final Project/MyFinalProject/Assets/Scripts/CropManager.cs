using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    [SerializeField] private Tilemap farmingTilemap;

    // Grid storing crops by tile position
    private Dictionary<Vector3Int, CropBlock> plantedCrops = new Dictionary<Vector3Int, CropBlock>();

    private void Start()
    {
        CreateGridUsingTilemap(farmingTilemap);
    }

    public void CreateGridUsingTilemap(Tilemap tilemap)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            Vector3 worldPos = tilemap.CellToWorld(pos);
            CreateGridBlock(tilemap, pos, worldPos, null);
        }
    }

    public void CreateGridBlock(Tilemap tilemap, Vector3Int location, Vector3 position, CropBlock cropBlock)
    {
        // You could create a CropBlock component dynamically or assign it later when a crop is planted.
        if (!plantedCrops.ContainsKey(location))
            plantedCrops.Add(location, cropBlock);
    }

    public void AddToPlantedCrops(Vector3Int position, CropBlock cropBlock)
    {
        plantedCrops[position] = cropBlock;
    }

    public void RemoveFromPlantedCrops(Vector3Int position)
    {
        if (plantedCrops.ContainsKey(position))
            plantedCrops.Remove(position);
    }

    public CropBlock GetCropBlock(Vector3Int position)
    {
        plantedCrops.TryGetValue(position, out var block);
        return block;
    }
}
