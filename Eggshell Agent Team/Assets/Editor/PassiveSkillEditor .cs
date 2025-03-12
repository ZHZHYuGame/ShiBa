using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PassiveSkillEditor : EditorWindow
{
    static Dictionary<int, PassiveSkill> dic;


    [MenuItem("编辑器/被动技能编辑器")]
    public static void init()
    {

        GetWindow<PassiveSkillEditor>("skilleditor").Show();
        dic = ConfigMgr.GetTable<Dictionary<int, PassiveSkill>>("PassiveSkillData");
    }
    public static int id;
    Sprite sprite;
    GameObject prefab;
    Animator anim;//动画
    private void OnGUI()
    {
        id = EditorGUILayout.IntField("被动技能Id:", id);
        if (!dic.ContainsKey(id))
        {
            dic.Add(id, new PassiveSkill());
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
        dic[id].Level = EditorGUILayout.IntField("等级:", dic[id].Level);
        dic[id].BulletSpeed = EditorGUILayout.FloatField("子弹速度:", dic[id].BulletSpeed);
        dic[id].MoveSpeed = EditorGUILayout.FloatField("移动速度:", dic[id].MoveSpeed);
        dic[id].MaxBlood = EditorGUILayout.FloatField("血量上限:", dic[id].MaxBlood);
        dic[id].Exp = EditorGUILayout.FloatField("经验:", dic[id].Exp);
        dic[id].Atk = EditorGUILayout.FloatField("伤害提升:", dic[id].Atk);
        dic[id].BloodReturning = EditorGUILayout.FloatField("血量恢复:", dic[id].BloodReturning);
        if (GUILayout.Button("保存数据"))//按钮
        {
            ConfigMgr.Save("PassiveSkillData", dic);
            AssetDatabase.Refresh();//刷新
        }
    }
}
