using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class ObjectPool
{
    static Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();


    private static string GetName(string str)
    {
        return str.Replace("(Clone)", "");
    }
    //放入
    public static void Enqueue(GameObject prefab)
    {
        prefab.name = GetName(prefab.name);
        string key = prefab.name;
        if (poolDictionary.ContainsKey(key))
        {
            poolDictionary[key].Enqueue(prefab);
            
        }
        else
        {
            poolDictionary.Add(key, new Queue<GameObject>());
            poolDictionary[key].Enqueue(prefab);
        }
        prefab.SetActive(false);
    }
    //创建池子
    public static void CreatePool(GameObject prefab, int size,Transform pos, string abName)
    {
        prefab.name = GetName(prefab.name);
        string key = prefab.name;
        if (!poolDictionary.ContainsKey(key))
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                GameObject obj = UIManager.Ins.InstantiatePrefab(pos,prefab,abName);
                //GameObject obj = GameObject.Instantiate(prefab,pos);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
            poolDictionary.Add(key, pool);
        }
    }
    //取出
    public static GameObject GetObject(GameObject prefab)
    {
        prefab.name = GetName(prefab.name);
        string key = prefab.name;
        if (poolDictionary[key].Count > 0)
        {
            GameObject obj = poolDictionary[key].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return GameObject.Instantiate(prefab); // 动态扩容
    }
}
