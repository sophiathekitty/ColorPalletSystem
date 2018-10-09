using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// custom editor for gradient color
/// </summary>
[CustomEditor(typeof(GradientColorSetting))]
[CanEditMultipleObjects]
public class GradientColorEditor : Editor
{
    /// <summary>
    /// display percent for preview color
    /// </summary>
    float percent;
    /// <summary>
    /// access the selected target
    /// </summary>
    GradientColorSetting gradient
    {
        get
        {
            return (GradientColorSetting)target;
        }
    }

    /// <summary>
    /// draw the inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        percent = EditorGUILayout.Slider(percent, 0, 1);
        Rect r = EditorGUILayout.BeginVertical();
        GUI.Box(r,"",ColorPreviewUtils.InitStyles(gradient.GetMappedColor(percent)));
        GUILayout.Space(100);
        EditorGUILayout.EndVertical();
    }
}
