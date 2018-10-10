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
    private ReorderableList GradientsList, Layers;
    private PalletDatabase database {  get { return target as PalletDatabase; } }
    private float percent;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        if (database.Definition != null)
            Layers.DoLayoutList();
        else
            database.Definition = EditorGUILayout.ObjectField(database.Definition, typeof(PalletDefinition), false) as PalletDefinition;
        EditorGUILayout.Space();
        GradientsList.DoLayoutList();
    }
    private void OnEnable()
    {
        SetupDefinitionList();
        SetupGradientList();
    }

    private void SetupDefinitionList()
    {
        Layers = new ReorderableList(database.Definition.layers, typeof(List<PalletDefinition.PalletLayer>), true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                Rect r1 = new Rect(rect.position, new Vector2(rect.width / 3, rect.height));
                Rect r2 = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3 * 2, rect.height);
                GUI.Label(r1, "Layers");
                database.Definition = EditorGUI.ObjectField(r2,database.Definition, typeof(PalletDefinition), false) as PalletDefinition;
            },

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2; rect.height -= 4;
                float oneThirdWidth = 60;
                float twoThirdsWidth = rect.width - 60;
                Rect r1 = new Rect(rect.position, new Vector2(twoThirdsWidth - 10, 18));
                Rect r2 = new Rect(rect.x + twoThirdsWidth, rect.y, oneThirdWidth, rect.height);
                database.Definition.layers[index].name = EditorGUI.TextField(r1, database.Definition.layers[index].name);
                database.Definition.layers[index].icon = EditorGUI.ObjectField(r2, database.Definition.layers[index].icon, typeof(Sprite), false) as Sprite;
            },

            elementHeightCallback = (int index) => { return 20; },

            onReorderCallback = (ReorderableList list) =>
            {
                EditorUtility.SetDirty(database);
                EditorUtility.SetDirty(database.palletPresets[list.index]);
            },

            onSelectCallback = (ReorderableList list) => { /* update preview */ },
            onAddCallback = (ReorderableList list) => { list.list.Add(new PalletDefinition.PalletLayer()); }
        };
    }


    private void SetupGradientList()
    { 
        GradientsList = new ReorderableList(database.palletPresets, typeof(List<PalletPreset>), true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                Rect r1 = new Rect(rect.position, new Vector2(rect.width / 3, rect.height));
                Rect r2 = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3 * 2, rect.height);
                GUI.Label(r1, "Pallets");
                percent = EditorGUI.Slider(r2, percent, 0, 1);
            },

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (database.Definition != null && database.palletPresets[index] != null)
                    database.palletPresets[index].Definition = database.Definition;

                if (database.palletPresets[index] != null)
                    DrawPalletList(rect, database.palletPresets[index].gradientColors);
                database.palletPresets[index] = (PalletPreset)EditorGUI.ObjectField(
                    new Rect(rect.position, new Vector2(rect.width, 16)),
                    database.palletPresets[index], typeof(PalletPreset), false);
            },

            elementHeightCallback = (int index) => { return 56; },

            onReorderCallback = (ReorderableList list) => 
            {
                EditorUtility.SetDirty(database);
                EditorUtility.SetDirty(database.palletPresets[list.index]);
            },

            onSelectCallback = (ReorderableList list) => { /* update preview */ },
            onAddCallback = (ReorderableList list) => { database.palletPresets.Add(null); }
        };
    }

    private void DrawPalletList(Rect rect, List<GradientColorSetting> gradients)
    {
        for(int i = 0; i < gradients.Count; i++)
            GUI.Box(new Rect(
                rect.x + (rect.width / gradients.Count * i),
                rect.y,rect.width/gradients.Count,rect.height), "", ColorPreviewUtils.InitStyles(gradients[i].GetMappedColor(percent)));
    }
}
