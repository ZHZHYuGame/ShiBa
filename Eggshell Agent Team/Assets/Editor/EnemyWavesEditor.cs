using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnemyEditor;

public class EnemyWavesEditor :EditorWindow
{
    //RefreshWaves
    //private int id;//编号
    //private int map_id;//指定地图ID
    //private int wave_id;//波次信息              wave_id是第几次，第几波怪物？
    //private int enemy_id;//怪物id
    //private int enemy_num;//怪物数量
    //private float coefficient;//系数
    //private int boss_id;
    //private int refreshBossWave;//boss//是否是Boss关卡
    static Dictionary<int, RefreshWaves> dic;

    [MenuItem("编辑器/怪物波次信息编辑器")]

    private static void Init()
    {
        GetWindow<EnemyWavesEditor>("怪物波次信息编辑器").Show();
        dic = ConfigMgr.GetTable<Dictionary<int, RefreshWaves>>("EnemyWavesTab");
    }
    Vector2 v2;
    int id = 0;

    private void OnGUI()
    {
        v2 = GUILayout.BeginScrollView(v2);
        //输入ID
        id = EditorGUILayout.IntField("怪物波次ID", id);
        if(!dic.ContainsKey(id))
        {
            dic.Add(id, new RefreshWaves());
            dic[id].Id= id;
        }
        dic[id].Map_id = EditorGUILayout.IntField("指定地图ID", dic[id].Map_id); 
        dic[id].Wave_id = EditorGUILayout.IntField("当前波次", dic[id].Wave_id);
        dic[id].Enemy_id = EditorGUILayout.IntField("怪物Id", dic[id].Enemy_id);
        dic[id].Enemy_num = EditorGUILayout.IntField("怪物数量", dic[id].Enemy_num);
        dic[id].ExpType =(ExpType)EditorGUILayout.EnumPopup("掉落经验类型", dic[id].ExpType);
        dic[id].Coefficient = EditorGUILayout.FloatField("增加难度系数", dic[id].Coefficient);
        dic[id].Boss_id = EditorGUILayout.IntField("BossID", dic[id].Boss_id);
        dic[id].RefreshBossWave = EditorGUILayout.IntField("是否有Boss(0没有/1有)", dic[id].RefreshBossWave);

        GUILayout.EndScrollView();

        if (GUILayout.Button("保存数据"))//按钮 生成预览图
        {
            ConfigMgr.Save("EnemyWavesTab", dic);
            AssetDatabase.Refresh();//刷新
        }
    }


}
