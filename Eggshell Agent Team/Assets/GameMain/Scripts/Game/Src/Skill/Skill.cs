using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能基类
/// </summary>
public class Skill : MonoBehaviour
{
    private int skill_id;//技能id
    private string skill_name;//技能名称
    private string skill_des;//描述
    private string slill_icon;//图标

    public Skill()
    {
    }

    public Skill(int skill_id, string skill_name, string skill_des, string slill_icon)
    {
        this.Skill_id = skill_id;
        this.Skill_name = skill_name;
        this.Skill_des = skill_des;
        this.Slill_icon = slill_icon;
    }

    public int Skill_id { get => skill_id; set => skill_id = value; }
    public string Skill_name { get => skill_name; set => skill_name = value; }
    public string Skill_des { get => skill_des; set => skill_des = value; }
    public string Slill_icon { get => slill_icon; set => slill_icon = value; }
}
