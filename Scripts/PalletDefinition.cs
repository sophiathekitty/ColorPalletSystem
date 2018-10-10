using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pallets/Definition", fileName = "Pallet Definition")]
public class PalletDefinition : ScriptableObject {
    public List<PalletLayer> layers;

    [System.Serializable]
    public class PalletLayer
    {
        public string name;
        public Sprite icon;
    }
}
