using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//定义类,用于存储游戏对象的基础数据
public class ObjData
{
    public string uuid;         //游戏对象的唯一标识
    public GameObject prefab;   //游戏对象的预制体
    public Vector3 pos;         //游戏对象的位置
    public Vector3 ang;         //游戏对象的旋转角度
    public ObjData(GameObject prefab, Vector3 pos, Vector3 ang)
    {
        //生成一个新的唯一标识符
        this.uuid = System.Guid.NewGuid().ToString();
        //赋值预制体
        this.prefab = prefab;
        //赋值位置
        this.pos = pos;
        //赋值旋转角度
        this.ang = ang;
    }
}
