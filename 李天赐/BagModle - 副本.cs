using ConfigTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagModle
{
     Dictionary<int,gift1> gift1=new Dictionary<int,gift1>();
     Dictionary<int,gift2> gift2=new Dictionary<int,gift2>();
     Dictionary<int,Zconsume> Zconsume = new Dictionary<int, Zconsume>();
     Dictionary<int,Bconsume> Bconsume = new Dictionary<int, Bconsume>();
     Dictionary<int,Equipments> equip = new Dictionary<int, Equipments>();
   public void Init()
    {
        gift1 = DataMgr.Instance.gift1Data;
        gift2 = DataMgr.Instance.gift2Data;
        Zconsume = DataMgr.Instance.zconsumeData;
        Bconsume = DataMgr.Instance.bconsumeData;
        equip = DataMgr.Instance.equipmentsData;
    }

    internal gift1 FindGift1(ushort item_id)
    {
        if (gift1.ContainsKey(item_id))
        {
            return gift1[item_id];
        }
        return null;
    }
    internal gift2 FindGift2(ushort item_id)
    {
        if (gift2.ContainsKey(item_id))
        {
            return gift2[item_id];
        }
        return null;
    }
    internal Zconsume FindZconsume(ushort item_id)
    {
        if (Zconsume.ContainsKey(item_id))
        {
            return Zconsume[item_id];
        }
        return null;
    }
    internal Bconsume FindBconsume(ushort item_id)
    {
        if (Bconsume.ContainsKey(item_id))
        {
            return Bconsume[item_id];
        }
        return null;
    }

    internal Equipments FindEquipments(ushort item_id)
    {
        if (equip.ContainsKey(item_id))
        {
            return equip[item_id];
        }
        return null;
    }
}
