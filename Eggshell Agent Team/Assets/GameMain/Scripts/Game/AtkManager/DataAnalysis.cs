using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 配置表管理类
/// </summary>
public class DataAnalysis
{
    public Dictionary<int, Map> mapDic;
    public Dictionary<int, RefreshWaves> refreshWavesDic;
    public Dictionary<int, Role> roleDic;
    public Dictionary<int, ActiveSkill> mainSkillDic;
    public Dictionary<int, PassiveSkill> beSkillDic;
    public Role playerData;
    public List<Exp> exps;
    public List<Skill> allSkill = new List<Skill>();
    
    public DataAnalysis()
    {
        roleDic = ConfigMgr.GetTable<Dictionary<int, Role>>("Enemy");
        refreshWavesDic = ConfigMgr.GetTable<Dictionary<int, RefreshWaves>>("EnemyWavesTab");
        mapDic = ConfigMgr.GetTable<Dictionary<int, Map>>("Map");
        playerData = ConfigMgr.GetListData<Role>("Role", 0);
        exps = ConfigMgr.GetTable<List<Exp>>("ExpData");
        mainSkillDic = ConfigMgr.GetTable<Dictionary<int, ActiveSkill>>("ActiveskillData");
        beSkillDic = ConfigMgr.GetTable<Dictionary<int, PassiveSkill>>("PassiveSkillData");
        GetAllSkill();

    }
    /// <summary>
    /// 获取所有技能
    /// </summary>
    private void GetAllSkill()
    {
        foreach (var item in mainSkillDic)
        {
            allSkill.Add(item.Value);
        }
        foreach (var item in beSkillDic)
        {
            allSkill.Add(item.Value);
        }
    }
}