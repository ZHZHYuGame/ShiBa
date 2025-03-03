using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class Player : Role
{
    // ========== 基础属性 ==========
    private Skill[] activeSkills = new ActiveSkill[6]; // 当前装备技能
    private Skill[] passiveSkills = new PassiveSkill[6]; // 当前被动技能

    public Player()
    {
    }

    public Player(Skill[] activeSkills, Skill[] passiveSkills)
    {
        this.ActiveSkills = activeSkills;
        this.PassiveSkills = passiveSkills;
    }

    public Skill[] ActiveSkills { get => activeSkills; set => activeSkills = value; }
    public Skill[] PassiveSkills { get => passiveSkills; set => passiveSkills = value; }
}

