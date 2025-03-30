using GameFramework;
using GameFramework.Event;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCityForm:UGuiForm
{
    public Button btn,loginForm,bag,shop,seven,mouth,liao,skill,boss;
    SevenDayEventArgs sCSevenDay;
    SCKnapsackInfoAckEventArgs snapsackInfoAck;
    SCEquipListEventArgs sCEquipListEvent;
    SCChannelChatAckHandleEventArgs sCChannelChatAckHandleEventArgs;
    SCSkillListInfoAckEventArgs sCSkillListEvent;
    SCNeutralBossInfoEventArgs sCNeutralBoss;
    public SCTimeAckEventArgs timeAck;
    public BagAllData bagAllData = new BagAllData();
    public List<SCChannelChatAck> sCChannelChatAcks = new List<SCChannelChatAck>();
    public static MainCityForm instance;
    private void Awake()
    {
        instance = this;
    }
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        
        shop.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.ScrollerViewForm, this);
        });
        mouth.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.EveryDayForm, this);
        });
        seven.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.SevenDayForm, sCSevenDay);
        });
        bag.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.BagForm, bagAllData);
        });
        liao.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.TalkForm, sCChannelChatAcks);
        });
        skill.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.SkillForm, sCSkillListEvent);
        });
        boss.onClick.AddListener(() =>
        {
            GameEntry.UI.OpenUIForm(UIFormId.BossForm, sCNeutralBoss);
        });
        GameEntry.Event.Subscribe(SCSkillListInfoAckEventArgs.EventId, SCCSkillList);
        GameEntry.Event.Subscribe(SevenDayEventArgs.EventId, SevenDay);
        GameEntry.Event.Subscribe(SCKnapsackInfoAckEventArgs.EventId, BagSCKnapsackInfo);
        GameEntry.Event.Subscribe(SCEquipListEventArgs.EventId, BagSCEquipList);
        GameEntry.Event.Subscribe(SCChannelChatAckHandleEventArgs.EventId, SCChannelChat);
        GameEntry.Event.Subscribe(SCNeutralBossInfoEventArgs.EventId, SCNeutralBoss);
        GameEntry.Event.Subscribe(SCTimeAckEventArgs.EventId, SCTimeAcks);
        
       // GameEntry.Event.Subscribe(SCSendRongluInfoEventArgs.EventId, SCSendRong);
    }

    private void SCTimeAcks(object sender, GameEventArgs e)
    {
        timeAck = (SCTimeAckEventArgs)e;

    }

    private void SCNeutralBoss(object sender, GameEventArgs e)
    {
        sCNeutralBoss = (SCNeutralBossInfoEventArgs)e;
    }

    private void SCCSkillList(object sender, GameEventArgs e)
    {
        sCSkillListEvent = (SCSkillListInfoAckEventArgs)e;
    }

    private void SCChannelChat(object sender, GameEventArgs e)
    {
        sCChannelChatAckHandleEventArgs = (SCChannelChatAckHandleEventArgs)e;
        sCChannelChatAcks.Add(sCChannelChatAckHandleEventArgs.sCRongluResultInfo);
    }

    private void BagSCKnapsackInfo(object sender, GameEventArgs e)
    {
        bagAllData.snapsackInfoAck = (SCKnapsackInfoAckEventArgs)e;
        //bagAllData.knapsackItems = 
    }
    private void BagSCEquipList(object sender, GameEventArgs e)
    {
        bagAllData.sCEquipListEvent = (SCEquipListEventArgs)e;
    }
    private void SevenDay(object sender, GameEventArgs e)
    {
        sCSevenDay = (SevenDayEventArgs)e;
    }
}
public class BagAllData
{
    public SCKnapsackInfoAckEventArgs snapsackInfoAck;
    public SCEquipListEventArgs sCEquipListEvent;
    //public List<KnapsackItem> knapsackItems=new List<KnapsackItem>();
    //public List<ItemDataWrapper> itemDatas=new List<ItemDataWrapper>();
}