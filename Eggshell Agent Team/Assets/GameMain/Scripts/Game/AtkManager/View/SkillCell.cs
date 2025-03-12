using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            string assetName = Path.GetFileNameWithoutExtension(activeSkill.Slill_icon);
            icon.sprite = ResourcesLoader.LoadResources<Sprite>(Application.streamingAssetsPath+"/effect",assetName, "effect");
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
            string assetName = Path.GetFileNameWithoutExtension(activeSkill.Slill_icon);
            icon.sprite = ResourcesLoader.LoadResources<Sprite>(Application.streamingAssetsPath + "/effect", assetName, "effect");
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
