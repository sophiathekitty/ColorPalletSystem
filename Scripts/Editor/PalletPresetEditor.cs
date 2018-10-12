using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
/// <summary>
/// custom editor for pallet preset
/// </summary>
[CustomEditor(typeof(PalletPreset))]
public class PalletPresetEditor : Editor
{
    /// <summary>
    /// holds the percent to preview colors at
    /// </summary>
    float percent;
    /// <summary>
    /// reorderable list of gradients
    /// </summary>
    private ReorderableList GradientsList;
    /// <summary>
    /// the pallet preset we're editing
    /// </summary>
    private PalletPreset Pallet {  get { return target as PalletPreset; } }
    /// <summary>
    /// draw the inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GradientsList.DoLayoutList();
        Rect rect = EditorGUILayout.BeginVertical();
        for(int i = 0; i < Pallet.Definition.layers.Count; i++){
            Material layerMat = new Material(ColorPreviewUtils.defaultMat);
            layerMat.color = Pallet.GetColor(percent,i);
            if(Pallet.Definition.layers[i] != null && Pallet.Definition.layers[i].icon != null)
                EditorGUI.DrawPreviewTexture(rect,Pallet.Definition.layers[i].icon.texture,layerMat,ScaleMode.ScaleToFit); // not transparent? D:
        }
        GUILayout.Space(200);
        EditorGUILayout.EndHorizontal();
    }
    private void OnEnable()
    {
        GradientsList = new ReorderableList(Pallet.gradientColors,
            typeof(List<GradientColorSetting>),
            true, true, false, false)
        {
            drawHeaderCallback = OnDrawHeader,
            drawElementCallback = OnDrawElement,
            elementHeightCallback = GetElementHeight,
            onReorderCallback = ListUpdated
        };

        if(Pallet.Definition != null)
        {
            while (Pallet.gradientColors.Count > Pallet.Definition.layers.Count)
                Pallet.gradientColors.RemoveAt(Pallet.gradientColors.Count - 1);
            while (Pallet.gradientColors.Count < Pallet.Definition.layers.Count)
                Pallet.gradientColors.Add(null);
        }
    }

    private void ListUpdated(ReorderableList list)
    {
        EditorUtility.SetDirty(target);
    }

    private float GetElementHeight(int index)
    {
        return 66;
    }

    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        Rect r1 = new Rect(rect.x, rect.y + 18, rect.width, rect.height - 38);
        if(Pallet.gradientColors[index] != null)
            GUI.Box(r1, "", ColorPreviewUtils.InitStyles(Pallet.gradientColors[index].GetMappedColor(percent)));
        if (Pallet.Definition != null)
        {
            GUI.Label(new Rect(rect.position, new Vector2(rect.width / 2, 16)), Pallet.Definition.layers[index].name);
            Pallet.gradientColors[index] = (GradientColorSetting)EditorGUI.ObjectField(
                new Rect(rect.x + rect.width/2,rect.y, rect.width/2, 16),
                Pallet.gradientColors[index], typeof(GradientColorSetting), false);
        }
        else
            Pallet.gradientColors[index] = (GradientColorSetting)EditorGUI.ObjectField(
                new Rect(rect.position, new Vector2(rect.width, 16)),
                Pallet.gradientColors[index], typeof(GradientColorSetting), false);

    }

    private void OnDrawHeader(Rect rect)
    {
        Rect r1 = new Rect(rect.position, new Vector2(rect.width / 3, rect.height));
        Rect r2 = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3 * 2, rect.height);
        GUI.Label(r1, "Gradients");
        percent = EditorGUI.Slider(r2,percent, 0, 1);
    }
}
