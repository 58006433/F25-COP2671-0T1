using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayLengthInMinutes = 2f; // full 24h cycle in real-time minutes
    public float timeOfDay = 0f; // 0 - 24
    public bool timePaused = false;

    public float TimePercent => timeOfDay / 24f;

    void Update()
    {
        if (timePaused) return;

        // Advance time
        timeOfDay += 24f / (dayLengthInMinutes * 60f) * Time.deltaTime;

        if (timeOfDay >= 24f)
            timeOfDay -= 24f;
    }
}