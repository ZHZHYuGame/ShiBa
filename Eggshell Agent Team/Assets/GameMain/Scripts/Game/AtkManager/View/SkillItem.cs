using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public Transform content;
    public Text names,title;
    public Image icon;
    List<GameObject> skillItems;

    private void Awake()
    {
        skillItems = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject skillItem = GameObject.Instantiate(ResourcesLoader.LoadResources<GameObject>(Application.streamingAssetsPath + "/myprefab", "StarItem", "myprefab"), content);
            skillItems.Add(skillItem);
        }
        
    }
    internal void Init(Skill skill)
    {
        names.text = skill.Skill_name;
        title.text = skill.Skill_des;
        string assetName = Path.GetFileNameWithoutExtension(skill.Slill_icon);
        icon.sprite =ResourcesLoader.LoadResources<Sprite>(Application.streamingAssetsPath + "/weapon", assetName, "weapon");
        //更新装备等级状态
        for (int i = 0;i < skill.Level+1;i++)
        {
            if (i >= 5) break;
            skillItems[i].transform.GetChild(0).gameObject.SetActive(true);
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
