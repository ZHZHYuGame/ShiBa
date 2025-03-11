using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    public Image icon;
    ActiveSkill activeSkill;
    PassiveSkill passiveSkill;
    internal void Init(ActiveSkill activeSkill)
    {
        this.activeSkill = activeSkill;
        if (activeSkill != null)
        {
            icon.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(activeSkill.Slill_icon);
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
        }
        
        
    }

    internal void Init(PassiveSkill passiveSkill)
    {
        this.passiveSkill = passiveSkill;
        if (activeSkill != null)
        {
            icon.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(activeSkill.Slill_icon);
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
