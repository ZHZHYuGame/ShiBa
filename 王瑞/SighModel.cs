using ConfigTools;
using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SighData
{
    public int day;
    public SignIn signIn;
    public bool ist;
}
public class SighModel : Singleton<SighModel>
{
    Dictionary<int, List<SighData>> dic;//一年数据
    public int day;//今天
    public int month;//当前月
    public List<SighData> signInList;//某月数据
    DateTime date;
    public override void Init()
    {
        base.Init();
        dic = DataMgrTool.Instance.GetSignDic();//获取一年数据
        //获取时间
        StarForce.GameEntry.Event.Subscribe(GetTimeEventArgs.EventId, GetTimeEvent);
        //签到数据信息
        StarForce.GameEntry.Event.Subscribe(GetWelfareInfoEventArgs.EventId, GetWelfareInfoEvent);
        
    }

    private void GetTimeEvent(object sender, GameEventArgs e)
    {
        GetTimeEventArgs e1 = (GetTimeEventArgs)e;
        date = DateTime.UnixEpoch.AddSeconds(e1.time.server_time);//int转换Date时间
        day = date.Day;
        month = date.Month; 
        signInList = new List<SighData>(dic[month].Count);//某月数据
        foreach (var item in dic[month])
        {
            signInList.Add(item);
        }
    }

    private void GetWelfareInfoEvent(object sender, GameEventArgs e)
    {
        GetWelfareInfoEventArgs e1 = (GetWelfareInfoEventArgs)e;
        uint sign = e1.info.sign_in_days;//这个月全部签到状态

        //获取签到状态
        for (int i = 0; i < signInList.Count; i++)
        {
            int index = i + 1;
            bool ist = (sign&(1<<index))>0?true:false;
            signInList[i].ist = ist;
        }

    }
}
