using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ExpPrefab : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float pickupRadius = 2f;
    [SerializeField] private float attractionRadius = 5f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float startDelay = 0.5f;
    [SerializeField] private AnimationCurve attractionCurve;

    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rb;
    Exp exp;
    [SerializeField]
    private bool isAttracted;
    [SerializeField]
    private bool canMove;
    private float attractionStartTime;
    private Vector2 initialPosition;
    Transform player;
    internal void Init(Exp exp)
    {
        this.exp = exp;
    }

    private void Awake()
    {

        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        initialPosition = transform.position;
        player = PlayerRole.Instance.transform;
        
    }

    private void Start()
    {
        if (playerTransform == null)
            playerTransform = player;
        
        

        StartCoroutine(EnableMovementAfterDelay());
    }

    private IEnumerator EnableMovementAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canMove = true;
        StartCoroutine(UpdatePlayerDistanceCheck());
    }

    private IEnumerator UpdatePlayerDistanceCheck()
    {
        var wait = new WaitForSeconds(0.1f);
        while (canMove)
        {
            if (!isAttracted)
            {
                float sqrDist = (playerTransform.position - transform.position).sqrMagnitude;
                if (sqrDist <= attractionRadius * attractionRadius)
                {
                    OnAttractionStart();
                }
            }
            yield return wait;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove || !isAttracted) return;

        float t = (Time.time - attractionStartTime) * moveSpeed;
        Vector2 targetPosition = playerTransform.position;

        // 使用动画曲线控制移动速度
        rb.MovePosition(Vector2.Lerp(
            initialPosition,
            targetPosition,
            attractionCurve.Evaluate(t))
        );

        // 直接传送的最后判断
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            CollectExp();
        }
    }

    private void OnAttractionStart()
    {
        isAttracted = true;
        attractionStartTime = Time.time;
        initialPosition = transform.position;
    }

    private void CollectExp()
    {
        //经验条增加经验 消息广播
        MsgManager<Exp>.Ins.OnBroadCast(MesID.Exp, exp);
        //放回对象池
        ObjectPool.Enqueue(gameObject); 


    }

    private void OnEnable()
    {
        isAttracted = false;
        canMove = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        StartCoroutine(EnableMovementAfterDelay());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (pickupRadius > attractionRadius)
            attractionRadius = pickupRadius + 0.1f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
#endif

}
