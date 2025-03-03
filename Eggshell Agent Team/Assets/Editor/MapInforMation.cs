using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class MapInforMation : EditorWindow
{
    List<Map> maplist = new List<Map>();

    int id;
    string mapname;
    string map_icon;
    int wave;
    float wave_time;
    int player_exp;
    int player_next_exp;
    int start_term;
    string map_Tips;
    int Victory;

    [MenuItem("StageTool/关卡编辑器")]

    private static void Init()
    {
        GetWindow<MapInforMation>().Show();
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        id = EditorGUILayout.IntField("ID：",id);
        mapname = EditorGUILayout.TextField("名称：",mapname);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        map_icon = EditorGUILayout.TextField("关卡图片：",map_icon);
        wave = EditorGUILayout.IntField("怪波次",wave);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        wave_time = EditorGUILayout.FloatField("怪波次间隔", wave_time);
        player_exp = EditorGUILayout.IntField("玩家经验：", player_exp);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        player_next_exp = EditorGUILayout.IntField("玩家下一次经验（累加）", player_next_exp);
        start_term = EditorGUILayout.IntField("开启条件", start_term);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        map_Tips = EditorGUILayout.TextField("地图描述", map_Tips);
        Victory = EditorGUILayout.IntField("是否胜利", Victory);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("存入数据"))
        {
            Map map = new Map();
            map.Id = id;
            map.Map_Name = mapname;
            map.Map_Icon = map_icon;
            map.Enemy_wave = wave;
            map.Enemy_wave_Time = wave_time;
            map.Player_EXP = player_exp;
            map.Player_Next_EXP = player_next_exp;
            map.Map_Start_term = start_term;
            map.Map_Tips = map_Tips;
            map.Map_Victory = Victory;
            maplist.Add(map);
            for (int i = 0; i < maplist.Count; i++)
            {
                if (!File.Exists(Application.dataPath+"/Resources/MapInforMations.json"))
                {
                    File.CreateText(Application.dataPath + "/Resources/MapInforMations.json");
                    AssetDatabase.Refresh();
                }
                else
                {
                    string maps = JsonConvert.SerializeObject(maplist);
                    File.WriteAllText(Application.dataPath + "/Resources/MapInforMations.json",maps);
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}

