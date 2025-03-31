using GameFramework.Event;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SkillForm:UGuiForm
{
    public SCSkillListInfoAckEventArgs sCSkillListEvent;
    public Button closeBtn;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        closeBtn.onClick.AddListener(() =>
        {
            GameEntry.UI.CloseUIForm(this);
        });
        GameEntry.Event.Subscribe(SCSkillListInfoAckEventArgs.EventId, SCCSkillList);

    }

  

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        sCSkillListEvent = (SCSkillListInfoAckEventArgs)userData;
        InitSkill(sCSkillListEvent.BagSCEquipChange);
    }

   
}
