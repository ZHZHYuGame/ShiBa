
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //缓存已加载的AssetBundle
     public  Dictionary<string,AssetBundle> _loadedBundles = new Dictionary<string,AssetBundle>();
     private readonly Dictionary<string, ResourceWrapper<UnityEngine.Object>> _resourceCache;//资源缓存
     private readonly LRUCache<string, ResourceWrapper<UnityEngine.Object>> _lruCache;//lru缓存

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cacheCapacity">缓存容量</param>
    public ResourceManager(int cacheCapacity=10)
    {
        _lruCache = new LRUCache<string, ResourceWrapper<UnityEngine.Object>>(cacheCapacity);
        _resourceCache = new Dictionary<string, ResourceWrapper<UnityEngine.Object>>();
    }
    /// <summary>
    /// 按需加载
    /// </summary>
    /// <param name="bundlePath"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public IEnumerator LoadAssetBundleAsync(string bundlePath, System.Action<AssetBundle> onComplete)
    {
        if (_loadedBundles.TryGetValue(bundlePath, out var assetBundle))
        {
            onComplete?.Invoke(assetBundle);
            yield break;
        }
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return bundleLoadRequest;
        if (bundleLoadRequest.assetBundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundlePath}");
            onComplete?.Invoke(null);
        }
        else
        {
            _loadedBundles[bundlePath] = bundleLoadRequest.assetBundle;
            onComplete?.Invoke(bundleLoadRequest.assetBundle);
        }
    }
    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundlePath"></param>
    /// <param name="assetName"></param>
    /// <param name="onComplete"></param>
    /// <param name="loadingProgress"></param>
    /// <returns></returns>
    public IEnumerator LoadAssetAsyncWithProgress<T>(
     AssetBundle assetBundle,// 直接传入已加载的 AssetBundle
       string bundlePath,
     string assetName,
     System.Action<T> onComplete,
     LoadingProgress loadingProgress,
     float initialProgress = 0f, // 全局进度起始值
     float progressRange = 1f // 当前资源加载的进度范围
 ) where T : UnityEngine.Object
    {
        string cacheKey = $"{assetBundle.name}/{assetName}";

        // 检查缓存中是否已存在资源
        if (_resourceCache.TryGetValue(cacheKey, out var resourceWrapper))
        {
            resourceWrapper.AddRef(); // 增加引用计数
            _lruCache.Add(cacheKey, resourceWrapper); // 更新 LRU 缓存
            onComplete?.Invoke(resourceWrapper.Asset as T);
            yield break;
        }
        // 按需加载 AssetBundle
        AssetBundle ab = null;
        yield return LoadAssetBundleAsync(bundlePath, (bundle) => ab = bundle);

        if (assetBundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundlePath}");
            onComplete?.Invoke(null);
            yield break;
        }
        // 异步加载资源
        var assetLoadRequest = assetBundle.LoadAssetAsync<T>(assetName);
        while (!assetLoadRequest.isDone)
        {
            float globalProgress = initialProgress + assetLoadRequest.progress * progressRange; // 映射到全局进度
            loadingProgress.UpdateProgress(globalProgress);
            yield return new WaitForEndOfFrame();
        }

        if (assetLoadRequest.asset == null)
        {
            Debug.LogError($"Failed to load asset: {assetName} from bundle: {assetBundle.name}");
            onComplete?.Invoke(null);
        }
        else
        {
            Debug.Log($"Asset loaded: {assetName}");
            loadingProgress.UpdateProgress(initialProgress + progressRange); // 更新到全局进度终点
            var wrapper = new ResourceWrapper<UnityEngine.Object>(assetLoadRequest.asset);
            _resourceCache[cacheKey] = wrapper; // 添加到缓存
            _lruCache.Add(cacheKey, wrapper); // 添加到 LRU 缓存
            onComplete?.Invoke(assetLoadRequest.asset as T);
        }
    }
    /// 加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>0
    public T LoadResource<T>(string bundlePath,string assetName) where T : UnityEngine.Object
    {
        //"D:/ShiBa/Eggshell Agent Team/Assets/StreamingAssets/mybundle/MyPrefab"
        string cacheKey = $"{bundlePath}/{assetName}";
        // 检查缓存中是否已存在资源
        if (_resourceCache.TryGetValue(cacheKey, out var resourcesWrapper))
        {
            //增加引用计数
            resourcesWrapper.AddRef();
            return resourcesWrapper.Asset as T;
        }
        // 如果缓存未命中，从 ResourceLoader 加载资源
        T resources = ResourcesLoader.LoadResources<T>(bundlePath, assetName);
        if (resources != null)
        {
            var wrapper = new ResourceWrapper<UnityEngine.Object>(resources);
            //添加到缓存
            _resourceCache[cacheKey] = wrapper;
            _lruCache.Add(cacheKey,wrapper);
        }

        return resources;
    }
    // 卸载 AssetBundle
    public void UnloadAssetBundle(string bundlePath, bool unloadAllLoadedObjects = false)
    {
        if (_loadedBundles.TryGetValue(bundlePath, out var assetBundle))
        {
            assetBundle.Unload(unloadAllLoadedObjects);
            _loadedBundles.Remove(bundlePath);
            Debug.Log($"Unloaded AssetBundle: {bundlePath}");
        }
        else
        {
            Debug.LogWarning($"AssetBundle not found: {bundlePath}");
        }
    }
    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="bundlePath"></param>
    /// <param name="assetName"></param>
    public void ReleaseResource(string bundlePath, string assetName)
    {
        string cacheKey = $"{bundlePath}/{assetName}";

        if (_resourceCache.TryGetValue(cacheKey, out var resourceWrapper))
        {
            resourceWrapper.RemoveRef(); // 减少引用计数
            if (resourceWrapper.RefCount <= 0)
            {
                _resourceCache.Remove(cacheKey); // 从缓存中移除
                _lruCache.Remove(cacheKey); // 从 LRU 缓存中移除
            }
        }
    }
}
