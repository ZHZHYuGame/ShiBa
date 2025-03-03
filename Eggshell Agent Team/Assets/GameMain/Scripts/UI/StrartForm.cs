using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrartForm : UGuiForm
{
    [SerializeField]
    Button button;
    [SerializeField]
    Text kaishi;
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        kaishi.text = "开始游戏";
        button.onClick.AddListener(() =>
        {
            GameEntry.UI.CloseAllLoadedUIForms();
            GameEntry.UI.OpenUIForm(UIFormId.MainForm, this);
        });

    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

  
}
