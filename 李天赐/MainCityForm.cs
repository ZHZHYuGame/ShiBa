using GameFramework;
using GameFramework.Event;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class MainCityForm : UGuiForm
//{
//    public Button btn, loginForm, bag, shop, seven, mouth;
//    SevenDayEventArgs sCSevenDay;
//    SCKnapsackInfoAckEventArgs snapsackInfoAck;
//    //SCEquipListEventArgs sCEquipListEvent;
//    public BagAllData bagAllData = new BagAllData();
//    protected override void OnInit(object userData)
//    {
//        base.OnInit(userData);

//        shop.onClick.AddListener(() =>
//        {
//            GameEntry.UI.OpenUIForm(UIFormId.ScrollerViewForm, this);
//        });
//        mouth.onClick.AddListener(() =>
//        {
//            GameEntry.UI.OpenUIForm(UIFormId.EveryDayForm, this);
//        });
//        seven.onClick.AddListener(() =>
//        {
//            GameEntry.UI.OpenUIForm(UIFormId.SevenDayForm, sCSevenDay);
//        });
//        bag.onClick.AddListener(() =>
//        {
//            GameEntry.UI.OpenUIForm(UIFormId.BagForm, bagAllData);

//        });
//        GameEntry.Event.Subscribe(SevenDayEventArgs.EventId, SevenDay);
//        GameEntry.Event.Subscribe(SCKnapsackInfoAckEventArgs.EventId, BagSCKnapsackInfo);
//        //GameEntry.Event.Subscribe(SCEquipListEventArgs.EventId, BagSCEquipList);
//    }

//    private void BagSCKnapsackInfo(object sender, GameEventArgs e)
//    {
//         bagAllData.snapsackInfoAck = (SCKnapsackInfoAckEventArgs)e;
//        //bagAllData.knapsackItems = 
//    }
//    private void BagSCEquipList(object sender, GameEventArgs e)
//    {
//       // bagAllData.sCEquipListEvent = (SCEquipListEventArgs)e;
//    }
//    private void SevenDay(object sender, GameEventArgs e)
//    {
//        sCSevenDay = (SevenDayEventArgs)e;
//    }
//}
//public class BagAllData
//{
//    public SCKnapsackInfoAckEventArgs snapsackInfoAck;
//    //public SCEquipListEventArgs sCEquipListEvent;
//    //public List<KnapsackItem> knapsackItems=new List<KnapsackItem>();
//    //public List<ItemDataWrapper> itemDatas=new List<ItemDataWrapper>();
//}
