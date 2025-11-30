using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightLighting : MonoBehaviour
{
    [Header("References")]
    public Light2D globalLight;
    public TimeManager timeManager;

    [Header("Light Intensity Curve")]
    public AnimationCurve intensityCurve;

    [Header("Light Color Gradient")]
    public Gradient colorGradient;

    void Update()
    {
        float t = timeManager.TimePercent; // 0 - 1

        // Apply intensity over day
        globalLight.intensity = intensityCurve.Evaluate(t);

        // Apply color transition
        globalLight.color = colorGradient.Evaluate(t);
    }
}