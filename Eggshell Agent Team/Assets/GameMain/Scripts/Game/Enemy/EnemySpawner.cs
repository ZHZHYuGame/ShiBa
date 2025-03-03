using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs; // 敌人预制体
    [SerializeField] float spawnRadius = 10f;   // 生成半径
    [SerializeField] int maxEnemies = 200;      // 最大敌人数
    [SerializeField] float waveInterval = 30f;  // 波次间隔

    private int currentWave = 0;
    private List<GameObject> enemyPool = new List<GameObject>();

    void Start()
    {
        InitializePool(); // 初始化对象池
        StartCoroutine(SpawnWave());
    }

    // 对象池预生成
    void InitializePool()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    // 波次生成逻辑
    IEnumerator SpawnWave()
    {
        while (true)
        {
            currentWave++;
            int enemiesToSpawn = Mathf.Min(10 + currentWave * 5, maxEnemies);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (enemyPool.Count > 0)
                {
                    GameObject enemy = GetPooledEnemy();
                    Vector2 spawnPos = (Random.insideUnitCircle.normalized * spawnRadius) + (Vector2)transform.position;
                    enemy.transform.position = spawnPos;
                    enemy.SetActive(true);
                }
                yield return new WaitForSeconds(0.1f); // 避免瞬时生成卡顿
            }
            yield return new WaitForSeconds(waveInterval);//刷新间隔
        }
    }

    GameObject GetPooledEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy) return enemy;
        }
        return null;
    }
}
