using UnityEngine;

public class FarmingController : MonoBehaviour
{
    public CropManager cropManager;
    public Transform player; // Assign Player Transform in Inspector
    public SeedPacket selectedSeed;

    private CropBlock selectedBlock;

    void Start()
    {
        Debug.Log("FarmingController START");
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
        if (player == null) return;

        Vector3 targetPos = player.position + Vector3.down;

        Vector3Int cell = cropManager.farmingTilemap.WorldToCell(targetPos);
        selectedBlock = cropManager.GetBlockAtCell(cell);
    }

    void TillSelectedSoil()
    {
        if (selectedBlock != null)
            selectedBlock.TillSoil();
        else
            Debug.Log("No block under player to till.");
    }

    void WaterSelectedSoil()
    {
        if (selectedBlock != null)
            selectedBlock.WaterSoil();
        else
            Debug.Log("No block under player to water.");
    }

    void PlantSelectedSeed()
    {
        Debug.Log("---- PLANT DEBUG ----");
        Debug.Log("selectedBlock: " + selectedBlock);
        Debug.Log("selectedSeed: " + selectedSeed);
        Debug.Log("Block tilled: " + (selectedBlock != null && selectedBlock.isTilled));
        Debug.Log("Block occupied: " + (selectedBlock != null && selectedBlock.isOccupied));

        if (selectedSeed != null && selectedBlock != null)
        {
            selectedBlock.PlantSeed(selectedSeed, cropManager.cropPrefab);
            cropManager.AddToPlantedCrops(selectedBlock);
        }
        else
        {
            Debug.Log("No block under player to plant seed.");
        }
    }

    void HarvestSelectedCrop()
    {
        if (selectedBlock != null)
            selectedBlock.HarvestPlants();
        else
            Debug.Log("No block under player to harvest.");
    }
}
