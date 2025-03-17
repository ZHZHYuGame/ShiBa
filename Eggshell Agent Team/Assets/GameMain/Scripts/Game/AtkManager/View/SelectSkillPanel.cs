using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SelectSkillPanel : BasePanel
{
    private static string name = "SelectSkillPanel";
    private static string path = "Panel/SelectSkillPanel";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    Player player;
    Transform activeSkillContent;
    Transform passiveSkillContent;
    Transform skillContent;
    List<Transform> activeSkillList;
    List<Transform> passiveSkillList;
    List<Skill> updateList;//随机技能
    List<Button> skillSelectBtns;
    List<Skill> allSkills;
    GameObject playerprefab;

    public SelectSkillPanel() : base(uIType)
    {
        player = SceneEntry.instance.player;//玩家数据
        playerprefab = SceneEntry.instance.playerPrefab;//玩家预制体
    }

    public override void OnStart()
    {
        base.OnStart();

        //组件查找
        activeSkillContent = UIMethod.Ins.GetOrAddSingleComponentInChild<Transform>(ActiveObj, "ActiveSkillContent");
        passiveSkillContent = UIMethod.Ins.GetOrAddSingleComponentInChild<Transform>(ActiveObj, "PassiveSkillContent");
        skillContent = UIMethod.Ins.GetOrAddSingleComponentInChild<Transform>(ActiveObj, "Skills");
        //所有技能表
        allSkills = GameMgr.GetInstance().dataAnalysis.allSkill;

        //动态加载格子
        LoadPrefab();
        //根据玩家技能更新已有的技能状态
        RefreshSkillIcon();
        //以获取到符合刷新到面板上的技能
        GetUpdateSkillList();
        //更新面板信息
        UpdatePanelData();
        for (int i = 0; i < skillSelectBtns.Count; i++)
        {
            int index =i;
            skillSelectBtns[i].onClick.AddListener(()=>{
                //消息广播
                MsgManager<Skill>.Ins.OnBroadCast(MesID.SkillUpLevel,updateList[index]);
                //添加技能
                SceneEntry.instance.AddWeapon(updateList[index]);
                //关闭面板
                GameMgr.GetInstance().UIManager_Root.Pop(false);
                //游戏恢复
                Time.timeScale =1;
            });
        }

    }

    private void UpdatePanelData()
    {
        skillSelectBtns = new List<Button>();
        for (int i = 0; i < updateList.Count; i++)
        {
            GameObject skillItem = GameObject.Instantiate(ResourcesLoader.LoadResources<GameObject>(Application.streamingAssetsPath + "/myprefab", "skillItem", "myprefab"), skillContent);
            skillItem.GetComponent<SkillItem>().Init(updateList[i]);
            skillSelectBtns.Add(skillItem.GetComponent<Button>());
        }
    }

    private void GetUpdateSkillList()
    {
        List<Skill> newSkillList = new List<Skill>();//玩家未满级的技能集合
        List<Skill> maxSkill = new List<Skill>();//满级
        List<Skill> nextSkill = new List<Skill>();//筛选
        //找到玩家满级和未满级的技能
        foreach (var item in player.Skills)
        {
            if (item.Level < 5) newSkillList.Add(item);//玩家未满级的技能集合
            else maxSkill.Add(item);//满级
        }
        //找出不同的技能数据
        List<Skill> skills = allSkills.Where(s1 => !maxSkill.Any(s2 => s2.Skill_name == s1.Skill_name)).ToList();
        //比较器
        SkillNameComparer comparer = new SkillNameComparer();

        List<Skill> skillsToAdd = skills.Except(newSkillList, comparer).ToList();
        newSkillList.AddRange(skillsToAdd);
        updateList = new List<Skill>();
        if (skills.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                //随机下标
                updateList.Add(skills[Random.Range(0, skills.Count)]);
            }
        }
        else
        {
            //没有技能可获取
            GameMgr.GetInstance().UIManager_Root.Pop(false);
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
public class SkillNameComparer : IEqualityComparer<Skill>
{
    public bool Equals(Skill x, Skill y)=>x?.Skill_name == y?.Skill_name;
    public int GetHashCode(Skill obj)=>obj.Skill_name?.GetHashCode() ?? 0;

}
