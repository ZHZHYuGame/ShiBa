using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人预制体
    public int enemyCount = 100; // 敌人数量
    public Transform player; // 玩家位置
    public int updatesPerFrame = 10; // 每帧更新的敌人数量
    public float updateRange = 50f; // 更新范围，只更新距离玩家较近的敌人

    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        // 生成敌人
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyObj = Instantiate(enemyPrefab, Random.insideUnitCircle * 50, Quaternion.identity);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.player = player;
            enemies.Add(enemy);
        }

        // 启动协程
        StartCoroutine(UpdateEnemies());
    }

    IEnumerator UpdateEnemies()
    {
        int index = 0;

        while (true)
        {
            // 每帧更新一定数量的敌人
            for (int i = 0; i < updatesPerFrame; i++)
            {
                if (index >= enemies.Count)
                {
                    index = 0; // 重置索引
                }

                // 只更新距离玩家较近的敌人
                if (Vector3.Distance(enemies[index].transform.position, player.position) < updateRange)
                {
                    enemies[index].MoveTowardsPlayer();
                }

                index++;
            }

            // 等待下一帧
            yield return null;
        }
    }
    private void Update()
    {
        // 显示性能数据
        Debug.Log("FPS: " + (1f / Time.deltaTime));
        Debug.Log("Enemy Count: " + enemyCount);
    }
}