using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";
    private static LayerType layerType = LayerType.Top;

    public static readonly UIType uIType = new UIType(path, name,layerType);
    public StartPanel() : base(uIType)
    {

    }
    Button test;
    Text text;
    public override void OnStart()
    {
        base.OnStart();
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "Back").onClick.AddListener(Back);
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "Load").onClick.AddListener(Load);
        test = UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "Test");
        text = UIMethod.Ins.GetOrAddSingleComponentInChild<Text>(ActiveObj, "text");
        test.onClick.AddListener(() =>
        {
            text.text = "¥Ûœ„Ω∂£¨¥Ûœ„Ω∂";
        });
    }

    private void Load()
    {
        Game game = new Game();
        GameMgr.GetInstance().SceneControl_Root.LoadScene(game.SceneName, game);
    }

    private void Back()
    {
        GameMgr.GetInstance().UIManager_Root.Pop(false);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDistroy()
    {
        Debug.Log("StartPanel back!");
        base.OnDistroy();
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    
}
