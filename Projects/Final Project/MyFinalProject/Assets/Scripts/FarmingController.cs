using UnityEngine;

public class FarmingController : MonoBehaviour
{
    public CropManager cropManager;
    public Camera mainCamera;
    public SeedPacket selectedSeed;

    private CropBlock selectedBlock;

    void OnEnable()
    {
        FarmingEvents.Instance.OnTillSoil.AddListener(TillSelectedSoil);
        FarmingEvents.Instance.OnWaterSoil.AddListener(WaterSelectedSoil);
        FarmingEvents.Instance.OnPlantSeed.AddListener(PlantSelectedSeed);
        FarmingEvents.Instance.OnHarvestCrop.AddListener(HarvestSelectedCrop);
    }

    void OnDisable()
    {
        FarmingEvents.Instance.OnTillSoil.RemoveListener(TillSelectedSoil);
        FarmingEvents.Instance.OnWaterSoil.RemoveListener(WaterSelectedSoil);
        FarmingEvents.Instance.OnPlantSeed.RemoveListener(PlantSelectedSeed);
        FarmingEvents.Instance.OnHarvestCrop.RemoveListener(HarvestSelectedCrop);
    }

    void Update()
    {
        UpdateSelectedBlock();
    }

    void UpdateSelectedBlock()
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cell = cropManager.farmingTilemap.WorldToCell(worldPos);

        selectedBlock = cropManager.GetBlockAtCell(cell);
    }

    void TillSelectedSoil()
    {
        selectedBlock?.TillSoil();
    }

    void WaterSelectedSoil()
    {
        selectedBlock?.WaterSoil();
    }

    void PlantSelectedSeed()
    {
        if (selectedSeed != null && selectedBlock != null)
        {
            selectedBlock.PlantSeed(selectedSeed, cropManager.cropPrefab);
            cropManager.AddToPlantedCrops(selectedBlock);
        }
    }

    void HarvestSelectedCrop()
    {
        selectedBlock?.HarvestPlants();
    }
}
