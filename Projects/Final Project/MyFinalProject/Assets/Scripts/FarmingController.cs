using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingController : MonoBehaviour
{
    [Header("References")]
    public CropManager cropManager;
    public Camera mainCamera;
    public SeedPacket selectedSeed;

    private CropBlock selectedBlock;

    void OnEnable()
    {
        // Subscribe to farming actions
        FarmingEvents.Instance.OnTillSoil.AddListener(TillSelectedSoil);
        FarmingEvents.Instance.OnWaterSoil.AddListener(WaterSelectedSoil);
        FarmingEvents.Instance.OnPlantSeed.AddListener(PlantSelectedSeed);
        FarmingEvents.Instance.OnHarvestCrop.AddListener(HarvestSelectedCrop);
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        FarmingEvents.Instance.OnTillSoil.RemoveListener(TillSelectedSoil);
        FarmingEvents.Instance.OnWaterSoil.RemoveListener(WaterSelectedSoil);
        FarmingEvents.Instance.OnPlantSeed.RemoveListener(PlantSelectedSeed);
        FarmingEvents.Instance.OnHarvestCrop.RemoveListener(HarvestSelectedCrop);
    }

    void Update()
    {
        UpdateSelectedBlock();
    }

    // selecting a block
    void UpdateSelectedBlock()
    {
        if (mainCamera == null) return;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = cropManager.farmingTilemap.WorldToCell(mouseWorldPos);

        selectedBlock = cropManager.GetBlockAtCell(cellPos);
    }

    // event listiners
    void TillSelectedSoil()
    {
        if (selectedBlock == null) return;

        selectedBlock.TillSoil();
    }

    void WaterSelectedSoil()
    {
        if (selectedBlock == null) return;

        selectedBlock.WaterSoil();
    }

    void PlantSelectedSeed()
    {
        if (selectedBlock == null || selectedSeed == null) return;

        selectedBlock.PlantSeed(selectedSeed, cropManager.cropPrefab);
    }

    void HarvestSelectedCrop()
    {
        if (selectedBlock == null) return;

        selectedBlock.HarvestPlants();
    }
}
