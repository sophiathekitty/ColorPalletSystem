using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorApplier : MonoBehaviour {
    public PalletDatabase pallet;
    public IntRangeVariable Counter;
    public int colorLayerIndex;
    public int offset = 0;
    private SpriteRenderer sprite;
    private TextMeshPro text;
    private ParticleSystem ps;
    private TextMeshProUGUI text2;
    private Image image;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        text = GetComponent<TextMeshPro>();
        ps = GetComponent<ParticleSystem>();
        text2 = GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>();
        Update();
	}

    int offsetCounter
    {
        get
        {
            if (Counter == null)
                return 0;
            int c = Counter.RuntimeValue - offset;
            if (c < Counter.MinValue)
                c = (int)Counter.MinValue;
            if (c > Counter.MaxValue)
                c = (int)Counter.MaxValue;
            return c;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Counter == null || pallet == null)
            return;
        if (sprite != null)
            sprite.color = pallet.GetColor(Counter.Percent, colorLayerIndex);
        if (text != null)
            text.color = pallet.GetColor(Counter.Percent, colorLayerIndex);
        if(ps != null)
        {
            var main = ps.main;
            main.startColor = pallet.GetColor(Counter.Percent, colorLayerIndex);
        }
        if (text2 != null)
            text2.color = pallet.GetColor(Counter.Percent, colorLayerIndex);
        if (image != null)
            image.color = pallet.GetColor(Counter.Percent, colorLayerIndex);
	}
}
