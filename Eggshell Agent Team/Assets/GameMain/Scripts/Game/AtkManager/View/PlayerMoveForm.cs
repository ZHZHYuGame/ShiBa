using StarForce;
using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMoveForm : BasePanel
{
    ETC etc;
    private static string name = "PlayerMoveForm";
    private static string path = "Panel/PlayerMoveForm";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    public PlayerMoveForm() : base(uIType)
    {
    }

    public override void OnStart()
    {
        Debug.Log(uiType.Name);
        //GameMgr.GetInstance().UIManager_Root.dic_uiobject.Remove(uiType.Name);
        GameMgr.GetInstance().UIManager_Root.Pops(this);
        etc = UIMethod.Ins.GetOrAddSingleComponentInChild<Image>(ActiveObj,"ETC").GetComponent<ETC>();
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj,"ZanTinh").onClick.AddListener(Pause);
      
        base.OnStart();
    }

    private void Pause()
    {
      
        GameMgr.GetInstance().UIManager_Root.Push(new PaulePanel());
        Time.timeScale = 0;
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
