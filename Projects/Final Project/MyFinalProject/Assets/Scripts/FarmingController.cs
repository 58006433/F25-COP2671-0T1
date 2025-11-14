using UnityEngine;
using UnityEngine.Events;

public class FarmingController : MonoBehaviour
{
    public UnityEvent OnHoe;
    public UnityEvent OnWater;
    public UnityEvent OnSeed;
    public UnityEvent OnGather;

    public CropBlock selectedBlock;
    public SeedPacket selectedSeed;

    public void HoeAction() => selectedBlock?.TillSoil();
    public void WaterAction() => selectedBlock?.WaterSoil();
    public void PlantAction() => selectedBlock?.PlantSeed(selectedSeed);
    public void GatherAction() => selectedBlock?.HarvestPlants();
}