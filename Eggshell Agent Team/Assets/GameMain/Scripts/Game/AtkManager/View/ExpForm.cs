using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpForm : BasePanel
{
    private static string name = "ExpForm";
    private static string path = "Panel/ExpForm";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    public Scrollbar scrollbar;
    public Text levelText;
    Exp exp;
    int allExp;//所有经验值
    int level = 1;//当前等级
    int nowLevelMaxExp;//当前等级最大经验值
    int nowExp ;//当前经验值
    int sprcialExpValue;//差值
    public ExpForm() : base(uIType)
    {
    }
    public override void OnStart()
    {
        base.OnStart();
        //消息侦听
        MsgManager<Exp>.Ins.OnAddListener(MesID.Exp, ChangeExpData);
        scrollbar = UIMethod.Ins.GetOrAddSingleComponentInChild<Scrollbar>(ActiveObj, "ExpScrollbar");
        levelText = UIMethod.Ins.GetOrAddSingleComponentInChild<Text>(ActiveObj, "Level");
        levelText.text = level.ToString();


    }

    private void ChangeExpData(Exp exp)
    {
        this.exp = exp;
        //计算模块
        //升级所需经验 = 50 × 等级^{1.6}  
        //生命上限 = 基础值 × (1 + 0.15) ^{ 等级}
        //攻击力 = 基础值 × (1 + 0.1 × 等级)
        nowExp += exp.Exp_value;
        allExp += exp.Exp_value;
        nowLevelMaxExp = (int)Mathf.Pow((50 * level), 1.6f);
        Debug.Log(nowLevelMaxExp + "当前总经验值");
        
        if (nowExp >= nowLevelMaxExp)
        {
            //游戏暂停
            Time.timeScale = 0;
            level++;
            levelText.text = level.ToString();
            Debug.Log(level);
            sprcialExpValue = nowExp - nowLevelMaxExp;//差值
            //清空经验
            nowExp = 0;
            //计算剩余经验值
            nowExp = sprcialExpValue;
        }
        float v = (1f / nowLevelMaxExp) * nowExp;
        scrollbar.size = v;
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
