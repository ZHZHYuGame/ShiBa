using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 被动技能
/// </summary>
public class PassiveSkill : Skill
{
    private int type;//类型
    private int level;//等级
    private float bulletSpeed;//子弹速度
    private float moveSpeed;//移动速度
    private float blood;//血量上限
    private float exp;//经验
    private float atk;//伤害提升
    private float bloodReturning;//血量回复

    public PassiveSkill()
    {
    }

    public PassiveSkill(int type, int level, float bulletSpeed, float moveSpeed, float blood, float exp, float atk, float bloodReturning)
    {
        this.Type = type;
        this.Level = level;
        this.BulletSpeed = bulletSpeed;
        this.MoveSpeed = moveSpeed;
        this.Blood = blood;
        this.Exp = exp;
        this.Atk = atk;
        this.BloodReturning = bloodReturning;
    }

    public int Type { get => type; set => type = value; }
    public int Level { get => level; set => level = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Blood { get => blood; set => blood = value; }
    public float Exp { get => exp; set => exp = value; }
    public float Atk { get => atk; set => atk = value; }
    public float BloodReturning { get => bloodReturning; set => bloodReturning = value; }
}
