using GameFramework;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.UI;

public partial class CreatRolePanel : UGuiForm
{
    [SerializeField] InputField inputname;
    [SerializeField] Button creatBtn;
    [SerializeField] Toggle tog1, tog2, tog3, tog4;
    [SerializeField] Text text1, text2, text3, text4,selectCh,inputext;

    sbyte sex,avatar=1;
    string name;
    public void InitText()
    {
        text1.text = "破军";
        text2.text = "天煞";
        text3.text = "墨弓";
        text4.text = "琴瑟";
        selectCh.text = "国家选择";
        inputext.text = "请输入名字";
    }
    public void InitToggle()
    {
        tog1.onValueChanged.AddListener((istrue) =>
        {
            name = "破军";
            sex = 1;
        });
        tog2.onValueChanged.AddListener((istrue) =>
        {
            sex = 0;
            name = "天煞";
        });
        tog3.onValueChanged.AddListener((istrue) =>
        {
            sex = 1;
            name = "墨弓";
        });
        tog4.onValueChanged.AddListener((istrue) =>
        {
            sex = 0;
            name = "琴瑟";
        });
    }
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        InitText();
        InitToggle();
        creatBtn.onClick.AddListener(() =>
        {
            CSCreateRoleReq msg = ReferencePool.Acquire<CSCreateRoleReq>();//消息类型
            msg.plat_name = Test.ins.play_name;
            msg.role_name = Test.ins.role_name;
            msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            msg.key = "";
            msg.plat_server_id = 1;
            msg.plat_fcm = 0;
            msg.avatar = avatar;
            msg.sex = sex;
            msg.prof = 1;
            msg.camp_type = 1;
            msg.plat_spid = "dev1";
            Test.ins.m_Channel.Send(msg);//发送出去
        });
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
