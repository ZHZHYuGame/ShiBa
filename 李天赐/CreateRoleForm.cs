
using GameFramework;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoleForm :UGuiForm
{
    public static CreateRoleForm instance;
    public InputField input;
    public Text proOne, proTwo, proThree, proFour,place;
    //public List<GameObject> list=new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        proOne.text = "∆∆æ¸";
        proTwo.text = "ÃÏ…∑";
        proThree.text = "ƒ´π•";
        proFour.text = "«Ÿ…™";
        place.text = "«Î ‰»Î√˚◊÷";
        
    }
    public void onBtnClick()
    {
        
        if (string.IsNullOrEmpty(input.text))
        {
            ColorLog.LogCyan("«Î ‰»ÎÍ«≥∆");
            return;
        }
        Test.instance.user = input.text;
        CSCreateRoleReq meq = ReferencePool.Acquire<CSCreateRoleReq>();
        meq.plat_name ="dev_"+MineForm.ins.input.text;
        meq.role_name = input.text;
        meq.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        meq.key = "";
        meq.plat_server_id = 1;
        meq.plat_fcm = 0;
        meq.avatar = Avatar.instance.index;
        meq.sex = ToggleAnim.ins.sex;
        meq.prof = 1;
        meq.camp_type = 1;
        meq.plat_spid = "dev";
        Test.m_Channel.Send(meq);
    }
   
}
