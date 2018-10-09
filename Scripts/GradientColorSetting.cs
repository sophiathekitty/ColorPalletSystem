using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// a gradient color
/// </summary>
[CreateAssetMenu(menuName = "Pallets/Gradient", fileName = "Gradient")]
public class GradientColorSetting : ScriptableObject {
    /// <summary>
    /// the gradient
    /// </summary>
    public Gradient gradient;
    /// <summary>
    /// animation curve for remapping the gradient
    /// </summary>
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f,0f),new Keyframe(1f,1f));
    /// <summary>
    /// returns a color based on a remapped percent
    /// </summary>
    /// <param name="percent"></param>
    /// <returns></returns>
    public Color GetMappedColor(float percent)
    {
        if (curve.length > 0)
            return gradient.Evaluate(curve.Evaluate(percent));
        return gradient.Evaluate(percent);
    }
}
