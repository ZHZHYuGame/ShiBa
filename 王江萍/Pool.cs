using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    //集合
    public List<GameObject> list = new List<GameObject>();
    //游戏预设体
    public GameObject GoPrefab;
    //最大个数
    public int MaxCount = 50;
    //对象保存对象池
    public void Push(GameObject go)
    {
        if (list.Count < MaxCount)
        {
            list.Add(go);
            //改为非激活
            go.SetActive(false);
        }
        else
        {
            Destroy(go);
        }

    }
    //对象池中取出对象
    public GameObject Pop()
    {
        if (list.Count > 0)
        {
            GameObject go = list[0];
            list.RemoveAt(0);
            //设置为激活状态
            go.SetActive(true);
            return go;

        }
        return Instantiate(GoPrefab);
    }
    //清除对象池
    public void Clear()
    {
        list.Clear();
    }
}