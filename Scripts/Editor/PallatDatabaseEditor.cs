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
    /// reorderable list of pallets, and layers
    /// </summary>
    private ReorderableList GradientsList, Layers;

    /// <summary>
    /// gets the target as a PalletDatabase
    /// </summary>
    /// <value>target</value>
    private PalletDatabase Database {  get { return target as PalletDatabase; } }
    private float percent;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        if (Database.Definition != null)
            Layers.DoLayoutList();
        else
            Database.Definition = EditorGUILayout.ObjectField(Database.Definition, typeof(PalletDefinition), false) as PalletDefinition;
        EditorGUILayout.Space();
        GradientsList.DoLayoutList();
        // trying to draw layer preview images stacked on each other
        // so that it can show how each layer will looked once color is applied.
        if(GradientsList.index >= 0){
            Rect rect = EditorGUILayout.BeginVertical();
            for(int i = 0; i < Database.Definition.layers.Count; i++){
                Material layerMat = new Material(ColorPreviewUtils.defaultMat);
                layerMat.color = Database.palletPresets[GradientsList.index].GetColor(percent,i);
                if(Database.Definition.layers[i] != null && Database.Definition.layers[i].icon != null)
                    EditorGUI.DrawPreviewTexture(rect,Database.Definition.layers[i].icon.texture,layerMat,ScaleMode.ScaleToFit);
            }
            GUILayout.Space(200);
            EditorGUILayout.EndHorizontal();
        }
    }
    private void OnEnable()
    {
        SetupDefinitionList();
        SetupGradientList();
    }

    private void SetupDefinitionList()
    {
        Layers = new ReorderableList(Database.Definition.layers, typeof(List<PalletDefinition.PalletLayer>), true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                Rect r1 = new Rect(rect.position, new Vector2(rect.width / 3, rect.height));
                Rect r2 = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3 * 2, rect.height);
                GUI.Label(r1, "Layers");
                Database.Definition = EditorGUI.ObjectField(r2,Database.Definition, typeof(PalletDefinition), false) as PalletDefinition;
            },

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2; rect.height -= 4;
                float oneThirdWidth = 60;
                float twoThirdsWidth = rect.width - 60;
                Rect r1 = new Rect(rect.position, new Vector2(twoThirdsWidth - 10, 18));
                Rect r2 = new Rect(rect.x + twoThirdsWidth, rect.y, oneThirdWidth, rect.height);
                Database.Definition.layers[index].name = EditorGUI.TextField(r1, Database.Definition.layers[index].name);
                Database.Definition.layers[index].icon = EditorGUI.ObjectField(r2, Database.Definition.layers[index].icon, typeof(Sprite), false) as Sprite;
            },

            elementHeightCallback = (int index) => { return 20; },

            onReorderCallback = (ReorderableList list) =>
            {
                EditorUtility.SetDirty(Database);
                EditorUtility.SetDirty(Database.Definition);
            },

            onSelectCallback = (ReorderableList list) => { /* update preview */ },

            onAddCallback = (ReorderableList list) => { list.list.Add(new PalletDefinition.PalletLayer()); }
        };
    }

    private void SetupGradientList()
    { 
        GradientsList = new ReorderableList(Database.palletPresets, typeof(List<PalletPreset>), true, true, true, true)
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
                if (Database.Definition != null && Database.palletPresets[index] != null)
                    Database.palletPresets[index].Definition = Database.Definition;

                if (Database.palletPresets[index] != null)
                    DrawPalletList(rect, Database.palletPresets[index].gradientColors);
                
                Database.palletPresets[index] = (PalletPreset)EditorGUI.ObjectField(
                    new Rect(rect.position, new Vector2(rect.width, 16)),
                    Database.palletPresets[index], typeof(PalletPreset), false);
            },

            elementHeightCallback = (int index) => { return 56; },

            onReorderCallback = (ReorderableList list) => 
            {
                EditorUtility.SetDirty(Database);
                EditorUtility.SetDirty(Database.palletPresets[list.index]);
            },

            onSelectCallback = (ReorderableList list) => { /* update preview */ },

            onAddCallback = (ReorderableList list) => { Database.palletPresets.Add(null); }
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
