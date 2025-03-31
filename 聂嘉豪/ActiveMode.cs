using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class  BagForm :UGuiForm
{
    [SerializeField] private Button CloseBtu;
    public BagModle bagmodle=new BagModle();
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        CloseBtu.onClick.AddListener(() =>
        {

            GameEntry.UI.CloseUIForm(this);
        });
        bagmodle.Init();
        myScroller.Delegate = this;
   
    }
    SCKnapsackInfoAckEventArgs sCKnapsackInfoAckEventArgs;
    protected override void OnOpen(object userData)
    {
       // base.OnOpen(userData);
        bagEquipDatas bagEquip = (bagEquipDatas)userData;
        SCKnapsackInfoAckEventArgs sCKnapsackInfoAckEventArgs = bagEquip.bagdata;
        this.sCKnapsackInfoAckEventArgs = sCKnapsackInfoAckEventArgs;
        myScroller.ReloadData();//Ë¢ÐÂ

        SCEquipList item = bagEquip.equipdata.SCKnapsackItemChangeParam;
        Debug.Log(item);

        //  Debug.Log(myScroller);
    }

}
