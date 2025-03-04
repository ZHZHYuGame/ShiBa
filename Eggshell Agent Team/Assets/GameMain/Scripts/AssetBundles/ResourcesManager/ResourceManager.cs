
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
     private readonly LRUCache<string, Object> _resourcescache;//资源缓存

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cacheCapacity">缓存容量</param>
    public ResourceManager(int cacheCapacity=10)
    {
        _resourcescache = new LRUCache<string, Object>(cacheCapacity);
    }
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadResource<T>(string bundlePath,string assetName) where T : Object
    {
        //"D:/ShiBa/Eggshell Agent Team/Assets/StreamingAssets/mybundle/MyPrefab"
        string cacheKey = $"{bundlePath}/{assetName}";
        if (_resourcescache.TryGetValue(cacheKey, out var cachedResource))
        {
            Debug.Log($"Resource loaded from cache: {cacheKey}");
            return cachedResource as T;
        }
        // 如果缓存未命中，从 ResourceLoader 加载资源
        T resources = ResourcesLoader.LoadResources<T>(bundlePath,assetName);
        if (resources == null)
        {
            _resourcescache.Add(cacheKey, resources);
        }
        return resources;
    }
    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundlePath"></param>
    /// <param name="assetName"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public IEnumerator LoadResourcesAsyc<T>(string bundlePath,string assetName,System.Action<T>onComplete) where T : UnityEngine.Object
    {
        string cacheKey = $"{bundlePath}/{assetName}";
        if (_resourcescache.TryGetValue(cacheKey, out var cachedResource))
        {
            Debug.Log($"Resource loaded from cache: {cacheKey}");
            onComplete.Invoke(cachedResource as T);
            yield break;
        }
        yield return ResourcesLoader.LoadResourcesAsyc<T>(bundlePath, assetName, resource =>
        {
            if (resource != null)
            {
                _resourcescache.Add(cacheKey,resource);
            }
            onComplete.Invoke(resource);
        });
    }
    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="bundlePath"></param>
    /// <param name="assetName"></param>
    public void UnloadResource(string bundlePath,string assetName)
    {
        string cacheKey = $"{bundlePath}/{assetName}";
        if (_resourcescache.TryGetValue(cacheKey, out var cachedResource))
        {
            Resources.UnloadAsset(cachedResource);//释放资源
            _resourcescache.Clear();
            Debug.Log($"Resource unloaded: {cacheKey}");
        }
    }
    /// <summary>
    /// 释放从未使用过的资源
    /// </summary>
    public void UnloadUnusedResources()
    {
        Resources.UnloadUnusedAssets();
        Debug.Log("Unused resources unloaded.");
    }
}
