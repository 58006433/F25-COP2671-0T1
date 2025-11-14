using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour
{
    public Button hoeButton;
    public Button waterButton;
    public Button plantButton;
    public Button harvestButton;

    public FarmingController farmingController;

    private void Start()
    {
        hoeButton.onClick.AddListener(farmingController.HoeAction);
        waterButton.onClick.AddListener(farmingController.WaterAction);
        plantButton.onClick.AddListener(farmingController.PlantAction);
        harvestButton.onClick.AddListener(farmingController.GatherAction);
    }
}
