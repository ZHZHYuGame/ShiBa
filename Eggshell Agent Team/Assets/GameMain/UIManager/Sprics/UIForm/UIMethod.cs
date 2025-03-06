using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMethod :Singleton<UIMethod>
{
    //单例文件
    //private static UIMethod instance;
    //public static UIMethod GetInstance()
    //{
    //    if (instance==null)
    //    {
    //        instance = new UIMethod();
    //    }
    //    return instance;
    //}

    /// <summary>
    /// 获取场景中的Canvas
    /// </summary>
    /// <returns></returns>
    public GameObject FinCanvas()
    {
        GameObject gameObject = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameObject==null)
        {
            Debug.LogError("没有在场景中找到Canvas");
            return gameObject;
        }
        return gameObject;
    }

    public GameObject FinCanvas(LayerType layerType)
    {
        GameObject gameObject;
        switch (layerType)
        {
            case LayerType.Top:
                gameObject = GameObject.Find("TopLayer").gameObject;
                
                return gameObject;
                
            case LayerType.Upper:
                gameObject = GameObject.Find("UpperLayer").gameObject;
                return gameObject;
               
            case LayerType.Normal:
                gameObject = GameObject.Find("NormalLayer").gameObject;
                return gameObject;
               
            case LayerType.Hud:
                gameObject = GameObject.Find("HudLayer").gameObject;
                return gameObject;
                
            default:
                gameObject = null;
                Debug.LogError("没有在场景中找到Canvas");
                return gameObject;
                
        }
       
    }

    public GameObject FingObjectInChild(GameObject panel,string child_name)
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();
        foreach (var tra in transforms)
        {
            if (tra.gameObject.name==child_name)
            {
                return tra.gameObject;
            }

        }

        Debug.LogError($"{panel.name}物体当中没有找到{child_name}物体");
        return null;
    }

    /// <summary>
    /// 从目标对象中获得对应组件
    /// </summary>
    /// <typeparam name="T">对应组件</typeparam>
    /// <param name="Get_Obj">目标对象</param>
    /// <returns></returns>
    public T AddOrGetComponent<T>(GameObject Get_Obj) where T : Component
    {
        if (Get_Obj.GetComponent<T>() != null)
        {
            return Get_Obj.GetComponent<T>();
        }

        Debug.LogWarning($"无法在{Get_Obj}物体上获得目标组件！");
        return null;
    }

    /// <summary>
    ///从目标Panel的子物体中，根据子物体的名称获得对应组件 
    /// </summary>
    /// <typeparam name="T">对应组件</typeparam>
    /// <param name="panel">目标Panel</param>
    /// <param name="ComponentName">子物体名称</param>
    /// <returns></returns>

    public T GetOrAddSingleComponentInChild<T>(GameObject panel, string ComponentName) where T : Component
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms)
        {
            if (tra.gameObject.name == ComponentName)
            {
                return tra.gameObject.GetComponent<T>();
                break;
            }
        }

        Debug.LogWarning($"没有在{panel.name}中找到{ComponentName}物体！");
        return null;
    }
}
