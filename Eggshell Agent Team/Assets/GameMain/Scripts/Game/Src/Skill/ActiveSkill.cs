using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//主动技能
public class ActiveSkill :Skill
{
    private int type;//武器类型
    private GameObject prefab;//模型
    private Animator animator;//动画
    private int level;//技能等级
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


    public ActiveSkill(int type, GameObject prefab, Animator animator, int level, float skill_hurt, int num, float rate, float coefficient, float skill_range, float skill_cooling, float skill_size)
    {
        this.Type = type;
        this.Prefab = prefab;
        this.Animator = animator;
        this.Level = level;
        this.Skill_hurt = skill_hurt;
        this.Num = num;
        this.Rate = rate;
        this.Coefficient = coefficient;
        this.Skill_range = skill_range;
        this.Skill_cooling = skill_cooling;
        this.Skill_size = skill_size;
    }

    public int Type { get => type; set => type = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public int Level { get => level; set => level = value; }
    public float Skill_hurt { get => skill_hurt; set => skill_hurt = value; }
    public int Num { get => num; set => num = value; }
    public float Rate { get => rate; set => rate = value; }
    public float Coefficient { get => coefficient; set => coefficient = value; }
    public float Skill_range { get => skill_range; set => skill_range = value; }
    public float Skill_cooling { get => skill_cooling; set => skill_cooling = value; }
    public float Skill_size { get => skill_size; set => skill_size = value; }
}


