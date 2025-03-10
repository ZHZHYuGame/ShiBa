using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class ConfigManager
{
    private static string configPath = "Assets/GameMain/GameResources/Configs/";

    // 加载配置文件
    public static T GetDic<T>(string fileName) where T : new()
    {
        string filePath = configPath + fileName + ".json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogWarning($"配置文件 {fileName} 不存在，创建新的空字典。");
            return new T();
        }
    }

    // 保存配置文件
    public static void Save(string fileName, object data)
    {
        string filePath = configPath + fileName + ".json";
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, json);
        Debug.Log($"配置文件 {fileName} 已保存。");
    }
}