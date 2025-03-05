using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AB包管理器
/// </summary>
public class AssetBundleManager
{
    private readonly LRUCache<string, AssetBundle> _bunleCache;//AB包缓存
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cacheCapacity">缓存容量</param>
    public AssetBundleManager(int cacheCapacity = 5)
    {
        _bunleCache = new LRUCache<string, AssetBundle>(cacheCapacity);
    }
    /// <summary>
    /// 异步加载AB包
    /// </summary>
    /// <param name="bundlePath"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public IEnumerator LoadAssetBundleAsync(string bundlePath, System.Action<bool> onComplete)
    {
        if (_bunleCache.TryGetValue(bundlePath, out var cachedBundle))
        {
            Debug.Log($"AssetBundle loaded from cache: {bundlePath}");
            onComplete.Invoke(true);
            yield break;
        }
        // 异步加载 AB包
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return bundleLoadRequest;

        if (bundleLoadRequest.assetBundle != null)
        {
            _bunleCache.Add(bundlePath, bundleLoadRequest.assetBundle); // 添加到缓存
            Debug.Log($"AssetBundle loaded: {bundlePath}");
            onComplete?.Invoke(true);
        }
        else
        {
            Debug.LogError($"Failed to load AssetBundle: {bundlePath}");
            onComplete?.Invoke(false);
        }
    }/// <summary>
     /// 从AB包加载资源
     /// </summary>
     /// <typeparam name="T">资源类型</typeparam>
     /// <param name="bundlePath">AB包路径</param>
     /// <param name="assetName">资源名称</param>
     /// <returns>加载的资源</returns>
    public T LoadAssetFromBundle<T>(string bundlePath, string assetName) where T : Object
    {
        if (_bunleCache.TryGetValue(bundlePath, out var bundle))
        {
            T asset = bundle.LoadAsset<T>(assetName);
            if (asset != null)
            {
                Debug.Log($"Asset loaded from bundle: {assetName}");
                return asset;
            }
            else
            {
                Debug.LogError($"Failed to load asset: {assetName} from bundle: {bundlePath}");
            }
        }
        else
        {
            Debug.LogError($"AssetBundle not found: {bundlePath}");
        }

        return null;
    }

    /// <summary>
    /// 卸载AB包
    /// </summary>
    /// <param name="bundlePath">AB包路径</param>
    /// <param name="unloadAllLoadedObjects">是否卸载所有加载的资源</param>
    public void UnloadAssetBundle(string bundlePath, bool unloadAllLoadedObjects)
    {
        if (_bunleCache.TryGetValue(bundlePath, out var bundle))
        {
            bundle.Unload(unloadAllLoadedObjects); // 卸载 AB包
            _bunleCache.Clear(); // 清空缓存
            Debug.Log($"AssetBundle unloaded: {bundlePath}");
        }
    }
}
