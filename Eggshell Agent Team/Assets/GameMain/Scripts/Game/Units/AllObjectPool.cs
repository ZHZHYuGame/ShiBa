using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池管理类
/// </summary>
public class AllObjectPool
{
    public AllObjectPool(GameObject hurttx, GameObject bullet)
    {

       // ObjectPool.CreatePool(hurttx, 200,GameObject.Find("Canvas/DamagePool").transform);//血条对象池
        ObjectPool.CreatePool(bullet, 20, GameObject.Find("BulletPool").transform,"weapon");//创建子弹池子

    }
}
