
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池管理类
/// </summary>
public class AllObjectPool
{
    GameObject DamagePool;
    GameObject BulletPool;
    GameObject ExpPool;

    public AllObjectPool(GameObject hurttx, GameObject bullet, GameObject expPrefab)
    {
        DamagePool = FindOrCreate("DamagePool", "Canvas/");
        BulletPool = FindOrCreate("BulletPool");
        ExpPool = FindOrCreate("ExpPool");
        
        ObjectPool.CreatePool(hurttx, 200, DamagePool.transform,"ui");//血条对象池
        ObjectPool.CreatePool(bullet, 20, BulletPool.transform,"weapon");//创建子弹池子
        ObjectPool.CreatePool(expPrefab, 50, ExpPool.transform,"exp");//创建经验池
        

    }
    
    private GameObject FindOrCreate(string name, string path="")
    {
        GameObject obj= GameObject.Find(path+name);
        if (obj==null)
        {
            obj = new GameObject(name);
        }
        return obj;
    }
}
