using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightPreset lightPreset;

    [Header("Variables")]
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private bool enableFog = true;

    private void Update()
    {
        if (lightPreset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightPreset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightPreset.fogColor.Evaluate(timePercent);

        if (enableFog)
        {
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogColor =
                lightPreset.fogColor.Evaluate(timePercent);

            if (lightPreset.fogDensity != null)
            {
                RenderSettings.fogDensity =
                    lightPreset.fogDensity.Evaluate(timePercent);
            }
        }
        else
        {
            RenderSettings.fog = false;
        }


        if (directionalLight != null)
        {
            directionalLight.color = lightPreset.directionalLightColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    break;
                }
            }
        }
    }

}