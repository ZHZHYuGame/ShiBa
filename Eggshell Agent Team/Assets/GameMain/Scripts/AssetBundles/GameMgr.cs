using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private ResourceManager _resourcesManager;
    private AssetBundleManager _assetBundleManager;
    private void Start()
    {
        _resourcesManager = new ResourceManager();
        _assetBundleManager = new AssetBundleManager();
         string bundlePath = Path.Combine(Application.streamingAssetsPath, "myprefab");
        //string bundlePath = "D:/ShiBa/Eggshell Agent Team/Assets/StreamingAssets/mybundle";
        string assetName = "Cube";
        // 同步加载资源
        GameObject prefab = _resourcesManager.LoadResource<GameObject>(bundlePath, assetName);
        if (prefab != null)
        {
            Instantiate(prefab);
        }

        // 异步加载资源
        //StartCoroutine(LoadAssetAsync(bundlePath,assetName));
    }

    IEnumerator LoadAssetAsync(string bundlePath,string assetName)
    {
        // 异步加载 AB包
        yield return _assetBundleManager.LoadAssetBundleAsync(bundlePath, success =>
        {
            if (success)
            {
                // 从 AB包加载资源
                GameObject asset = _assetBundleManager.LoadAssetFromBundle<GameObject>(bundlePath, assetName);
                if (asset != null)
                {
                    Instantiate(asset);
                }
            }
        });
    }

    void OnDestroy()
    {
        // 释放资源
        _resourcesManager.UnloadResource("AssetBundles/mybundle", "Cube");
        _assetBundleManager.UnloadAssetBundle(Application.streamingAssetsPath + "/mybundle", true);
        _resourcesManager.UnloadUnusedResources();
    }
}
