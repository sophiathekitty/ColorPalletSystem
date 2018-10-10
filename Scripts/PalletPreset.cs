using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// preset for a pallet of gradient colors
/// </summary>
[CreateAssetMenu(menuName = "Pallets/Pallet", fileName = "Pallet Preset")]
public class PalletPreset : ScriptableObject {
    public PalletDefinition Definition;
    /// <summary>
    /// array of color gradient layers
    /// </summary>
    public List<GradientColorSetting> gradientColors = new List<GradientColorSetting>();
    /// <summary>
    /// returns a color for a set layer and gradient percent
    /// </summary>
    /// <param name="percent">position in gradient</param>
    /// <param name="colorLayer">color layer</param>
    /// <returns></returns>
    public Color GetColor(float percent, int colorLayer)
    {
        if (colorLayer < 0)
            colorLayer = 0;
        if (colorLayer > gradientColors.Count - 1)
            colorLayer = gradientColors.Count - 1;
        if(gradientColors.Count > 0 && gradientColors[colorLayer] != null)
            return gradientColors[colorLayer].GetMappedColor(percent);
        return new Color();
    }
}
