using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Range(0f, 24f)] public float timeOfDay = 6f;
    public float daySpeed = 0.1f; // Hours per second

    void Update()
    {
        timeOfDay += Time.deltaTime * daySpeed;
        if (timeOfDay >= 24f)
            timeOfDay = 0f;
    }
}
