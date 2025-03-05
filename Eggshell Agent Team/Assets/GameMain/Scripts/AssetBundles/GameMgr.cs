using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private ResourceManager _resourcesManager;
    private AssetBundleManager _assetBundleManager;

    private UIManager UIManager; //UI管理
    public UIManager UIManager_Root { get => UIManager; }

    private SceneControl SceneControl;//场景管理
    public SceneControl SceneControl_Root { get => SceneControl; }


    #region 单例

    /// <summary>
    /// 单例
    /// </summary>
    /// 
    private static GameMgr instance;
    public static GameMgr GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("GameMgr获得实例失败");
            return instance;
        }
        return instance;
    }
    #endregion 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        instance = this;
        UIManager = new UIManager();
        SceneControl = new SceneControl();
    }
    private void Start()
    {


        DontDestroyOnLoad(this.gameObject);
        _resourcesManager = new ResourceManager();
        _assetBundleManager = new AssetBundleManager();
         string bundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles/myprefab");
        //string bundlePath = "D:/ShiBa/Eggshell Agent Team/Assets/StreamingAssets/mybundle";
        string assetName = "Cube";
        // 同步加载资源
        GameObject prefab = _resourcesManager.LoadResource<GameObject>(bundlePath, assetName);
        if (prefab != null)
        {
            Instantiate(prefab);
        }

        //加载第一个场景
        Game game = new Game();
        SceneControl_Root.dic_scene.Add("11", game);

        #region 推入第一个面板
        //UIManager_Root.Push(new StartPanel());
        UIManager_Root.Push(new StrartForm());

        #endregion
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
