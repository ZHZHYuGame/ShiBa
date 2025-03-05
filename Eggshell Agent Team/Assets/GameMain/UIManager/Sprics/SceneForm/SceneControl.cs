using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl:Singleton<SceneControl>
{
    public Dictionary<string, SceneBase> dic_scene;

    public SceneControl()
    {
        dic_scene = new Dictionary<string, SceneBase>();
    }
    /// <summary>
    /// 加载一个场景
    /// </summary>
    /// <param name="scene_name">目标场景的名称</param>
    /// <param name="sceneBase">目标场景的base</param>
    public void LoadScene(string scene_name,SceneBase sceneBase)
    {
        if (!dic_scene.ContainsKey(scene_name))
        {
            dic_scene.Add(scene_name, sceneBase);
        }
        if (dic_scene.ContainsKey(SceneManager.GetActiveScene().name))
        {
            dic_scene[SceneManager.GetActiveScene().name].ExitScene();
        }
        else
        {
            Debug.LogError($"SceneControl的字典中不包含{SceneManager.GetActiveScene().name}!");

        }
        #region Pop()
        GameMgr.GetInstance().UIManager_Root.Pop(true);
        #endregion
        SceneManager.LoadScene(scene_name);
        sceneBase.EnterScene();
    }
}
