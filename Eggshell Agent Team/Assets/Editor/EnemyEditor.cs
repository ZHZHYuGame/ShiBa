using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class EnemyEditor : EditorWindow
{
    List<Role> bosslist = new List<Role>();

    /* public int id;//id
    public string Name;//玩家名称
    public Animator animator;//动画
    public GameObject prefab;//模型
    public int lever;//等级
    public float blood;//血量
    public float atkspeed;//攻速
    public float movespeed;//移速*/
    int Enemy_id;
    string Enemy_name;
    int Enemy_lever;
    float Enemy_blood;
    float Enemy_atkspeed;
    float Enemy_movespeed;
    float Enemy_atk;
    string Enemy_type;

    [MenuItem("StageTool/怪物编辑器")]
    public static void Init()
    {
        GetWindow<EnemyEditor>().Show();
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        Enemy_id = EditorGUILayout.IntField("BossID：", Enemy_id);
        Enemy_name = EditorGUILayout.TextField("Boss名称：", Enemy_name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Enemy_lever = EditorGUILayout.IntField("Boss等级：", Enemy_lever);
        Enemy_blood = EditorGUILayout.FloatField("Boss血量：", Enemy_blood);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Enemy_atkspeed = EditorGUILayout.FloatField("Boss攻速：", Enemy_atkspeed);
        Enemy_movespeed = EditorGUILayout.FloatField("Boss移速：", Enemy_movespeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Enemy_atk = EditorGUILayout.FloatField("Boss伤害：", Enemy_atk);
        Enemy_type = EditorGUILayout.TextField("Boss类型：", Enemy_type);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("保存怪物数据"))
        {
            
            Boss boss = new Boss();
            boss.id = Enemy_id;
            boss.Name = Enemy_name;
            boss.lever = Enemy_lever;
            boss.blood = Enemy_blood;
            boss.atkspeed = Enemy_atkspeed;
            boss.movespeed = Enemy_movespeed;
            boss.atk = Enemy_atk;
            boss.enemy_type = Enemy_type;
            bosslist.Add(boss);
            if (!File.Exists(Application.dataPath + "/Resources/enemy.json"))
            {
                File.CreateText(Application.dataPath + "/Resources/enemy.json");
                AssetDatabase.Refresh();
            }
            else
            {
                for (int i = 0; i < bosslist.Count; i++)
                {
                    string bossinfo = JsonConvert.SerializeObject(bosslist);
                    File.WriteAllText(Application.dataPath + "/Resources/enemy.json", bossinfo);
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
