using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInforMation : MonoBehaviour//编译器数据存储
{

}
public class PassiveSkill : Skill
{
    public int Type;//类型
    public int Lever;//等级
    public float Build_Speed;//子弹速度
    public float Move_Speed;//移动速度
    public float Max_Blood;//血量上限
    public float Exp;//经验
    public float Atk_Up;//伤害提升
    public float Blood_Returning;//血量回复
}
public class ActiveSkill : Skill
{
    public int type;//武器类型
    public GameObject prefab;//模型
    public Animator animator;//动画
    public int level;//技能等级
    public float skill_hurt;//伤害
    public int num;//数量
    public float rate;//频率
    public float coefficient;//系数
    public float skill_range;//范围
    public float skill_cooling;//冷却
    public float skill_size;//技能大小
}

public class Skill
{
    public int Skill_Id;//技能ID
    public string Skill_Name;//技能名称
    public string Skill_des;//技能描述
    public string Skill_icon;//技能图标
}

public class Player : Role
{
    public Dictionary<int, ActiveSkill> player_activeSkill_Dic = new Dictionary<int, ActiveSkill>();
    public Dictionary<int, PassiveSkill> player_passSkill_Dic = new Dictionary<int, PassiveSkill>();
}
public class Role
{
    public int id;//id
    public string Name;//玩家名称
    public Animator animator;//动画
    public GameObject prefab;//模型
    public int lever;//等级
    public float blood;//血量
    public float atkspeed;//攻速
    public float movespeed;//移速
    public float atk;//伤害
    public string enemy_type;
    public int enemy_def;
    public int enemy_maxboold;
}
public class Map
{
    public int Id;
    public string Map_Name;//地图名称
    public string Map_Icon;//图标
    public int Enemy_wave;//怪物波次
    public float Enemy_wave_Time;//波次间隔
    public int Player_EXP;//掉落经验值
    public int Player_Next_EXP;//每波次增加的经验值
    public int Map_Start_term;//关卡开启条件
    public string Map_Tips;//关卡描述
    public int Map_Victory;//是否通关
}
public class Boss:Role
{
    
}