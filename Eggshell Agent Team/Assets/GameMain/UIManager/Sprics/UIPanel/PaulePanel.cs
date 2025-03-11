using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaulePanel : BasePanel
{
    private static string name = "PaulePanel";
    private static string path = "Panel/PaulePanel";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    public PaulePanel() : base(uIType)
    {
    }
    public override void OnStart()
    {
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "JiXu").onClick.AddListener(Strate);
        base.OnStart();
    }

    private void Strate()
    {
        Debug.Log("播放");
        Time.timeScale = 1;
        GameMgr.GetInstance().UIManager_Root.Pop(false);
        
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
