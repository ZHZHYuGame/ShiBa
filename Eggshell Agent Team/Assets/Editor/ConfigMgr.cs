using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigMgr
{
    static Dictionary<string, object> dic = new Dictionary<string, object>();

    //传入路径返回散列表
    public static T GetTable<T>(string path) where T : class, new()
    {
        if (dic.ContainsKey(path))
        {
            return dic[path] as T;
        }
        else
        {
            if (File.Exists(Application.dataPath + "/Resources/" + path + ".json"))
            {
                string json = Resources.Load<TextAsset>(path).text;
                T table = JsonConvert.DeserializeObject<T>(json);
                dic.Add(path, table);
                return table;
            }
            else
            {
                return new T();
            }
        }

    }
    //获取字典数据
    public static T GetDicData<T>(string path, int key)
    {
        Dictionary<int, T> Intdic = GetTable<Dictionary<int, T>>(path);
        return Intdic[key];
    }
    //获取字典数据
    public static T GetDicData<T>(string path, string key)
    {
        Dictionary<string, T> Intdic = GetTable<Dictionary<string, T>>(path);
        return Intdic[key];
    }
    //获取集合数据
    public static T GetListData<T>(string path, int index)
    {
        List<T> list = GetTable<List<T>>(path);
        return list[index];
    }
    //保存json
    public static void Save(string path, object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(Application.dataPath + "/Resources/" + path + ".json", json);
    }

}