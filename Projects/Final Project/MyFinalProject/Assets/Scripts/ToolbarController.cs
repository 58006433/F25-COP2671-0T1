using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public void OnHoe()
    {
        FarmingEvents.Instance.OnTillSoil.Invoke();
        Debug.Log("Hoe Tool Used");
    }

    public void OnWater()
    {
        FarmingEvents.Instance.OnWaterSoil.Invoke();
        Debug.Log("Water Tool Used");
    }

    public void OnSeed()
    {
        FarmingEvents.Instance.OnPlantSeed.Invoke();
        Debug.Log("Seed Tool Used");
    }

    public void OnGather()
    {
        FarmingEvents.Instance.OnHarvestCrop.Invoke();
        Debug.Log("Harvest Tool Used");
    }
}
