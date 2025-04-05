using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expand
{
    // 扩展方法：检查边界是否在相机视野内
    public static bool CheckBoundIsInCamera(this Bounds bound, Camera camera)
    {
        // 函数：计算投影位置的编码
        System.Func<Vector4, int> ComputeOutCode = (projectionPos) =>
        {
            int _code = 0;
            if (projectionPos.x < -projectionPos.w) _code |= 1;
            if (projectionPos.x > projectionPos.w) _code |= 2;
            if (projectionPos.y < -projectionPos.w) _code |= 4;
            if (projectionPos.y > projectionPos.w) _code |= 8;
            if (projectionPos.z < -projectionPos.w) _code |= 16;
            if (projectionPos.z > projectionPos.w) _code |= 32;
            return _code;
        };

        Vector4 worldPos = Vector4.one;
        int code = 63; // 初始编码，表示在视野外
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                for (int k = -1; k <= 1; k += 2)
                {
                    // 计算边界顶点的世界坐标
                    worldPos.x = bound.center.x + i * bound.extents.x;
                    worldPos.y = bound.center.y + j * bound.extents.y;
                    worldPos.z = bound.center.z + k * bound.extents.z;

                    // 将世界坐标转换为投影坐标，并计算出编码
                    code &= ComputeOutCode(camera.projectionMatrix * camera.worldToCameraMatrix * worldPos);
                }
            }
        }
        return code == 0 ? true : false; // 如果编码为0，则在视野内；否则在视野外
    }
}
