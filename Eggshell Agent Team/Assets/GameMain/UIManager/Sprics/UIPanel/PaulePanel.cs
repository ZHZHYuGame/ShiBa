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
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "FanHun").onClick.AddListener(FanHun);
        base.OnStart();
    }

    private void FanHun()
    {
        Time.timeScale = 1;
        Game game = new Game();
        GameMgr.GetInstance().UIManager_Root.Pop(true);
        GameMgr.GetInstance().SceneControl_Root.LoadScene("11", game);
        GameMgr.GetInstance().UIManager_Root.Push(new MainPanel());
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
