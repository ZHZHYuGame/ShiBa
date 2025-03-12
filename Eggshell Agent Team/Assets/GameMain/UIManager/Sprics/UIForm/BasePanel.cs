using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel 
{
    public UIType uiType;
    /// <summary>
    /// 在Panel在场景里面对应的物体
    /// </summary>
    public GameObject ActiveObj;

    public BasePanel(UIType uItype)
    {
        uiType = uItype;
    }

    public virtual void OnStart()
    {
        Debug.Log($"{uiType.Name}开始使用");
        UIMethod.Ins.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }

    public virtual void OnEndable()
    {
        UIMethod.Ins.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }

    public virtual void OnDistroy()
    {
        UIMethod.Ins.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;


    }
    public virtual void OnDestroy()
    {
        UIMethod.Ins.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }
}
