using ConfigTools;
using GameFramework.Resource;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public Image icon,selectBg;
    public Text levelText,nowLevel,nextLevel;
    public Skill skill;
    public RoleSkillInfoItem skillInfo;
    public ParticleSystem particleSystem;
    public GameObject particelGameobject;
    public SCSkillListInfoAck infoAck;
    
    internal void InitData(Skill skill, RoleSkillInfoItem roleSkillInfoItem, SCSkillListInfoAck sCSkillListInfoAck)
    {
        this.skill = skill;
        this.skillInfo = roleSkillInfoItem;
        this.infoAck = sCSkillListInfoAck;
        levelText.text = "Lv." + roleSkillInfoItem.level+ "<color=green>"+"("+ "+"+sCSkillListInfoAck.active_skill_add+")"+"</color>";
        //Item/SKill/Skill_320300
        var bytePath = AssetUtility.GetSpriteAsset("Item/SKill/Skill_" + skill.skill_icon);

        var loadCallBack = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
        {
            icon.sprite = asset as Sprite;
        });
        GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallBack);
    }
}
