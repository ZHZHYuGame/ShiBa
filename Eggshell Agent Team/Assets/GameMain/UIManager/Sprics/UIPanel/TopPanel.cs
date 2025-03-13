using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : BasePanel
{
    private static string name = "TopPanel";
    private static string path = "Panel/TopPanel";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    public GameObject backBtn;
    public TopPanel() : base(uIType)
    {
    }

    

    public override void OnStart()
    {
        base.OnStart();
        backBtn = UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "BackBtn").gameObject;
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "BackBtn").onClick.AddListener(Back);
        ReddotManager.Instance.ChangeValue("main/activity",0);
    }

    private void Back()
    {
        GameMgr.GetInstance().UIManager_Root.Pop(false);
        GameMgr.GetInstance().UIManager_Root.Push(new MainPanel());
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    

    
}
