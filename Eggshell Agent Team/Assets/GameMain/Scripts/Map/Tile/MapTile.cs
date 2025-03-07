using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 地图块
/// </summary>
public class MapTile
{
    public Vector2 postion;
    public string name;
    Dictionary<uint, EntityBase> tileDic;

    public MapTile(int x, int y)
    {
        name = x + "_" + y;
        tileDic = new Dictionary<uint, EntityBase>();
    }

    //添加新的对象
    public void AddEntity(EntityBase entity)
    {
        if (tileDic.ContainsKey(entity.index)) return;
        tileDic.Add(entity.index, entity);
    }
    //卸载移除对象 (资源卸载等待扩展)
    public void RemoveEntity(uint id)
    {
        if (!tileDic.ContainsKey(id)) return;
        tileDic[id].Destory();
        tileDic.Remove(id);
    }

    public void DelAllEntity()
    {
        foreach (var item in tileDic.Values)
        {
            item.Destory();
        }
        tileDic.Clear();
    }
    public void Show()
    {
        foreach (var item in tileDic.Values)
        {
            item.Show();
        }
    }
    public void Hide()
    {
        foreach (var item in tileDic.Values)
        {
            item.Hide();
        }
    }
}


