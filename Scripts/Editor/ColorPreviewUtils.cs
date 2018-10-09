using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPreviewUtils  {

    /// <summary>
    /// generate style
    /// </summary>
    public static GUIStyle InitStyles(Color col)
    {
        GUIStyle style = null;
        style = new GUIStyle(GUI.skin.box);
        style.normal.background = MakeTex(2, 2, col);
        return style;
    }
    /// <summary>
    /// make the color preview texture
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="col">color</param>
    /// <returns></returns>
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
