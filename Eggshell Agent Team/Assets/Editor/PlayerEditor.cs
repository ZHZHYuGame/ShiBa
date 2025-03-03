using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System;
using System.IO;

public class PlayerEditor : EditorWindow
{
    private static Dictionary<int, Player> Player_Dic = new Dictionary<int, Player>();
    private static Dictionary<int, Skill> Skill_Dic = new Dictionary<int, Skill>();

    [MenuItem("StageTool/玩家编辑器")]

    public static void Init()
    {
        GetWindow<PlayerEditor>().Show();
        if (File.Exists(Application.dataPath+"/Resources/Player.json"))
        {
            string playerinfomation = File.ReadAllText(Application.dataPath + "/Resources/Player.json");
            JsonConvert.DeserializeObject<Dictionary<int, Player>>(playerinfomation);
        }
        else if (!File.Exists(Application.dataPath + "/Resources/Player.json"))
        {
            File.CreateText(Application.dataPath + "/Resources/Player.json");
            AssetDatabase.Refresh();
        }
        else
        {
            Player_Dic = new Dictionary<int, Player>();
        }
    }
    Vector2 pos;
    int Player_Id;
    //string Player_Name;
    Animator Player_Animator;
    GameObject Player_Prefab;
    //int Player_Lever;
    //float Player_Blood;
    //float Player_AtkSpeed;
    //float Player_MoveSpeed;
    //float Player_Atk;

    Skill[] Now_Skill;
    Skill[] Sive_Skill;

    int Skill_id;

    private void OnGUI()
    {
        GUILayout.BeginScrollView(pos);

        Player_Id = EditorGUILayout.IntField("编号：",Player_Id);
        if (!Player_Dic.ContainsKey(Player_Id))
        {
            Player_Dic.Add(Player_Id,new Player());
            Player_Dic[Player_Id].id = Player_Id;
        }
        Player_Dic[Player_Id].Name = EditorGUILayout.TextField("角色名称：", Player_Dic[Player_Id].Name);
        Player_Animator = (Animator)EditorGUILayout.ObjectField("动作", Player_Dic[Player_Id].animator, typeof(Animator), false);
        Player_Prefab = (GameObject)EditorGUILayout.ObjectField("模型", Player_Dic[Player_Id].prefab, typeof(GameObject), false);
        Player_Dic[Player_Id].lever = EditorGUILayout.IntField("玩家等级：", Player_Dic[Player_Id].lever);
        Player_Dic[Player_Id].blood = EditorGUILayout.FloatField("玩家血量：", Player_Dic[Player_Id].blood);
        Player_Dic[Player_Id].atkspeed = EditorGUILayout.FloatField("玩家攻速：", Player_Dic[Player_Id].atkspeed);
        Player_Dic[Player_Id].movespeed = EditorGUILayout.FloatField("玩家移速：", Player_Dic[Player_Id].movespeed);
        Player_Dic[Player_Id].atk = EditorGUILayout.FloatField("玩家攻击力：", Player_Dic[Player_Id].atk);
        if (GUILayout.Button("保存玩家数据"))
        {
            if (Player_Prefab != null)
            {
                string path = AssetDatabase.GetAssetPath(Player_Prefab);
                path = path.Replace("Assets/Resources/","");
                path = path.Replace(".prefab","");
            }
            for (int i = 0; i < Player_Dic.Count; i++)
            {
                string maps = JsonConvert.SerializeObject(Player_Dic);
                File.WriteAllText(Application.dataPath + "/Resources/Player.json", maps);
                AssetDatabase.Refresh();
            }
        }
        GUILayout.EndScrollView();
    }
}
