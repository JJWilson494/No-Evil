using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReplacement : MonoBehaviour {

    [SerializeField]
    private Material mat;

    public enum MatMode { Item, Object, Enemy, Glow }
    [SerializeField] MatMode _mode = MatMode.Object;
    public MatMode mode { get { return _mode; } set { _mode = value; } }

    // Use this for initialization
    void Start () {
        switch (_mode)
        {
            case MatMode.Item:
                mat.SetOverrideTag("MatTag", "Item");
                break;
            case MatMode.Object:
                mat.SetOverrideTag("MatTag", "Object");
                break;
            case MatMode.Enemy:
                mat.SetOverrideTag("MatTag", "Enemy");
                break;
            case MatMode.Glow:
                mat.SetOverrideTag("MatTag", "Glow");
                break;

        }
	}
	
}
