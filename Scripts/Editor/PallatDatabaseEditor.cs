using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(PalletDatabase))]
public class PallatDatabaseEditor : Editor
{
    /// <summary>
    /// reorderable list of pallets
    /// </summary>
    private ReorderableList GradientsList;
    private PalletDatabase database {  get { return target as PalletDatabase; } }
    private float percent;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GradientsList.DoLayoutList();
    }
    private void OnEnable()
    {
        GradientsList = new ReorderableList(database.palletPresets, typeof(List<PalletPreset>), true, true, true, true)
        {
            drawHeaderCallback = OnDrawHeader,
            drawElementCallback = OnDrawElement,
            elementHeightCallback = GetElementHeight,
            onReorderCallback = ListUpdated,
            onSelectCallback = OnSelect,
            onAddCallback = OnAddElement
        };
    }

    private void OnAddElement(ReorderableList list)
    {
        database.palletPresets.Add(null);
    }

    private void OnSelect(ReorderableList list)
    {
        //list.index;
    }

    private void ListUpdated(ReorderableList list)
    {
        EditorUtility.SetDirty(database);
        EditorUtility.SetDirty(database.palletPresets[list.index]);
    }

    private float GetElementHeight(int index)
    {
        return 56;
    }

    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        if (database.palletPresets[index] != null)
            DrawPalletList(rect, database.palletPresets[index].gradientColors);
        database.palletPresets[index] = (PalletPreset)EditorGUI.ObjectField(
            new Rect(rect.position, new Vector2(rect.width, 16)),
            database.palletPresets[index], typeof(PalletPreset), false);
    }

    private void OnDrawHeader(Rect rect)
    {
        Rect r1 = new Rect(rect.position, new Vector2(rect.width / 3, rect.height));
        Rect r2 = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3 * 2, rect.height);
        GUI.Label(r1, "Pallets");
        percent = EditorGUI.Slider(r2, percent, 0, 1);
    }

    private void DrawPalletList(Rect rect, List<GradientColorSetting> gradients)
    {
        for(int i = 0; i < gradients.Count; i++)
            GUI.Box(new Rect(
                rect.x + (rect.width / gradients.Count * i),
                rect.y,rect.width/gradients.Count,rect.height), "", ColorPreviewUtils.InitStyles(gradients[i].GetMappedColor(percent)));
    }
}
