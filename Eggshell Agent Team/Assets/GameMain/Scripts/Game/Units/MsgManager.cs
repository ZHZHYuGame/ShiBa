using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MesID
{
    Exp,
    SkillUpLevel,
}

public class MsgManager<T> : Singleton<MsgManager<T>>
{
    Dictionary<MesID, Action<T>> MesDic = new Dictionary<MesID, Action<T>>();

    public void OnAddListener(MesID id, Action<T> action)
    {
        if (MesDic.ContainsKey(id))
        {
            MesDic[id] += action;
        }
        else
        {
            MesDic.Add(id, action);
        }
    }

    public void OnRemoveListen(MesID id)
    {
        if (MesDic.ContainsKey(id))
        {
            MesDic.Remove(id);
        }
    }

    public void OnBroadCast(MesID id, T t)
    {
        if (MesDic.ContainsKey(id))
        {
            MesDic[id](t);
        }
        else
        {
            Debug.Log("消息未侦听");
        }
    }

}
 