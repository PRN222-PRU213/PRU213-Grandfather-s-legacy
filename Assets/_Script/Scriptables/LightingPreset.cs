using UnityEngine;

[CreateAssetMenu(menuName = "Lighting/Light Preset")]
public class LightPreset : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionalLightColor;
    public Gradient fogColor;
    public AnimationCurve fogDensity;
}
