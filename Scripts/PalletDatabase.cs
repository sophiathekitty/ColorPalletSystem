using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pallets/Database", fileName = "Pallet Database")]
public class PalletDatabase : ScriptableObject {

    public IntRangeVariable indexIntRange;
    public IntVariable indexIntVariable;
    public PalletPreset[] palletPresets;
    public int presetIndex
    {
        get
        {
            if (indexIntRange != null)
                return indexIntRange.RuntimeValue;
            if (indexIntVariable != null)
                return indexIntVariable.RuntimeValue;

            return 0;
        }
        set
        {
            if (indexIntRange != null)
                indexIntRange.RuntimeValue = value;
            if (indexIntVariable != null)
                indexIntVariable.RuntimeValue = value;
        }
    }

    public Color GetColor(float percent, int layer)
    {
        if (indexIntRange != null || indexIntVariable != null)
            return palletPresets[presetIndex].GetColor(percent, layer);
        return new Color();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
