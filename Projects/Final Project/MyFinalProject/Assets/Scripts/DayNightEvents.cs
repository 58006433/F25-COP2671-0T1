using UnityEngine;
using UnityEngine.Events;

public class DayNightEvents : MonoBehaviour
{
    public UnityEvent OnSunrise;
    public UnityEvent OnSunset;

    [Header("Trigger Times")]
    public float sunriseTime = 6f;
    public float sunsetTime = 18f;

    private TimeManager timeManager;

    private bool sunriseTriggered = false;
    private bool sunsetTriggered = false;

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
    }

    void Update()
    {
        float t = timeManager.timeOfDay;

        // Sunrise event
        if (!sunriseTriggered && t >= sunriseTime && t < sunriseTime + 0.1f)
        {
            OnSunrise?.Invoke();
            sunriseTriggered = true;
            sunsetTriggered = false;
        }

        // Sunset event
        if (!sunsetTriggered && t >= sunsetTime && t < sunsetTime + 0.1f)
        {
            OnSunset?.Invoke();
            sunsetTriggered = true;
            sunriseTriggered = false;
        }
    }
}
