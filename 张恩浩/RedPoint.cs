using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 节点类
/// </summary>
public class RedPoint
{
    public RedPointType type;//红点类型
    public int number;//当前节点的所有子节点
    public List<RedPoint> parentNotes = new List<RedPoint>();//父节点
    public List<RedPoint> childerNotes = new List<RedPoint>();//子节点
    public bool isRefRed = false;
    public Action<RedPointType, bool> handler;//事件
}

/// <summary>
/// 所有红点枚举类型
/// </summary>
public enum RedPointType
{
    root,
    task,
    task1,
    task2,
    skill,
    mainskill,
    beskill,
}
//单例
public class Singleton<T> where T : class, new()
{
    private static T _instance;
    private static object obj = new object();
    public static T GetInstance()
    {
        if (_instance == null)
        {
            lock (obj)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
            }
        }
        return _instance;
    }

}