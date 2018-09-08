using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pallets/Gradient", fileName = "Gradient")]
public class GradientColorSetting : ScriptableObject {

    public Gradient gradient;
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f,0f),new Keyframe(1f,1f));

    public Color GetColor(float percent)
    {
        if (curve.length > 0)
            return gradient.Evaluate(curve.Evaluate(percent));
        return gradient.Evaluate(percent);
    }
}
