using RedpointSystem;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StrartForm : BasePanel
{
    [SerializeField]
    Button button;
    [SerializeField]
    Text kaishi;

    private static string name = "StrartForm";
    private static string path = "Panel/StrartForm";
    private static LayerType layerType = LayerType.Normal;

    public static readonly UIType uIType = new UIType(path, name, layerType);
    public StrartForm() : base(uIType)
    {
        //初始的时候直接让充值提示显示  红点直接显示出来
        RedPointSystem.Instance.AddNode(RedPointKey.Play_LEVEL1_TOP);
    }

    

   
    public override void OnStart()
    {
        base.OnStart();
        //kaishi.text = "开始游戏";
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "Strest").onClick.AddListener(Strest);
    }

    private void Strest()
    {
        GameMgr.GetInstance().UIManager_Root.Pop(false);
        Debug.Log(4);
        GameMgr.GetInstance().UIManager_Root.Push(new MainPanel());
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }
}
