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
    public Role playerData;
    public List<Exp> exps;
    
    public DataAnalysis()
    {
        roleDic = ConfigMgr.GetTable<Dictionary<int, Role>>("Enemy");
        refreshWavesDic = ConfigMgr.GetTable<Dictionary<int, RefreshWaves>>("EnemyWavesTab");
        mapDic = ConfigMgr.GetTable<Dictionary<int, Map>>("Map");
        playerData = ConfigMgr.GetListData<Role>("Role",0);
        exps = ConfigMgr.GetTable<List<Exp>>("ExpData");

    }
}