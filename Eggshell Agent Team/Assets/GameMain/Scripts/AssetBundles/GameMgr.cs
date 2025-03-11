using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private UIManager UIManager; //UI管理
    public UIManager UIManager_Root { get => UIManager; }

    private SceneControl SceneControl;//场景管理
    public SceneControl SceneControl_Root { get => SceneControl; }
    public DataAnalysis dataAnalysis;

    public ModelMgr ModelMgr;//面板数据管理类


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
        ModelMgr = new ModelMgr();
        ModelMgr.LoadAll();//加载所有Model
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //加载第一个场景
        Game game = new Game();
        SceneControl_Root.dic_scene.Add("11", game);
        dataAnalysis = new DataAnalysis();
       // AudioMgr.Instance.PlayMusic();
        #region 推入第一个面板
        //UIManager_Root.Push(new StartPanel());
        UIManager_Root.Push(new StrartForm());
        #endregion
    }
}
