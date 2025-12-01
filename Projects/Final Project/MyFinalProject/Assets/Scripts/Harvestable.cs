using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public string cropName;
    public int amount = 1; // amount added to inventory (optional)

    public void Collect()
    {
        // later hook into your inventory
        Destroy(gameObject);
    }
}