using GameFramework;
using GameFramework.Network;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
public class LoginPanel : UGuiForm
{
    [SerializeField] Button logBtn;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        logBtn.onClick.AddListener(() =>
        {
            CSLoginReq msg = ReferencePool.Acquire<CSLoginReq>();
            msg.rand_1 = UnityEngine.Random.Range(1000000, 10000000);
            msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            msg.key = "";
            msg.plat_name = Test.ins.play_name;
            msg.rand_2 = UnityEngine.Random.Range(1000000, 10000000);
            msg.plat_fcm = 0;
            msg.plat_server_id = 1;
            Test.ins. m_Channel.Send(msg);
        });
    }
}
