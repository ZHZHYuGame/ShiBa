using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAnimalCellViews : EnhancedScrollerCellView
{
    [SerializeField] private Image icon;
    [SerializeField] private Text names;
    [SerializeField] private GameObject black,locks;
    [SerializeField] private Text level;
    [SerializeField] private Text tiems;
    
    internal void SetData(BossData bossData)
    {
        //设置toggle组
        gameObject.GetComponent<Toggle>().group = transform.parent.parent.GetComponent<ToggleGroup>();
        icon.sprite = Resources.Load<Sprite>("BossView/boss_item_" + bossData.icon);
        names.text = bossData.name;
        if (bossData.status != 0)
        {
            black.gameObject.SetActive(true);
            locks.gameObject.SetActive(true);
            // 将Unix时间戳转换为DateTimeOffset  
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(bossData.time);

            // 获取UTC时间
            DateTime utcTime = dateTimeOffset.UtcDateTime;

            // 转换为本地时间
            DateTime localTime = dateTimeOffset.ToLocalTime().DateTime;
            TimeSpan timeSpan = localTime - DateTime.Now;
            tiems.text = $"{timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        }
        else
        {
            black.gameObject.SetActive(false);
            locks.gameObject.SetActive(false);
            tiems.text = "<color=#00931F>已刷新</color>";
        }
        level.text = $"Lv.{bossData.level}";
    }
    private void Start()
    {
        
    }
}
