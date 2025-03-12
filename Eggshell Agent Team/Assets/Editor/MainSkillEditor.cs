using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;




public class MainSkillEditor : EditorWindow
{
    static Dictionary<int, ActiveSkill> dic;


    [MenuItem("编辑器/主动技能编辑器")]
    public static void init()
    {

        GetWindow<MainSkillEditor>("skilleditor").Show();
        dic = ConfigMgr.GetTable<Dictionary<int, ActiveSkill>>("ActiveskillData");
    }

    public static int id;
    Sprite sprite;
    GameObject prefab;
    Animator anim;//动画
    private void OnGUI()
    {
        id = EditorGUILayout.IntField("主动技能id",id);
        if (!dic.ContainsKey(id))
        {
            dic.Add(id, new ActiveSkill());
            dic[id].Skill_id = id;
        }
        dic[id].Skill_name = EditorGUILayout.TextField("技能名称：", dic[id].Skill_name);
        dic[id].Skill_des = EditorGUILayout.TextField("技能描述：", dic[id].Skill_des);
        dic[id].Skill_type = EditorGUILayout.IntField("技能类型：", dic[id].Skill_type, GUILayout.Width(200));
        if (dic[id].Skill_name == null)
        {
            sprite = null;
            prefab = null;
            anim = null;
        }
        if (dic[id].Slill_icon != null && dic[id].Slill_icon.Length > 0)
        {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>(dic[id].Slill_icon);
        }
        sprite = (Sprite)EditorGUILayout.ObjectField("技能图片:", sprite, typeof(Sprite), false, GUILayout.Width(200));
        if (sprite != null)
        {
            string path = AssetDatabase.GetAssetPath(sprite);
            dic[id].Slill_icon = path;
        }
        //升级后的图片
        if (dic[id].Slill_AfterIcon != null && dic[id].Slill_AfterIcon.Length > 0)
        {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>(dic[id].Slill_AfterIcon);
        }
        sprite = (Sprite)EditorGUILayout.ObjectField("满级技能图片:", sprite, typeof(Sprite), false, GUILayout.Width(200));
        if (sprite != null)
        {
            string path = AssetDatabase.GetAssetPath(sprite);
            dic[id].Slill_AfterIcon = path;
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
        dic[id].Level = EditorGUILayout.IntField("等级", dic[id].Level);
        dic[id].Skill_hurt = EditorGUILayout.FloatField("伤害",dic[id].Skill_hurt);
        dic[id].Num = EditorGUILayout.IntField("数量", dic[id].Num);
        dic[id].Rate = EditorGUILayout.FloatField("频率", dic[id].Rate);
        dic[id].Coefficient = EditorGUILayout.FloatField("系数", dic[id].Coefficient);
        dic[id].Skill_range = EditorGUILayout.FloatField("范围", dic[id].Skill_range);
        dic[id].Skill_cooling = EditorGUILayout.FloatField("冷却", dic[id].Skill_cooling);
        dic[id].Skill_size = EditorGUILayout.FloatField("大小", dic[id].Skill_size);
        if (GUILayout.Button("保存数据"))//按钮
        {
            ConfigMgr.Save("ActiveskillData", dic);
            AssetDatabase.Refresh();//刷新
        }
    }
    //private void OnGUI()
    //{
    //    id = EditorGUILayout.IntField(id);
    //    if (!dic.ContainsKey(id))
    //    {
    //        dic.Add(id, new SkillData());
    //        dic[id].id = id;
    //    }
    //    if (dic[id].skillName == null)
    //    {
    //        sprite = null;
    //        effectPrefab = null;
    //    }
    //    dic[id].skillName = EditorGUILayout.TextField("技能名称：", dic[id].skillName);

    //    if (dic[id].icon != null && dic[id].icon.Length > 0)
    //    {
    //        sprite = Resources.Load<Sprite>(dic[id].icon);
    //    }
    //    sprite = (Sprite)EditorGUILayout.ObjectField("技能图片:", sprite, typeof(Sprite), false);//, GUILayout.Width(100), GUILayout.Height(100));

    //    if (sprite != null)
    //    {
    //        string path = AssetDatabase.GetAssetPath(sprite);
    //        path = path.Replace("Assets/Resources/", "");
    //        path = path.Replace(".jpg", "");
    //        path = path.Replace(".png", "");
    //        dic[id].icon = path;
    //    }
    //    if (dic[id].effectPrefab != null && dic[id].effectPrefab.Length > 0)
    //    {
    //        effectPrefab = Resources.Load<GameObject>(dic[id].effectPrefab);
    //    }
    //    effectPrefab = (GameObject)EditorGUILayout.ObjectField("技能效果:", effectPrefab, typeof(GameObject), false);//GUILayout.Width(100), GUILayout.Height(100));

    //    if (effectPrefab != null)
    //    {
    //        string path = AssetDatabase.GetAssetPath(effectPrefab);
    //        path = path.Replace("Assets/Resources/", "");
    //        path = path.Replace(".prefab", "");
    //        dic[id].effectPrefab = path;
    //    }

    //    dic[id].baseDamage = EditorGUILayout.FloatField("技能伤害：", dic[id].baseDamage);
    //    dic[id].atkRange = EditorGUILayout.FloatField("施法范围：", dic[id].atkRange);

    //    if (GUILayout.Button("保存技能"))
    //    {
    //        ConfigDataManager.Save("skillData", dic);
    //        AssetDatabase.Refresh();
    //    }
    //}
}