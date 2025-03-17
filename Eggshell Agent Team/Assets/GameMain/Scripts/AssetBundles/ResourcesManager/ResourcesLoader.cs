using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 资源加载器
/// </summary>
public static class ResourcesLoader
{
    // LRU 缓存实例
    private static readonly LRUCache<string, UnityEngine.Object> _lruCache = 
        new LRUCache<string, UnityEngine.Object>(capacity: 100, timeThresHold: TimeSpan.FromMinutes(10));

    /// <summary>
    /// 同步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T LoadResources<T>(string bundlepath, string assetName, string abName) where T : UnityEngine.Object
    {
        // 检查并移除超时未访问的资源
        CheckAndMoveExpiredNodes();
        if (string.IsNullOrEmpty(bundlepath) || string.IsNullOrEmpty(assetName))
        {
            Debug.LogError("Resource path is null or empty.");
            return null;
        }
        // 生成缓存键（AssetBundle 路径 + 资源名称）
        string cacheKey = $"{bundlepath}/{assetName}";
        // 先从 LRU 缓存中查找资源
        if (_lruCache.TryGetValue(cacheKey, out var cachedAsset))
        {
            return cachedAsset as T;
        }
        if (!File.Exists(bundlepath))
        {
            Debug.LogError($"AssetBundle file not found: {bundlepath}");
            return null;
        }
        if (!ResourceManager._loadedBundles.TryGetValue(bundlepath, out var bundles))
        {
            bundles = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/"+ abName);
            if (bundles == null)
            {
                Debug.LogError($"Failed to load AssetBundle: {bundles}");
                return null;
            }
            ResourceManager._loadedBundles.Add(bundlepath, bundles);
        }
        T asset = bundles.LoadAsset<T>(assetName);
        if (asset == null)
        {
            Debug.LogError($"Failed to load asset: {assetName} from bundle: {bundlepath}");
            bundles.Unload(false); // 卸载 AB包，但不卸载已加载的资源
            return null;
        }
        // 将加载的资源添加到 LRU 缓存
        _lruCache.Add(cacheKey, asset);
        Debug.Log($"Asset loaded: {assetName} from bundle: {bundlepath}");
        return asset;
    }
    /// <summary>
    /// 检查并移除超时未访问的资源
    /// </summary>
    public static void CheckAndMoveExpiredNodes()
    {
        _lruCache.CheckAndMoveExpiredNodes();
    }
    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundlePath"></param>
    /// <param name="assetName"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public static IEnumerator LoadResourcesAsyc<T>(string bundlePath, string assetName, Action<T> onComplete) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(bundlePath) || string.IsNullOrEmpty(assetName))
        {
            Debug.LogError("Resource path is null or empty.");
            onComplete?.Invoke(null);
            yield break;
        }
        var assetbundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return assetbundleRequest;
        if (assetbundleRequest == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundlePath}");
            onComplete?.Invoke(null);
            yield break;
        }
        //异步加载资源
        var assetLoadRequest = assetbundleRequest.assetBundle.LoadAssetAsync(assetName);
        yield return assetbundleRequest;
        if (assetLoadRequest.asset == null)
        {
            Debug.LogError($"Failed to load asset: {assetName} from bundle: {bundlePath}");
            assetbundleRequest.assetBundle.Unload(false); // 卸载 AB包，但不卸载已加载的资源
            onComplete?.Invoke(null);
        }
        else
        {
            Debug.Log($"Asset loaded: {assetName} from bundle: {bundlePath}");
            onComplete?.Invoke(assetLoadRequest.asset as T);
        }
    }

}
