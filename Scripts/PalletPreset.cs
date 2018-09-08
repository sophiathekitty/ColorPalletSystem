using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pallets/Pallet", fileName = "Pallet Preset")]
public class PalletPreset : ScriptableObject {
    public GradientColorSetting[] gradientColors;

    public Color GetColor(float percent, int colorLayer)
    {
        if (colorLayer < 0)
            colorLayer = 0;
        if (colorLayer > gradientColors.Length - 1)
            colorLayer = gradientColors.Length - 1;
        if(gradientColors.Length > 0 && gradientColors[colorLayer] != null)
            return gradientColors[colorLayer].GetColor(percent);
        return new Color();
    }
}
