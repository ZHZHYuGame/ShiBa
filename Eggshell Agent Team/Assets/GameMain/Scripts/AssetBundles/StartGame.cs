using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public LoadingProgress loadingProgress; // 进度条脚本
    public ResourceManager _resourcesManager;
    private AssetBundleManager _assetBundleManager;
    private float progress = 0f;
    private string UI_AB_URL = "";
    public static StartGame instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _resourcesManager = new ResourceManager();
        _assetBundleManager = new AssetBundleManager();
        Debug.Log("开始加载AB包资源");
        StartCoroutine(LoadAssetAsync());
        
        // 异步加载资源
        //StartCoroutine(LoadAssetAsync(bundlePath,assetName));

    }

    string str = "";
    public string ABGetPath(string path)
    {
        return Application.streamingAssetsPath + "/" + path;
    }

    IEnumerator LoadAssetAsync()
    {
        //string bundlePath = Path.Combine(Application.streamingAssetsPath, "myprefab");
        ////string assetName = "Cube";
        ///
        Debug.Log(11);
        UI_AB_URL = ABGetPath("ui");
        AssetBundle ui = AssetBundle.LoadFromFile(UI_AB_URL);

        if (ui == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {UI_AB_URL}");
            yield break;
        }
        ResourceManager._loadedBundles.Add(UI_AB_URL, ui);
        string[] ui_name = ui.GetAllAssetNames();
        int totalAssets = ui_name.Length;
        int loadedAssets = 0;

        foreach (var item in ui_name)
        {
            float initialProgress = (float)loadedAssets / totalAssets; // 当前全局进度起始值
            float progressRange = 1f / totalAssets; // 当前资源加载的进度范围
            yield return _resourcesManager.LoadAssetAsyncWithProgress<UnityEngine.Object>(
                ui, // 直接传入已加载的 AssetBundle
                UI_AB_URL,
                item,
                loadingProgress,
                initialProgress,
                progressRange
            );

            loadedAssets++;
        }


        ui.Unload(false);

    }
    private void Update()
    {
        LoadingProgress.Update();
    }
    void OnDestroy()
    {
        // 释放资源
        string bundlePath = Path.Combine(Application.streamingAssetsPath, "mybundle");
        _resourcesManager.ReleaseResource(bundlePath, "MyPrefab");
    }
}
