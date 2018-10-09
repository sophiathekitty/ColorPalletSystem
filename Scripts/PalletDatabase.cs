using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// database of gradient pallets
/// </summary>
[CreateAssetMenu(menuName ="Pallets/Database", fileName = "Pallet Database")]
public class PalletDatabase : ScriptableObject {
    #region SharedVariableSaveSystem
    public IntRangeVariable indexIntRange;
    public IntVariable indexIntVariable;
    #endregion
    /// <summary>
    /// holds the index value
    /// </summary>
    private int index;
    /// <summary>
    /// array of pallet presets
    /// </summary>
    public List<PalletPreset> palletPresets;
    /// <summary>
    /// the index of the preset to use
    /// </summary>
    public int Index
    {
        get
        {
            #region SharedVariableSaveSystem
            if (indexIntRange != null)
                return indexIntRange.RuntimeValue;
            if (indexIntVariable != null)
                return indexIntVariable.RuntimeValue;
            #endregion
            return index;
        }
        set
        {
            #region SharedVariableSaveSystem
            if (indexIntRange != null)
                indexIntRange.RuntimeValue = value;
            if (indexIntVariable != null)
                indexIntVariable.RuntimeValue = value;
            #endregion
            index = value;
        }
    }
    /// <summary>
    /// gets the color from the database
    /// </summary>
    /// <param name="percent">the position in the gradient</param>
    /// <param name="layer">the gradient layer</param>
    /// <returns></returns>
    public Color GetColor(float percent, int layer)
    {
        /* // maybe see about deleting this bit of code?
        if (indexIntRange != null || indexIntVariable != null)
            return palletPresets[Index].GetColor(percent, layer);
        return new Color();
        */
        return palletPresets[Index].GetColor(percent, layer);
    }
}
