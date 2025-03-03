using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    // ========== 基础属性 ==========
    private int id;
    private Animator animator;//动画
    private GameObject prefab;//模型
    private int level;//等级
    private string names;//名称
    private float blood;//血量
    private float atkSpeed;//攻速
    private float moveSpeed;//移速
    private float atk;//伤害

    public Role()
    {
    }

    public Role(int id, Animator animator, GameObject prefab, int level, string names, float blood, float atkSpeed, float moveSpeed, float atk)
    {
        this.Id = id;
        this.Animator = animator;
        this.Prefab = prefab;
        this.Level = level;
        this.Names = names;
        this.Blood = blood;
        this.AtkSpeed = atkSpeed;
        this.MoveSpeed = moveSpeed;
        this.Atk = atk;
    }

    public int Id { get => id; set => id = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }
    public int Level { get => level; set => level = value; }
    public string Names { get => names; set => names = value; }
    public float Blood { get => blood; set => blood = value; }
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Atk { get => atk; set => atk = value; }
}
