using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using StarForce;

public class MapInforMation : EditorWindow
{
    static Dictionary<int, Map> dic;

    [MenuItem("编辑器/关卡编辑器")]

    private static void Init()
    {
        GetWindow<MapInforMation>("关卡编辑器").Show();
        dic = ConfigMgr.GetTable<Dictionary<int, Map>>("Map");
    }
    Vector2 v2;
    int id = 0;
    GameObject prefab;
    Sprite icon;
    
    private void OnGUI()
    {
        v2 = GUILayout.BeginScrollView(v2);
        //输入id
        id = EditorGUILayout.IntField("关卡编号", id);
        if (!dic.ContainsKey(id))
        {
            dic.Add(id, new Map());
            dic[id].Id = id;
        }

        dic[id].Map_Name = EditorGUILayout.TextField("名称", dic[id].Map_Name);
        if (dic[id].Map_Icon != null && dic[id].Map_Icon.Length > 0)
        {
            icon = AssetDatabase.LoadAssetAtPath<Sprite>(dic[id].Map_Icon);
        }
        icon = (Sprite)EditorGUILayout.ObjectField("图标", icon, typeof(Sprite), false);
        if (icon != null)
        {
            //获取地址
            string path = AssetDatabase.GetAssetPath(icon);
            dic[id].Map_Icon = path;
        }
        if (dic[id].Map_Prefab != null && dic[id].Map_Prefab.Length > 0)
        {
            //加载
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(dic[id].Map_Prefab);
        }
        prefab = (GameObject)EditorGUILayout.ObjectField("地图：", prefab, typeof(GameObject), false);
        if (prefab != null)
        {
            //获取地址
            string path = AssetDatabase.GetAssetPath(prefab);
            dic[id].Map_Prefab = path;
        }
        dic[id].Enemy_wave = EditorGUILayout.IntField("怪物总波次：", dic[id].Enemy_wave);
        dic[id].Enemy_wave_Time = EditorGUILayout.FloatField("波次间隔：", dic[id].Enemy_wave_Time);
        dic[id].Map_Start_term = EditorGUILayout.IntField("关卡开启：", dic[id].Map_Start_term);
        dic[id].Map_Tips = EditorGUILayout.TextField("关卡描述：", dic[id].Map_Tips);
        dic[id].Map_Victory = EditorGUILayout.IntField("通关状态：", dic[id].Map_Victory);
        dic[id].RefeshId = EditorGUILayout.IntField("怪物波次信息：", dic[id].RefeshId);

        GUILayout.EndScrollView();
        if (GUILayout.Button("保存数据"))//按钮
        {
            ConfigMgr.Save("Map", dic);
            AssetDatabase.Refresh();//刷新
        }
    }
}

