

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SceneEntry : MonoBehaviour
{
    public GameObject playerPrefab;
    Role role;
    public Camera cam;
    public GameObject hpBase;
    public GameObject hurttx;//飘血
    public GameObject bullet;//子弹
    public GameObject expPrefab;//经验
    Canvas canvas;
    AllObjectPool allObjectPool;
    public Player player;
    public List<Skill> allSkill;
    //---单例--
    public static SceneEntry instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //画布获取
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //关卡加载
        Map map = ConfigMgr.GetDicData<Map>("Map", PlayerPrefs.GetInt("levelIndex"));
        //地图加载
        MapData data = ConfigMgr.GetDicData<MapData>("MapDatas", map.Id);
        MapManager.Instance.Init(data);
        //=======UI=======
        //摇杆加载 
        GameMgr.GetInstance().UIManager_Root.Push(new PlayerMoveForm());
        GameMgr.GetInstance().UIManager_Root.Push(new ExpForm());
        //================
        PlayerLoad(map);
        //相机加载1
        cam.gameObject.AddComponent<CameraMgr>().Init(playerPrefab.transform);
        //血条加载
        hpBase = Instantiate(UIManager.Ins._resourcesManager.LoadResource<GameObject>(Application.streamingAssetsPath + "/myprefab", "HpBase", "myprefab"), canvas.transform);
        hpBase.GetComponent<HpBase>().Init(playerPrefab, role);
        //对象池管理
        allObjectPool = new AllObjectPool(hurttx, bullet, expPrefab);
        //更新玩家技能信息
        MsgManager<Skill>.Ins.OnAddListener(MesID.SkillUpLevel,SkillUpLevel);

    }
    private void  SkillUpLevel(Skill skill){
        //判断当前技能是什么类型
        //添加到对应的集合
        if (skill is ActiveSkill){
            ActiveSkill addSkill = (ActiveSkill)skill;
            for (int i = 0; i < player.ActiveSkills.Count; i++)
            {
                if(addSkill==player.ActiveSkills[i]){
                    player.ActiveSkills[i].Level++;
                    return;
                }
            }
            addSkill.Level++;
            player.ActiveSkills.Add(addSkill);
            player.Skills.Add(skill);


        }else if(skill is PassiveSkill){
            PassiveSkill addSkill = (PassiveSkill)skill;
            for (int i = 0; i < player.PassiveSkills.Count; i++)
            {
                if(addSkill == player.PassiveSkills[i]){
                    player.PassiveSkills[i].Level++;
                    return;
                }
            }
            addSkill.Level++;
            player.PassiveSkills.Add(addSkill);
            player.Skills.Add(skill);
        }
        
    }   
    private void PlayerLoad(Map map)
    {
        //玩家数据
        role = ConfigMgr.GetListData<Role>("Role", 0);
        player = new Player(role);
        //玩家默认武器添加
        ActiveSkill weapon = ConfigMgr.GetDicData<ActiveSkill>(("ActiveskillData"), 1001);
        weapon.Level = 1;
        player.ActiveSkills.Add(weapon);//主动武器添加
        player.Skills.Add(weapon);//所有武器集合
        string assetName = Path.GetFileNameWithoutExtension(role.This_object_path);
        //玩家生成
        playerPrefab = Instantiate(ResourcesLoader.LoadResources<GameObject>(Application.streamingAssetsPath + "/role", assetName, "role"));
        //怪物生成规则
        playerPrefab.AddComponent<EnemySpawner>();
        playerPrefab.GetComponent<EnemySpawner>().Init(map);
        //获取所有技能数据
        allSkill = new List<Skill>();
        //武器添加
        AddWeapon(weapon);

    }

    public void AddWeapon(Skill weapon)
    {
        for (int i = 0; i < player.Skills.Count; i++)
        {
            int index = i;
            if (player.Skills[index] is ActiveSkill)
            {

                switch (player.Skills[index].Skill_name)
                {
                    case "火箭弹":
                        playerPrefab.GetOrAddComponent<AutoAttackController>().Init((ActiveSkill)player.Skills[index]);
                        break;
                    case "万刃轮":
                        playerPrefab.GetOrAddComponent<WeaponOrbitController>().Init((ActiveSkill)player.Skills[index]);
                        break;
                }

            }
            else if (player.Skills[index] is PassiveSkill)
            {
                //玩家基础属性增加
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
