using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public LoadingProgress loadingProgress; // 进度条脚本
    private ResourceManager _resourcesManager;
    private AssetBundleManager _assetBundleManager;
    private float progress = 0f;
    private string UI_AB_URL = "";
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
         StartCoroutine(LoadAssetAsync());
        //同步加载资源
        //GameObject prefab = _resourcesManager.LoadResource<GameObject>(bundlePath, assetName);
        //if (prefab != null)
        //{
        //    Instantiate(prefab);
        //}

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

    string str = "";
    public string ABGetPath(string path)
    {
        return Path.Combine(Application.streamingAssetsPath, path);
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
        _resourcesManager._loadedBundles.Add(UI_AB_URL, ui);
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
                OnAssetLoaded,
                loadingProgress,
                initialProgress,
                progressRange
            );

            loadedAssets++;
        }


        ui.Unload(false);

    }
    private void OnAssetLoaded(object asset)
    {
        if (asset != null)
        {

        }
    }
    void OnDestroy()
    {
        // 释放资源
        string bundlePath = Path.Combine(Application.streamingAssetsPath, "mybundle");
        _resourcesManager.ReleaseResource(bundlePath, "MyPrefab");
    }
}
