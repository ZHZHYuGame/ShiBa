using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

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
    private ExpType expType;//掉落经验类型
    private int boss_id;//boss编号
    private int refreshBossWave;//boss

    public RefreshWaves()
    {

    }

    public RefreshWaves(int id, int map_id, int wave_id, int enemy_id, int enemy_num, float coefficient, int boss_id, int refreshBossWave, ExpType expType = default)
    {
        this.id = id;
        this.map_id = map_id;
        this.wave_id = wave_id;
        this.enemy_id = enemy_id;
        this.enemy_num = enemy_num;
        this.coefficient = coefficient;
        this.boss_id = boss_id;
        this.refreshBossWave = refreshBossWave;
        this.ExpType = expType;
    }

    public int Id { get => id; set => id = value; }
    public int Map_id { get => map_id; set => map_id = value; }
    public int Wave_id { get => wave_id; set => wave_id = value; }
    public int Enemy_id { get => enemy_id; set => enemy_id = value; }
    public int Enemy_num { get => enemy_num; set => enemy_num = value; }
    public float Coefficient { get => coefficient; set => coefficient = value; }
    public int Boss_id { get => boss_id; set => boss_id = value; }
    public int RefreshBossWave { get => refreshBossWave; set => refreshBossWave = value; }
    public ExpType ExpType { get => expType; set => expType = value; }
}
public enum ExpType
{
    lowerExp,
    midelExp,
    higherExp,
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
    public Role(Role role)
    {
        this.Id = role.id;
        this.Name = role.name;
        this.This_animator_path = role.this_animator_path;
        this.This_object_path = role.this_object_path;
        this.Lever = role.lever;
        this.Blood = role.blood;
        this.Atkspeed = role.atkspeed;
        this.Movespeed = role.movespeed;
        this.Atk = role.atk;
        this.Type = role.type;
        this.Def = role.def;
        this.Maxboold = role.maxboold;
        this.BodySize = role.bodySize;
        this.BulletPath = role.bulletPath;
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
public class Player : Role
{
    // ========== 基础属性 ==========
    private  List<ActiveSkill> activeSkills =  new List<ActiveSkill>(4);// 当前装备技能
    private  List<PassiveSkill> passiveSkills =  new List<PassiveSkill>(4);// 当前装备技能
    private  List<Skill> skills =  new List<Skill>(8);// 当前装备技能
    

    public Player()
    {
        
    }
    public Player(Role role)
    {
        this.Id = role.Id;
        this.Name = role.Name;
        this.This_animator_path = role.This_animator_path;
        this.This_object_path = role.This_object_path;
        this.Lever = role.Lever;
        this.Blood = role.Blood;
        this.Atkspeed = role.Atkspeed;
        this.Movespeed = role.Movespeed;
        this.Atk = role.Atk;
        this.Type = role.Type;
        this.Def = role.Def;
        this.Maxboold = role.Maxboold;
        this.BodySize = role.BodySize;
        this.BulletPath = role.BulletPath;
    }
    public List<ActiveSkill> ActiveSkills { get => activeSkills; set => activeSkills = value; }
    public List<PassiveSkill> PassiveSkills { get => passiveSkills; set => passiveSkills = value; }
    public List<Skill> Skills { get => skills; set => skills = value; }
}



public class BossEnemy : Role
{
    //boss技能...
}
public class Exp
{
    private int id;
    private string exp_path;//图片路径
    private string exp_name;//名称
    private ExpType exp_type;//类型
    private int exp_value;//经验值

    public Exp()
    {
    }

    public Exp(int id, string exp_path, string exp_name, ExpType exp_type, int exp_value)
    {
        this.Id = id;
        this.Exp_path = exp_path;
        this.Exp_name = exp_name;
        this.Exp_type = exp_type;
        this.Exp_value = exp_value;
    }

    public int Id { get => id; set => id = value; }
    public string Exp_path { get => exp_path; set => exp_path = value; }
    public string Exp_name { get => exp_name; set => exp_name = value; }
    public ExpType Exp_type { get => exp_type; set => exp_type = value; }
    public int Exp_value { get => exp_value; set => exp_value = value; }
}
public class Skill
{
    private int skill_id;//技能id
    private string skill_name;//技能名称
    private string skill_des;//描述
    private string slill_icon;//图标
    private int skill_type;//类型
    private int level;//技能等级

    public Skill()
    {
    }

    public Skill(int skill_id, string skill_name, string skill_des, string slill_icon, int skill_type, int level)
    {
        this.Skill_id = skill_id;
        this.Skill_name = skill_name;
        this.Skill_des = skill_des;
        this.Slill_icon = slill_icon;
        this.Skill_type = skill_type;
        this.Level = level;
    }

    public int Skill_id { get => skill_id; set => skill_id = value; }
    public string Skill_name { get => skill_name; set => skill_name = value; }
    public string Skill_des { get => skill_des; set => skill_des = value; }
    public string Slill_icon { get => slill_icon; set => slill_icon = value; }
    public int Skill_type { get => skill_type; set => skill_type = value; }
    public int Level { get => level; set => level = value; }
}
public class ActiveSkill:Skill
{
    private string slill_AfterIcon;//究极形态
    private string this_animator_path;//当前动画路径
    private string this_object_path;//当前模型路径
    private float skill_hurt;//伤害
    private int num;//数量
    private float rate;//频率
    private float coefficient;//系数
    private float skill_range;//范围
    private float skill_cooling;//冷却
    private float skill_size;//技能大小

    public ActiveSkill()
    {
    }

    public ActiveSkill(string slill_AfterIcon, string this_animator_path, string this_object_path, float skill_hurt, int num, float rate, float coefficient, float skill_range, float skill_cooling, float skill_size)
    {
        this.Slill_AfterIcon = slill_AfterIcon;
        this.This_animator_path = this_animator_path;
        this.This_object_path = this_object_path;
        this.Skill_hurt = skill_hurt;
        this.Num = num;
        this.Rate = rate;
        this.Coefficient = coefficient;
        this.Skill_range = skill_range;
        this.Skill_cooling = skill_cooling;
        this.Skill_size = skill_size;
    }

    public string Slill_AfterIcon { get => slill_AfterIcon; set => slill_AfterIcon = value; }
    public string This_animator_path { get => this_animator_path; set => this_animator_path = value; }
    public string This_object_path { get => this_object_path; set => this_object_path = value; }
    public float Skill_hurt { get => skill_hurt; set => skill_hurt = value; }
    public int Num { get => num; set => num = value; }
    public float Rate { get => rate; set => rate = value; }
    public float Coefficient { get => coefficient; set => coefficient = value; }
    public float Skill_range { get => skill_range; set => skill_range = value; }
    public float Skill_cooling { get => skill_cooling; set => skill_cooling = value; }
    public float Skill_size { get => skill_size; set => skill_size = value; }
}
public class PassiveSkill:Skill
{
    private float bulletSpeed;//子弹速度
    private float moveSpeed;//移动速度
    private float maxBlood;//血量上限
    private float exp;//经验
    private float atk;//伤害提升
    private float bloodReturning;//血量回复

    public PassiveSkill()
    {
    }

    public PassiveSkill(float bulletSpeed, float moveSpeed, float maxBlood, float exp, float atk, float bloodReturning)
    {
        this.BulletSpeed = bulletSpeed;
        this.MoveSpeed = moveSpeed;
        this.MaxBlood = maxBlood;
        this.Exp = exp;
        this.Atk = atk;
        this.BloodReturning = bloodReturning;
    }

    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float MaxBlood { get => maxBlood; set => maxBlood = value; }
    public float Exp { get => exp; set => exp = value; }
    public float Atk { get => atk; set => atk = value; }
    public float BloodReturning { get => bloodReturning; set => bloodReturning = value; }
}