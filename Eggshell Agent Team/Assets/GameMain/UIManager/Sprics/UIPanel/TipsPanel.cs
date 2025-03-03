using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";
    private static LayerType layerType = LayerType.Top;

    public static readonly UIType uIType = new UIType(path, name, layerType);
    public TipsPanel() : base(uIType)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
