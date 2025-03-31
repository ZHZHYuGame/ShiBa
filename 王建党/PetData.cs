using UnityEngine;

[System.Serializable]
public class PetData
{
    public string petName = "EggDog";
    public int level = 1;
    public float currentExp;
    public float maxExp = 100;
    public float attackPower = 10;
    public float health = 100;
    public float moveSpeed = 5;
    public Skill[] skills; // 宠物技能数组
}

[System.Serializable]
public class Skill
{
    public string skillName;
    public float damageMultiplier = 1.5f; // 技能伤害倍率
    public float cooldown = 5f;
    public float triggerProbability = 0.3f; // 触发概率（30%）
    public GameObject skillEffectPrefab; // 技能特效预制体
}