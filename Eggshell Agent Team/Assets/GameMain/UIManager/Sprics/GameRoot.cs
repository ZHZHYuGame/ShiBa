using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private UIManager UIManager;
    public UIManager UIManager_Root { get => UIManager; }

    private SceneControl SceneControl;
    public SceneControl SceneControl_Root { get => SceneControl; }
    private static GameRoot instance;
    public static GameRoot GetInstance()
    {
        if (instance==null)
        {
            Debug.LogError("GameRoot获得实例失败");
            return instance;
        }
        return instance;
    }
    private void Awake()
    {
        if (instance==null)
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

        //UIManager_Root.CanvasObj = UIMethod.Ins.FinCanvas();//获得场景中的画布

        //加载第一个场景
        Scene1 scene1 = new Scene1();
        SceneControl_Root.dic_scene.Add(scene1.SceneName, scene1);

        #region 推入第一个面板
        UIManager_Root.Push(new StartPanel());
        
        #endregion
    }
}
