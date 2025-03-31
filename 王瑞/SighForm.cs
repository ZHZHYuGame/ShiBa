using ConfigTools;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SighForm : UGuiForm
{
    [SerializeField]
    SighScrollerController sighScrollerController;
    [SerializeField]
    Button buQian;
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
    }
    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
}
