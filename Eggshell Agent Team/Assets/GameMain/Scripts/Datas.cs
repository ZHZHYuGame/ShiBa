using UnityEngine;

//地图信息表
public class Map
{
    private int id;
    private string map_Name;//地图名称
    private string map_Icon;//图标
    private string map_Prefab;//地图预制体
    private int enemy_wave;//怪物波次
    private float enemy_wave_Time;//波次间隔
    private int map_Start_term;//关卡开启条件
    private string map_Tips;//关卡描述
    private int map_Victory;//是否通关
    private int refeshId;//怪物波次信息
    

    public Map()
    {
    }

    public Map(int id, string map_Name, string map_Icon, int enemy_wave, float enemy_wave_Time, int map_Start_term, string map_Tips, int map_Victory, string map_Prefab)
    {
        this.Id = id;
        this.Map_Name = map_Name;
        this.Map_Icon = map_Icon;
        this.Enemy_wave = enemy_wave;
        this.Enemy_wave_Time = enemy_wave_Time;
        this.Map_Start_term = map_Start_term;
        this.Map_Tips = map_Tips;
        this.Map_Victory = map_Victory;
        this.Map_Prefab = map_Prefab;
    }

    public int Id { get => id; set => id = value; }
    public string Map_Name { get => map_Name; set => map_Name = value; }
    public string Map_Icon { get => map_Icon; set => map_Icon = value; }
    public int Enemy_wave { get => enemy_wave; set => enemy_wave = value; }
    public float Enemy_wave_Time { get => enemy_wave_Time; set => enemy_wave_Time = value; }
    public int Map_Start_term { get => map_Start_term; set => map_Start_term = value; }
    public string Map_Tips { get => map_Tips; set => map_Tips = value; }
    public int Map_Victory { get => map_Victory; set => map_Victory = value; }
    public int RefeshId { get => refeshId; set => refeshId = value; }
    public string Map_Prefab { get => map_Prefab; set => map_Prefab = value; }
}

//地图波次表
public class RefreshWaves
{
    private int id;//编号
    private int map_id;//指定场景
    private int wave_id;//波次信息
    private int enemy_id;//怪物id
    private int enemy_num;//怪物数量
    private float coefficient;//系数
    private int boss_id;//boss编号
    private int refreshBossWave;//boss

    public RefreshWaves()
    {
    }

    public RefreshWaves(int id, int map_id, int wave_id, int enemy_id, int enemy_num, float coefficient, int boss_id, int refreshBossWave)
    {
        this.id = id;
        this.map_id = map_id;
        this.wave_id = wave_id;
        this.enemy_id = enemy_id;
        this.enemy_num = enemy_num;
        this.coefficient = coefficient;
        this.boss_id = boss_id;
        this.refreshBossWave = refreshBossWave;
    }

    public int Id { get => id; set => id = value; }
    public int Map_id { get => map_id; set => map_id = value; }
    public int Wave_id { get => wave_id; set => wave_id = value; }
    public int Enemy_id { get => enemy_id; set => enemy_id = value; }
    public int Enemy_num { get => enemy_num; set => enemy_num = value; }
    public float Coefficient { get => coefficient; set => coefficient = value; }
    public int Boss_id { get => boss_id; set => boss_id = value; }
    public int RefreshBossWave { get => refreshBossWave; set => refreshBossWave = value; }
}
public class Role 
{
    // ========== 基础属性 ==========
    private int id;//id
    private string name;//角色名称
    private string this_animator_path;//当前动画路径
    private string this_object_path;//当前模型路径
    private int lever;//等级
    private float blood;//血量
    private float atkspeed;//攻速
    private float movespeed;//移速
    private float atk;//伤害
    private int type;//怪物类型
    private float def;//防御
    private float maxboold;//最大血量
    private float bodySize;//大小
    private string bulletPath;//子弹模型

    public Role()
    {
    }

    public Role(int id, string name, string this_animator_path, string this_object_path, int lever, float blood, float atkspeed, float movespeed, float atk, int type, float def, float maxboold, float bodySize, string bulletPath = null)
    {
        this.Id = id;
        this.Name = name;
        this.This_animator_path = this_animator_path;
        this.This_object_path = this_object_path;
        this.Lever = lever;
        this.Blood = blood;
        this.Atkspeed = atkspeed;
        this.Movespeed = movespeed;
        this.Atk = atk;
        this.Type = type;
        this.Def = def;
        this.Maxboold = maxboold;
        this.BodySize = bodySize;
        this.BulletPath = bulletPath;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string This_animator_path { get => this_animator_path; set => this_animator_path = value; }
    public string This_object_path { get => this_object_path; set => this_object_path = value; }
    public int Lever { get => lever; set => lever = value; }
    public float Blood { get => blood; set => blood = value; }
    public float Atkspeed { get => atkspeed; set => atkspeed = value; }
    public float Movespeed { get => movespeed; set => movespeed = value; }
    public float Atk { get => atk; set => atk = value; }
    public int Type { get => type; set => type = value; }
    public float Def { get => def; set => def = value; }
    public float Maxboold { get => maxboold; set => maxboold = value; }
    public float BodySize { get => bodySize; set => bodySize = value; }
    public string BulletPath { get => bulletPath; set => bulletPath = value; }
}
//public class Player : Role
//{
//    // ========== 基础属性 ==========
//    private Skill[] activeSkills = new ActiveSkill[2]; // 当前装备技能
//    private Skill[] passiveSkills = new PassiveSkill[2]; // 当前被动技能

//    public Player()
//    {
//    }

//    public Player(Skill[] activeSkills, Skill[] passiveSkills)
//    {
//        this.ActiveSkills = activeSkills;
//        this.PassiveSkills = passiveSkills;
//    }

//    public Skill[] ActiveSkills { get => activeSkills; set => activeSkills = value; }
//    public Skill[] PassiveSkills { get => passiveSkills; set => passiveSkills = value; }
//}
//boss
public class BossEnemy: Role
{
    //boss技能...
}
