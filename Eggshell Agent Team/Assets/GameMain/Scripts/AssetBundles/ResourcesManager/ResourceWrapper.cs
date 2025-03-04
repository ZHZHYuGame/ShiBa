using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceWrapper<T> where T:UnityEngine.Object
{
    public T Asset;//资源
    public int RefCount;//引用计数

    public ResourceWrapper(T asset)
    {
        Asset = asset;
        RefCount = 1;
    }
    /// <summary>
    /// 增加引用计数
    /// </summary>
    public void AddRef()
    {
        RefCount++;
    }
    /// <summary>
    /// 减少引用计数
    /// </summary>
    public void RemoveRef()
    {
        RefCount--;
        if (RefCount <= 0)
        {
            // 引用计数为 0，释放资源
            Resources.UnloadAsset(Asset);
            Asset = null;
        }
    }
}
