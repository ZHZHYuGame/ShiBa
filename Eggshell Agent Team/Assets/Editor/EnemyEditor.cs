using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class EnemyEditor : EditorWindow
{
    static Dictionary<int, Role> dic;
    public enum RoleType
    {
        近战,
        远程
    }
    public RoleType type;

    [MenuItem("编辑器/怪物编辑器")]

    public static void Init()
    {
        GetWindow<EnemyEditor>("怪物编辑器").Show();
        dic = ConfigMgr.GetTable<Dictionary<int, Role>>("Enemy");
    }
    Vector2 v2;
    int id = 0;
    Animator anim;//动画
    GameObject prefab;
    GameObject bulletPrefab;


    private void OnGUI()
    {
        v2 = GUILayout.BeginScrollView(v2);
        //输入id
        id = EditorGUILayout.IntField("编号", id);
        if (!dic.ContainsKey(id))
        {
            dic.Add(id, new Role());
            dic[id].Id = id;
        }
        dic[id].Name = EditorGUILayout.TextField("名称", dic[id].Name);
        //动画
        if (dic[id].This_animator_path != null && dic[id].This_animator_path.Length > 0)
        {
            anim = AssetDatabase.LoadAssetAtPath<Animator>(dic[id].This_animator_path);
        }
        anim = (Animator)EditorGUILayout.ObjectField("动画", anim, typeof(Animator), false);
        if (anim != null)
        {
            //获取地址
            string path = AssetDatabase.GetAssetPath(anim);
            dic[id].This_animator_path = path;
        }
        //模型
        if (dic[id].This_object_path != null && dic[id].This_object_path.Length > 0)
        {
            //加载
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(dic[id].This_object_path);
        }
        prefab = (GameObject)EditorGUILayout.ObjectField("模型", prefab, typeof(GameObject), false);
        if (prefab != null)
        {
            //获取地址
            string path = AssetDatabase.GetAssetPath(prefab);
            dic[id].This_object_path = path;
        }
        dic[id].Lever = EditorGUILayout.IntField("等级", dic[id].Lever);
        dic[id].Blood = EditorGUILayout.FloatField("血量", dic[id].Blood);
        dic[id].Atkspeed = EditorGUILayout.FloatField("攻速", dic[id].Atkspeed);
        dic[id].Movespeed = EditorGUILayout.FloatField("移速", dic[id].Movespeed);
        dic[id].Atk = EditorGUILayout.FloatField("基础伤害", dic[id].Atk);
        dic[id].Def = EditorGUILayout.FloatField("防御", dic[id].Def);
        dic[id].Maxboold = EditorGUILayout.FloatField("血量", dic[id].Maxboold);
        dic[id].BodySize = EditorGUILayout.FloatField("大小", dic[id].BodySize);
        //枚举下拉列表
        type = (RoleType)EditorGUILayout.EnumPopup("攻击类型", type);//返回枚举值
        switch (type)
        {
            case RoleType.近战:
                break;
            case RoleType.远程:
                //模型
                if (dic[id].BulletPath != null && dic[id].BulletPath.Length > 0)
                {
                    //加载
                    bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(dic[id].BulletPath);
                }
                bulletPrefab = (GameObject)EditorGUILayout.ObjectField("子弹模型", bulletPrefab, typeof(GameObject), false);
                if (bulletPrefab != null)
                {
                    //获取地址
                    string path = AssetDatabase.GetAssetPath(bulletPrefab);
                    dic[id].BulletPath = path;
                }
                break;
        }
        GUILayout.EndScrollView();
        if (GUILayout.Button("保存数据"))//按钮
        {
            ConfigMgr.Save("Enemy", dic);
            AssetDatabase.Refresh();//刷新
        }
    }
}
