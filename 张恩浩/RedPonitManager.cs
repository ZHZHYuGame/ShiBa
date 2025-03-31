using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedPonitManager : Singleton<RedPonitManager>
{
    //所有节点
    public Dictionary<RedPointType, RedPoint> allRedPointDic = new Dictionary<RedPointType, RedPoint>();
    //初始化节点关系
    public void InitPoint()
    {
        //任务
        RegisterRedPointToPoint(RedPointType.task, RedPointType.root);
        RegisterRedPointToPoint(RedPointType.task1, RedPointType.task);
        RegisterRedPointToPoint(RedPointType.task2, RedPointType.task);
        //技能
        RegisterRedPointToPoint(RedPointType.skill, RedPointType.root);
        RegisterRedPointToPoint(RedPointType.mainskill, RedPointType.skill);
        RegisterRedPointToPoint(RedPointType.beskill, RedPointType.skill);

    }

    private void RegisterRedPointToPoint(RedPointType child, RedPointType parent)
    {
        RedPoint parentNote = CreatOrGetPonit(parent);
        RedPoint childNote = CreatOrGetPonit(child);
        //将子节点添加到父节点的子节点集合中
        if (childNote.parentNotes.Find((x) => x.type == parent) == null)
        {
            childNote.parentNotes.Add(parentNote);
        }
        //将父节点添加到子节点的父级合中
        if (parentNote.childerNotes.Find((x) => x.type == child) == null)
        {
            parentNote.childerNotes.Add(childNote);
        }


    }
    //添加获取
    private RedPoint CreatOrGetPonit(RedPointType type)
    {
        RedPoint redPoint = null;
        if (!allRedPointDic.ContainsKey(type))
        {
            redPoint = new RedPoint();
            redPoint.type = type;
            //添加到总节点
            allRedPointDic.Add(type, redPoint);
        }
        redPoint = allRedPointDic[type];
        return redPoint;
    }

    /// <summary>
    /// UI与红点的关系
    /// </summary>
    /// <param name="type"></param>
    /// <param name="action"></param>
    public void RegisterUIHandle(RedPointType type, Action<RedPointType, bool> action)
    {
        RedPoint note = null;
        //查找红点
        if (allRedPointDic.TryGetValue(type, out note))
        {
            //修改该节点的红点状态
            note.handler += action;
        }
        //在注册的时候可同步数据给UI
        note.handler?.Invoke(type, note.isRefRed);
    }
    /// <summary>
    /// 逐层更新父节点的红点信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="state">状态</param>
    public void RedPointStateUpdateToParent(RedPointType type, bool state)
    {
        //获取当前红点信息
        RedPoint note = CreatOrGetPonit(type);
        //更改红点状态
        note.isRefRed = state;
        foreach (var item in note.parentNotes)
        {
            if (note.isRefRed && item.isRefRed != note.isRefRed)
            {
                item.isRefRed = state;
                item.handler(item.type, item.isRefRed);
            }
            else
            {
                int num = 0;
                //遍历所有的子节点
                foreach (var c in item.childerNotes)
                {
                    //如果有一个为真，代表父节点红点还是true显示状态
                    if (c.isRefRed == true)
                    {
                        num++;
                    }
                }
                item.isRefRed = num > 0;
                item.handler(item.type, item.isRefRed);//更新UI状态
            }
            RedPointStateUpdateToParent(item.type, item.isRefRed);
        }
    }
}