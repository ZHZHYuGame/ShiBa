using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectSkillPanel : BasePanel
{
    private static string name = "SelectSkillPanel";
    private static string path = "Panel/SelectSkillPanel";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    Player player;
    Transform activeSkillContent;
    Transform passiveSkillContent;
    List<Transform> activeSkillList;
    List<Transform> passiveSkillList;
    List<Skill> updateList;


    public SelectSkillPanel() : base(uIType)
    {
        player = SceneEntry.instance.player;//玩家数据
    }

    public override void OnStart()
    {
        base.OnStart();
        //动态加载格子
        LoadPrefab();
        //根据玩家技能更新已有的技能状态
        RefreshSkillIcon();
        //随机刷新技能列表
        List<Skill> newSkillList = new List<Skill>();
        for (int i = 0; i < player.Skills.Count; i++)
        {
            if (player.Skills[i].Level < 5)
            {
                newSkillList.Add(player.Skills[i]);
            }
        }



        List<Skill> updateList = new List<Skill>();
        for (int i = 0;i < 3; i++)
        {
            //随机下标
            updateList.Add(newSkillList[Random.Range(0, newSkillList.Count)]);

        }
        

    }

    private void RefreshSkillIcon()
    {
        for (int i = 0; i < activeSkillList.Count; i++)
        {
            int index = i;
            if (player.ActiveSkills.Count > index)
            {
                activeSkillList[index].GetComponent<SkillCell>().Init(player.ActiveSkills[index]);
            }
        }
        for (int i = 0; i < passiveSkillList.Count; i++)
        {
            int index = i;
            if (player.PassiveSkills.Count > index)
            {
                passiveSkillList[index].GetComponent<SkillCell>().Init(player.PassiveSkills[index]);
            }
        }
    }

    private void LoadPrefab()
    {
        activeSkillList = new List<Transform>();
        passiveSkillList = new List<Transform>();
        //组件查找
        activeSkillContent = UIMethod.Ins.GetOrAddSingleComponentInChild<Transform>(ActiveObj, "ActiveSkillContent");
        passiveSkillContent = UIMethod.Ins.GetOrAddSingleComponentInChild<Transform>(ActiveObj, "PassiveSkillContent");
        for (int i = 0; i < 5; i++)
        {
            GameObject prefab = GameObject.Instantiate(ResourcesLoader.LoadResources<GameObject>(Application.streamingAssetsPath + "/myprefab", "bg", "myprefab"), activeSkillContent);
            activeSkillList.Add(prefab.transform);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject prefab = GameObject.Instantiate(ResourcesLoader.LoadResources<GameObject>(Application.streamingAssetsPath + "/myprefab", "bg", "myprefab"), passiveSkillContent);
            passiveSkillList.Add(prefab.transform);
        }
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

}
