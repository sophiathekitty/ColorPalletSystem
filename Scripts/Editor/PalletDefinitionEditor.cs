using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
[CustomEditor(typeof(PalletDefinition))]
public class PalletDefinitionEditor : Editor
{
    private ReorderableList LayerList;
    private PalletDefinition Definition { get { return target as PalletDefinition; } }
    private void OnEnable()
    {
        LayerList = new ReorderableList(Definition.layers, 
            typeof(List<PalletDefinition.PalletLayer>), 
            true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                GUI.Label(rect, "Layers");
            },
            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2; rect.height -= 4;
                float oneThirdWidth = 60;
                float twoThirdsWidth = rect.width - 60;
                Rect r1 = new Rect(rect.position, new Vector2(twoThirdsWidth - 10, 18));
                Rect r2 = new Rect(rect.x + twoThirdsWidth, rect.y, oneThirdWidth, rect.height);
                Definition.layers[index].name = EditorGUI.TextField(r1, Definition.layers[index].name);
                Definition.layers[index].icon = EditorGUI.ObjectField(r2, Definition.layers[index].icon, typeof(Sprite), false) as Sprite;
            },
            elementHeightCallback = (int index) =>
            {
                if (Definition.layers[index].icon != null)
                    return 60;
                return 30;
            },
            onReorderCallback = (ReorderableList list) => 
            {
                EditorUtility.SetDirty(target);
            },
            onAddCallback = (ReorderableList list) =>
            {
                list.list.Add(new PalletDefinition.PalletLayer());
            }
        };
    }

    private void ListUpdated(ReorderableList list)
    {
        
    }

    public override void OnInspectorGUI()
    {
        LayerList.DoLayoutList();
    }
}
