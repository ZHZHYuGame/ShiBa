using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>(); // 敌人预制体
    [SerializeField] float spawnRadius = 10f;   // 生成半径
    [SerializeField] int maxEnemies = 200;      // 最大敌人数
    [SerializeField] float waveInterval;  // 波次间隔

    List<RefreshWaves> refreshWavesList;//波次刷新规则
    List<Role> roles = new List<Role>();//怪物集合
    private int currentWave = 0;
    private List<GameObject> enemyPool = new List<GameObject>();
    Dictionary<int, RefreshWaves> refreshWavesDic;
    Map map;

    void Start()
    {
        InitializePool(); // 初始化对象池
        StartCoroutine(SpawnWave());
    }

    // 对象池预生成
    void InitializePool()
    {
       
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            ObjectPool.CreatePool(enemyPrefabs[i], 200,GameObject.Find("EnemyPool").transform);//怪物对象池
        }
    }

    // 波次生成逻辑
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < refreshWavesList.Count; i++)
        {
            int index = i;
            for (int j = 0; j < refreshWavesList[i].Enemy_num; j++)
            {
                //生成怪物
                GameObject enemy = ObjectPool.GetObject(enemyPrefabs[index]);
                Vector2 spawnPos = (Random.insideUnitCircle.normalized * spawnRadius) + (Vector2)transform.position;
                enemy.transform.position = spawnPos;
                enemy.AddComponent<AI_Move>();
                enemy.AddComponent<Monster>().Init(SetMonsterData(index), refreshWavesList[index]);//怪物属性
              
                yield return new WaitForSeconds(0.1f); // 避免瞬时生成卡顿
            }

            yield return new WaitForSeconds(waveInterval);//刷新间隔
        }
    }

    private Role SetMonsterData(int index)
    {
        Role newData = new Role(roles[index]);
        newData.Blood *= refreshWavesList[index].Coefficient;
        newData.Atk *= refreshWavesList[index].Coefficient;
        newData.Atkspeed *= refreshWavesList[index].Coefficient;
        newData.Def *= refreshWavesList[index].Coefficient;
        newData.Maxboold *= refreshWavesList[index].Coefficient;

        return newData;
    }

    GameObject GetPooledEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy) return enemy;
        }
        return null;
    }

    internal void Init(Map map)
    {
        this.map = map;
        GetDatas(map.Id);//数据获取
        SetDatas();

    }

    private void SetDatas()
    {
        for (int i = 0; i < roles.Count; i++)
        {
            enemyPrefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(roles[i].This_object_path));
        }
        waveInterval = map.Enemy_wave_Time;
    }

    private void GetDatas(int v)
    {
        //获取地图波次信息
        refreshWavesDic = ConfigMgr.GetTable<Dictionary<int, RefreshWaves>>("EnemyWavesTab");
        refreshWavesList = new List<RefreshWaves>();
        foreach (var item in refreshWavesDic)
        {
            if (item.Value.Map_id == v)
            {
                refreshWavesList.Add(item.Value);
            }
        }
        //怪物信息
        foreach (var item in refreshWavesList)
        {
            roles.Add(ConfigMgr.GetDicData<Role>("Enemy", item.Enemy_id));
        }
    }
}
