using UnityEngine;
using UnityEngine.AI;

public class PetPanel : MonoBehaviour
{
    public PetData petData;
    public Transform owner; // 玩家角色
    public Transform attackTarget; // 当前攻击目标
    public Slider healthSlider; // 血条UI

    private NavMeshAgent navAgent;
    private float attackTimer;
    private float skillCooldownTimer;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = petData.moveSpeed;
        UpdateHealthUI();
    }

    void Update()
    {
        FollowOwner();
        AutoAttack();
    }

    // 跟随玩家
    void FollowOwner()
    {
        if (Vector3.Distance(transform.position, owner.position) > 3f)
        {
            navAgent.SetDestination(owner.position);
        }
    }

    // 自动索敌攻击
    void AutoAttack()
    {
        if (attackTarget == null)
        {
            FindNearestEnemy();
            return;
        }

        if (Vector3.Distance(transform.position, attackTarget.position) <= 2f)
        {
            navAgent.isStopped = true;
            if (Time.time > attackTimer)
            {
                Attack();
                attackTimer = Time.time + 1f; // 每秒攻击一次
            }
        }
        else
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(attackTarget.position);
        }
    }

    void FindNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Enemy"));
        if (enemies.Length > 0)
        {
            attackTarget = enemies[0].transform;
        }
    }

    void Attack()
    {
        // 普通攻击
        Enemy enemy = attackTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            float damage = petData.attackPower;

            // 技能触发判定
            foreach (Skill skill in petData.skills)
            {
                if (Time.time > skillCooldownTimer && Random.value < skill.triggerProbability)
                {
                    damage *= skill.damageMultiplier;
                    Instantiate(skill.skillEffectPrefab, attackTarget.position, Quaternion.identity);
                    skillCooldownTimer = Time.time + skill.cooldown;
                    break;
                }
            }

            enemy.TakeDamage(damage);
        }
    }

    // 受伤处理
    public void TakeDamage(float damage)
    {
        petData.health -= damage;
        if (petData.health <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        Destroy(gameObject);
        // 触发复活逻辑（如回到玩家身边复活）
    }

    // 经验获取与升级
    public void GainExp(float exp)
    {
        petData.currentExp += exp;
        if (petData.currentExp >= petData.maxExp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        petData.level++;
        petData.attackPower *= 1.2f;
        petData.health *= 1.5f;
        petData.currentExp = 0;
        petData.maxExp *= 1.5f;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = petData.health / 100f; // 假设最大血量为100
        }
    }
}