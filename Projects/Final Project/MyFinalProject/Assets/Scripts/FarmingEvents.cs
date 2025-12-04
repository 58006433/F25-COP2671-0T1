using UnityEngine;
using UnityEngine.Events;

public class FarmingEvents : MonoBehaviour
{
    public static FarmingEvents Instance;

    public UnityEvent OnTillSoil;
    public UnityEvent OnWaterSoil;
    public UnityEvent OnPlantSeed;
    public UnityEvent OnHarvestCrop;

    private void Awake()
    {
        Debug.Log("FarmingEvents Awake() called");

        if (Instance == null) Instance = this;

        // Initialize events if null
        OnTillSoil ??= new UnityEvent();
        OnWaterSoil ??= new UnityEvent();
        OnPlantSeed ??= new UnityEvent();
        OnHarvestCrop ??= new UnityEvent();
    }
}
