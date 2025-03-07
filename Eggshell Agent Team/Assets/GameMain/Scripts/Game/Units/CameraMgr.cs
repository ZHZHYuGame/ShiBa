using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMgr : MonoBehaviour
{
    [Header("目标对象")]
    public Transform target; // 需要跟随的目标（如玩家角色）

    [Header("偏移量")]
    public Vector3 offset = new Vector3(0f, 2f, -5f); // 相对于目标的本地坐标偏移

    [Header("平滑参数")]
    [Tooltip("位置平滑时间（越小跟随越快）")]
    public float positionSmoothTime = 0.3f;
    [Tooltip("旋转平滑速度（值越大旋转越快）")]
    public float rotationSmoothSpeed = 5f;

    private Vector3 velocity = Vector3.zero; // 用于SmoothDamp的速度缓存

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("未指定目标对象！");
            return;
        }

        // 计算目标位置：将本地偏移转换为世界坐标
        Vector3 targetPosition = target.TransformPoint(offset);

        // 使用SmoothDamp平滑移动位置
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            positionSmoothTime
        );

        // 计算目标旋转，使相机看向目标
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
    }
    public void Init(Transform transform)
    {
        target = transform;
    }
}
