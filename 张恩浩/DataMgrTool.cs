
using ConfigTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.Progress;

public class DataMgrTool : Singleton<DataMgrTool>
{


    public gift1 GetGiftById(int reward_item)
    {
        return DataMgr.Instance.gift1Data[reward_item];
    }
    public List<string> GetIconPathById(gift1 gift)
    {

        List<string> list = new List<string>(5)
            {
                IsBconsumeOrZconsume(gift.item_1_id),
                IsBconsumeOrZconsume(gift.item_2_id),
                IsBconsumeOrZconsume(gift.item_3_id),
                IsBconsumeOrZconsume(gift.item_4_id),
                IsBconsumeOrZconsume(gift.item_5_id)
            };

        return list;
    }

    public string IsBconsumeOrZconsume(int id)
    {
        string str = string.Empty;
        if (DataMgr.Instance.bconsumeData.ContainsKey(id))
        {
            str = DataMgr.Instance.bconsumeData[id].icon_id.ToString();
        }
        else if (DataMgr.Instance.zconsumeData.ContainsKey(id))
        {
            str = DataMgr.Instance.zconsumeData[id].icon_id.ToString();
        }
        return str;
    }

    public Dictionary<int, List<SighData>> GetSignDic()
    {
        Dictionary<int, List<SighData>> dic = new Dictionary<int, List<SighData>>();
        foreach (var item in DataMgr.Instance.signinData.Values)
        {
            int index = item.month + 1;
            SighData sighData = new SighData()
            {
                day = item.day,
                signIn = item,

            };
            if (dic.ContainsKey(index))
            {
                dic[index].Add(sighData);
            }
            else
            {
                dic.Add(index, new List<SighData>());
                dic[index].Add(sighData);
            }
        }
        return dic;
    }
    //解析数据
    public List<BagData> GetBagDatas(GetBagInfoEventArgs e1)
    {
        List<BagData> bagDatas = new List<BagData>();
        KnapsackInfo[] info_list = e1.info.info_list;
        for (int i = 0;i<info_list.Length;i++)
        {
            BagData bagData = new BagData();
            if (info_list[i] == null)
            {
                continue;
            }
            else if (DataMgr.Instance.equipmentsData.ContainsKey(info_list[i].ItemId))
            {
                bagData.icon_id = DataMgr.Instance.equipmentsData[info_list[i].ItemId].iconid;
                bagData.name = DataMgr.Instance.equipmentsData[info_list[i].ItemId].name;
                bagData.userLevel = DataMgr.Instance.equipmentsData[info_list[i].ItemId].limitlevel;
                bagData.title = DataMgr.Instance.equipmentsData[info_list[i].ItemId].description;
                bagData.bagtype = DataMgr.Instance.equipmentsData[info_list[i].ItemId].bagtype;
                bagData.quality = DataMgr.Instance.equipmentsData[info_list[i].ItemId].quality;
                bagData.subtype = DataMgr.Instance.equipmentsData[info_list[i].ItemId].subtype % 100;

            }
            else if (DataMgr.Instance.bconsumeData.ContainsKey(info_list[i].ItemId))
            {
                bagData.icon_id = int.Parse(DataMgr.Instance.bconsumeData[info_list[i].ItemId].icon_id);
                bagData.name = DataMgr.Instance.bconsumeData[info_list[i].ItemId].name;
                bagData.userLevel = DataMgr.Instance.bconsumeData[info_list[i].ItemId].limit_level;
                bagData.title = DataMgr.Instance.bconsumeData[info_list[i].ItemId].description;
                bagData.bagtype = DataMgr.Instance.bconsumeData[info_list[i].ItemId].bag_type;

            }
            else if (DataMgr.Instance.zconsumeData.ContainsKey(info_list[i].ItemId))
            {

               bagData.icon_id = int.Parse(DataMgr.Instance.zconsumeData[info_list[i].ItemId].icon_id);
                bagData.name = DataMgr.Instance.zconsumeData[info_list[i].ItemId].name;
                bagData.userLevel = DataMgr.Instance.zconsumeData[info_list[i].ItemId].limit_level;
                bagData.title = DataMgr.Instance.zconsumeData[info_list[i].ItemId].description;

            }
            else if (DataMgr.Instance.gift1Data.ContainsKey(info_list[i].ItemId))
            {
               bagData.icon_id = DataMgr.Instance.gift1Data[info_list[i].ItemId].id;
                bagData.name = DataMgr.Instance.gift1Data[info_list[i].ItemId].name;
                bagData.userLevel = DataMgr.Instance.gift1Data[info_list[i].ItemId].limit_level;
            }
            bagData.id = info_list[i].ItemId;
            bagData.num = info_list[i].Num;
            bagData.index = info_list[i].Index;
            bagData.ist = info_list[i].IsBind;
            bagDatas.Add(bagData);
        }
        return bagDatas;
    }
    //背包分类
    internal List<BagData> SelectBagData(List<BagData> bagDatas, int index)
    {
        List<BagData> newBagDatas = new List<BagData>();
        if(index == 0)
        {
            newBagDatas = bagDatas;
        }else{
            for (int i = 1; i < bagDatas.Count; i++)
            {
                if (bagDatas[i].bagtype == index)
                {
                    newBagDatas.Add(bagDatas[i]);
                }
            }
        }
        
        return newBagDatas;
    }
    //获取装备
    internal List<BagData> GetEquipData(EquipListEventArgs e1)
    {
        
        Dictionary<int, ItemDataWrapper> equip_list = e1.info.equip_list;
        List<BagData> bagDatas = new List<BagData>(9);
        for (int i = 0; i < 9; i++)
        {
            bagDatas.Add(new BagData());
        }
        foreach (var item in equip_list)//遍历字典
        {
            BagData bagData = new BagData();
            if (DataMgr.Instance.equipmentsData.ContainsKey(item.Value.item_id))
            {
                bagData.icon_id = DataMgr.Instance.equipmentsData[item.Value.item_id].iconid;
                bagData.name = DataMgr.Instance.equipmentsData[item.Value.item_id].name;
                bagData.userLevel = DataMgr.Instance.equipmentsData[item.Value.item_id].limitlevel;
                bagData.title = DataMgr.Instance.equipmentsData[item.Value.item_id].description;
                bagData.bagtype = DataMgr.Instance.equipmentsData[item.Value.item_id].bagtype;
                bagData.quality = DataMgr.Instance.equipmentsData[item.Value.item_id].quality;
                bagData.subtype = DataMgr.Instance.equipmentsData[item.Value.item_id].subtype%100;

            }
            bagData.id = item.Value.item_id;
            bagData.num = item.Value.num;
            bagData.index = item.Value.index;
            bagData.ist = item.Value.is_bind;
            bagDatas[item.Value.index] = bagData;
        }
        


        return bagDatas;
    }
    public BagData GetBagData(ushort item_id)
    {
        BagData bagData = new BagData();
        if (DataMgr.Instance.equipmentsData.ContainsKey(item_id))
        {
            
            bagData.icon_id = DataMgr.Instance.equipmentsData[item_id].iconid;
            bagData.name = DataMgr.Instance.equipmentsData[item_id].name;
            bagData.userLevel = DataMgr.Instance.equipmentsData[item_id].limitlevel;
            bagData.title = DataMgr.Instance.equipmentsData[item_id].description;
            bagData.bagtype = DataMgr.Instance.equipmentsData[item_id].bagtype;
            bagData.quality = DataMgr.Instance.equipmentsData[item_id].quality;
            bagData.subtype = DataMgr.Instance.equipmentsData[item_id].subtype%100;
        }
        return bagData;
    }

