using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static EnemyEditor;

public class RoleEditor : EditorWindow
{
    //static Dictionary<int, Role> dic;
    static List<Role> list;
    private int id;
    private Vector2 scrollPosition;
    private GameObject model;
    private AnimationClip animationClip;
    private GameObject bulletModel;
    public RoleType type;
    [MenuItem("编辑器/角色编辑器")]
    public static void Init()
    {
        GetWindow<RoleEditor>("角色编辑器").Show();
        list = ConfigMgr.GetTable<List<Role>>("Role");
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        DrawRoleField();
        GUILayout.EndScrollView();
        DrawSaveBtn();
    }


    private void DrawRoleField()
    {
        id = EditorGUILayout.IntField("编号", id);
        foreach (var item in list)
        {
            if(item.Id==id)
            {
                break;
            }
            else
            {
                list.Add(new Role());
                list[id].Id = id;
            }
        }
        list[id].Name = EditorGUILayout.TextField("角色名称", list[id].Name);

        // 和模型
        if (list[id].This_object_path != null && list[id].This_object_path.Length > 0)
        {
            //加载
            model = AssetDatabase.LoadAssetAtPath<GameObject>(list[id].This_object_path);
        }
        model = (GameObject)EditorGUILayout.ObjectField("模型", model, typeof(GameObject), false);
        // 基础属性
        list[id].Lever = EditorGUILayout.IntField("等级", list[id].Lever);
        list[id].Blood = EditorGUILayout.FloatField("血量", list[id].Blood);
        list[id].Maxboold = EditorGUILayout.FloatField("最大血量", list[id].Maxboold);
        list[id].Atk = EditorGUILayout.FloatField("伤害", list[id].Atk);
        list[id].Atkspeed = EditorGUILayout.FloatField("攻速", list[id].Atkspeed);
        list[id].Movespeed = EditorGUILayout.FloatField("移速", list[id].Movespeed);
        list[id].Def = EditorGUILayout.FloatField("防御", list[id].Def);
        list[id].BodySize = EditorGUILayout.FloatField("大小", list[id].BodySize);

        // 怪物类型
        list[id].Type = EditorGUILayout.IntField("怪物类型", list[id].Type);

        // 子弹模型
        switch (type)
        {
            case RoleType.近战:
                break;
            case RoleType.远程:
                //模型
                if (list[id].BulletPath != null && list[id].BulletPath.Length > 0)
                {
                    //加载
                    bulletModel = AssetDatabase.LoadAssetAtPath<GameObject>(list[id].BulletPath);
                }
                bulletModel = (GameObject)EditorGUILayout.ObjectField("子弹模型", bulletModel, typeof(GameObject), false);
                if (bulletModel != null)
                {
                    //获取地址
                    string path = AssetDatabase.GetAssetPath(bulletModel);
                    list[id].BulletPath = path;
                }
                break;
        }
    }

    private void DrawTimeLine()
    {
        if (model != null)
        {
            animationClip = (AnimationClip)EditorGUILayout.ObjectField("动画", animationClip, typeof(AnimationClip), false);
            if (animationClip != null)
            {
                var director = model.GetComponent<PlayableDirector>() ?? model.AddComponent<PlayableDirector>();
                var asset = ScriptableObject.CreateInstance<TimelineAsset>();
                director.playableAsset = asset;

                var animationTrack = asset.CreateTrack<AnimationTrack>(null, "动画轨道");
                director.SetGenericBinding(animationTrack, model.GetComponent<Animator>());

                var animClip = animationTrack.CreateClip(animationClip);
                animClip.start = 0.1f;
                animClip.duration = animationClip.length;
            }
        }
    }


    private void DrawSaveBtn()
    {
        if (GUILayout.Button("保存"))
        {
            ConfigMgr.Save("Role", list);
            AssetDatabase.Refresh();
        }
    }
}