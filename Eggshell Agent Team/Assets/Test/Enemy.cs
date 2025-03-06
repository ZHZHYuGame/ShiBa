using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // 玩家位置
    public float moveSpeed = 5f; // 移动速度
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveTowardsPlayer()
    {
        // 计算移动方向
        Vector3 direction = (player.position - transform.position).normalized;
        // 使用刚体移动
        rb.velocity = direction * moveSpeed;
    }
}