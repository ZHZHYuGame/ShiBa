using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//武器自动攻击
public class AutoAttackController : MonoBehaviour
{
    [SerializeField] float attackRange = 5f;    // 攻击范围
    [SerializeField] float attackInterval = 0.5f; // 攻击间隔
    [SerializeField] GameObject projectilePrefab; // 子弹/攻击特效

    private Transform nearestEnemy;
    private float timer;

    private void Awake()
    {
        ObjectPool.CreatePool(projectilePrefab, 20);//创建子弹池子
    }
    void Update()
    {
        timer += Time.deltaTime;

        // 自动搜索最近敌人
        nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null && timer >= attackInterval)
        {
            Attack();
            timer = 0;
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemyList = new List<GameObject> ();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyList.Add(enemies[i]);
        }
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i] == null)
            {
                //删除怪物或者放入怪物对象池中
                enemyList.RemoveAt(i);
            }
        }
        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= attackRange)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void Attack()
    {
        // 生成攻击特效/子弹
        if (projectilePrefab && nearestEnemy)
        {
            //对象池生成子弹
            GameObject projectile = ObjectPool.GetObject(projectilePrefab);//生成子弹
            Vector3 pos = nearestEnemy.transform.position;
            pos.z = 0;
            projectile.transform.position = transform.position;

            
        }
    }
}
