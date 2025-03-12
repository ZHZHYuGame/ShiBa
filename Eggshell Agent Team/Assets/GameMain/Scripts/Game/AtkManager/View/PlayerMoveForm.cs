using StarForce;
using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMoveForm : BasePanel
{
    ETC etc;
    private static string name = "PlayerMoveForm";
    private static string path = "Panel/PlayerMoveForm";
    private static LayerType layerType = LayerType.Etc;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    public PlayerMoveForm() : base(uIType)
    {
    }

    public override void OnStart()
    {
        Debug.Log(uiType.Name);
        etc = UIMethod.Ins.GetOrAddSingleComponentInChild<Image>(ActiveObj,"ETC").GetComponent<ETC>();
        etc.tag = "Etc";
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