    internal RongluData GetRongLuData(SendRongluEventArgs e1)
    {
        Ronglus ronglu_info = e1.info.ronglu_info;
        RongluData rongluData = new RongluData();
        if (DataMgr.Instance.rongliansxData.ContainsKey(ronglu_info.ronglu_level))
        {
            rongluData.ronglu_level = ronglu_info.ronglu_level;
            rongluData.ronglu_jingyan = ronglu_info.ronglu_jingyan;
            rongluData.upgrade_need_jingyan = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level].upgrade_need_jingyan;
            rongluData.now_gongji = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level].gongji;
            rongluData.now_fangyu = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level].fangyu;
            rongluData.now_maxhp = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level].maxhp;
            rongluData.next_gongji = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level + 1].gongji;
            rongluData.next_fangyu = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level + 1].fangyu;
            rongluData.next_maxhp = DataMgr.Instance.rongliansxData[ronglu_info.ronglu_level + 1].maxhp;

        }
        return rongluData;
    }
    //获取装备信息
    internal List<RoleSkillData> GetSkillData(GetSkillInfoEventArgs e1)
    {
        List<RoleSkillData> roleSkilldic = new List<RoleSkillData>();
        
        for (int i = 0; i < e1.skillInfo.skill_list.Length; i++)
        {
            RoleSkillData skillData = new RoleSkillData();
            int index = i;
            if (DataMgr.Instance.roleskillData.ContainsKey(e1.skillInfo.skill_list[index].skill_id))
            {
                skillData.skill_id = e1.skillInfo.skill_list[index].skill_id;
                skillData.skill_name = DataMgr.Instance.roleskillData[e1.skillInfo.skill_list[index].skill_id].skill_name;
                skillData.skill_icon = DataMgr.Instance.roleskillData[e1.skillInfo.skill_list[index].skill_id].skill_icon;
                skillData.skill_desc = DataMgr.Instance.roleskillData[e1.skillInfo.skill_list[index].skill_id].skill_desc;
                switch (e1.skillInfo.skill_list[index].skill_id)
                {
                    case 211:
                        skillData.skill_level = e1.skillInfo.skill_list[index].level;
                        skillData.coin_cost = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level+1].coin_cost;
                        skillData.fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].fix_hurt;
                        skillData.capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].capbility;
                        skillData.next_fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level+1].fix_hurt;
                        skillData.next_capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level+1].capbility;
                        skillData.cd_s = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].cd_s;
                        skillData.hurt_percent = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].hurt_percent;
                        break;
                    case 221:
                        skillData.skill_level = e1.skillInfo.skill_list[index].level;
                        skillData.coin_cost = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].coin_cost;
                        skillData.fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].fix_hurt;
                        skillData.capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].capbility;
                        skillData.next_fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level+1].fix_hurt;
                        skillData.next_capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level+1].capbility;
                        skillData.cd_s = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].cd_s;
                        skillData.hurt_percent = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].hurt_percent;
                        break;
                    case 231:
                        skillData.skill_level = e1.skillInfo.skill_list[index].level;
                        skillData.coin_cost = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].coin_cost;
                        skillData.fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].fix_hurt;
                        skillData.capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].capbility;
                        skillData.next_fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].fix_hurt;
                        skillData.next_capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].capbility;
                        skillData.cd_s = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].cd_s;
                        skillData.hurt_percent = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].hurt_percent;
                        break;
                    case 241:
                        skillData.skill_level = e1.skillInfo.skill_list[index].level;
                        skillData.coin_cost = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].coin_cost;
                        skillData.fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].fix_hurt;
                        skillData.capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].capbility;
                        skillData.next_fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].fix_hurt;
                        skillData.next_capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index + 1].level].capbility;
                        skillData.cd_s = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].cd_s;
                        skillData.hurt_percent = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].hurt_percent;
                        break;
                    case 251:
                        skillData.skill_level = e1.skillInfo.skill_list[index].level;
                        skillData.coin_cost = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level + 1].coin_cost;
                        skillData.fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].fix_hurt;
                        skillData.capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].capbility;
                        skillData.next_fix_hurt = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index ].level+1].fix_hurt;
                        skillData.next_capbility = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index ].level+1].capbility;
                        skillData.cd_s = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].cd_s;
                        skillData.hurt_percent = DataMgr.Instance.skill211Data[e1.skillInfo.skill_list[index].level].hurt_percent;
                        break;

                }
                roleSkilldic.Add(skillData);
            }
        }
        return roleSkilldic;
    }
}
public class RoleSkillData
{
    public int skill_id;//编号(服务器)
    public string skill_name;//技能名称
    public int skill_icon;//技能图标
    public string skill_desc;//描述
    public int skill_level;//等级(服务器)
    public int coin_cost;//金钱
    public int fix_hurt;//额外伤害
    public int capbility;//攻击力
    public int next_fix_hurt;//额外伤害
    public int next_capbility;//攻击力
    public float cd_s;//冷却
    public int hurt_percent;//百分比伤害


}

public class BossData
{
    public string name;
    public int icon;
    public int status;
    public int level;
    public uint time;

    public BossData(string name, int icon, int status, int level, uint time)
    {
        this.name = name;
        this.icon = icon;
        this.status = status;
        this.level = level;
        this.time = time;
    }
}
public class RongluData
{
    public int ronglu_level;//当前等级
    public int ronglu_jingyan;//当前经验值
    public int upgrade_need_jingyan;//当前等级最大经验值
    public int now_gongji;//当前等级攻击力
    public int now_fangyu;//当前等级防御力
    public int now_maxhp;//当前等级血量
    public int next_gongji;//下一等级攻击力
    public int next_fangyu;//下一等级防御力
    public int next_maxhp;//下一等级血量


}
public class BagData
{
    public int id;//编号
    public int icon_id;//图片id
    public int num;//数量
    public int index;//格子下标
    public int ist;//是否绑定
    public string name;//装备名称
    public int userLevel;//使用等级
    public string title;//装备描述
    public int bagtype;//物品类型
    public int quality;//品质
    public int subtype;//装备位置
    //获取途径

}
