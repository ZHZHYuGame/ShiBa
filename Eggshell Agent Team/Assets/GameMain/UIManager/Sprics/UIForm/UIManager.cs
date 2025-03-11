using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// 存储UI Panel 的结构
    /// </summary>
    public Stack<BasePanel> stack_ui;
    /// <summary>
    /// 存储 Panel 的名称与物体的名称
    /// </summary>
    public Dictionary<string, GameObject> dic_uiobject;

    /// <summary>
    /// 当前场景对应的Canvas
    /// </summary>
    public GameObject CanvasObj;
    public ResourceManager _resourcesManager;


    public GameObject CanvasTop;
    public GameObject CanvasUpper;
    public GameObject CanvasNormal;
    public GameObject CanvasHud;
    public GameObject CanvasEtc;

    private Dictionary<LayerType, GameObject> canvasDictionary;

    public UIManager()
    {
        _resourcesManager=new ResourceManager();
        stack_ui = new Stack<BasePanel>();
        dic_uiobject = new Dictionary<string, GameObject>();
        canvasDictionary = new Dictionary<LayerType, GameObject>
        {
            { LayerType.Top, CanvasTop },
            { LayerType.Upper, CanvasUpper },
            { LayerType.Normal, CanvasNormal },
            { LayerType.Hud, CanvasHud },
            { LayerType.Etc, CanvasEtc }
        };
    }

    #region 加载Panel
    public GameObject GetSingleObject(UIType uiType)
    {


        if (dic_uiobject.ContainsKey(uiType.Name))
        {
            return dic_uiobject[uiType.Name];
        }

        //if (CanvasObj==null)
        //{
        //    //Debug.LogError("UIManager未能成功获取Canvas");
        //    return UIMethod.Ins.FinCanvas();
        //}
        if (!canvasDictionary.ContainsKey(uiType.UILayerType))
        {
            Debug.LogError($"未知的 LayerType: {uiType.UILayerType}");
            return null;
        }


        GameObject targetCanvas = canvasDictionary[uiType.UILayerType];

        if (targetCanvas == null)
        {
            // 如果 Canvas 不存在，尝试获取或创建
            targetCanvas = UIMethod.Ins.FinCanvas(uiType.UILayerType);
            if (targetCanvas == null)
            {
                Debug.LogError($"无法获取或创建 Canvas: {uiType.UILayerType}");
                return null;
            }
            // 更新字典中的 Canvas 引用
            canvasDictionary[uiType.UILayerType] = targetCanvas;
        }
        //Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameMain/GameResources/Prefabs/HpBase.prefab"), canvas.transform);
        //GameObject prefab = Resources.Load<GameObject>(uiType.Path);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameMain/GameResources/" + uiType.Path+ ".prefab");
        if (prefab == null)
        {
            Debug.LogError($"无法加载预制体: {uiType.Path}");
            return null;
        }

        // 实例化到目标 Canvas 下

        //同步加载资源         
        //勿删!!!勿删!!!勿删!!!   打包到AB包即可加载UI面板
        //勿删!!!勿删!!!勿删!!!   打包到AB包即可加载UI面板
        //勿删!!!勿删!!!勿删!!!   打包到AB包即可加载UI面板
        if (prefab != null)
        {
            GameObject gameObject = GameObject.Instantiate(prefab, targetCanvas.transform);
            return gameObject;
        }
        return null;
        //return InstantiatePrefab(targetCanvas.transform, prefab,"myprefab");
   //     return InstantiatePerfab(targetCanvas, prefab);
        #region 废弃代码
        //GameObject gameObject;
        //switch (uiType.UILayerType)
        //{
        //    case LayerType.Top:
        //        if (CanvasTop==null)
        //        {
        //            return UIMethod.Ins.FinCanvas(uiType.UILayerType);
        //        }
        //        gameObject= GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path), CanvasTop.transform);
        //        break;
        //    case LayerType.Upper:
        //        if (CanvasUpper == null)
        //        {
        //            return UIMethod.Ins.FinCanvas(uiType.UILayerType);
        //        }
        //        gameObject = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path), CanvasUpper.transform);
        //        break;

        //    case LayerType.Normal:
        //        if (CanvasNormal == null)
        //        {
        //            return UIMethod.Ins.FinCanvas(uiType.UILayerType);
        //        }
        //        gameObject = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path), CanvasNormal.transform);
        //        break;

        //    case LayerType.Hud:
        //        if (CanvasHud == null)
        //        {
        //            return UIMethod.Ins.FinCanvas(uiType.UILayerType);
        //        }
        //        gameObject = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path), CanvasHud.transform);
        //        break;

        //    default:
        //        gameObject = null;
        //        break;
        //}

        ////GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path), CanvasObj.transform);

        //return gameObject;
        #endregion
    }

    //public GameObject InstantiatePrefab(Transform targetCanvas, GameObject prefab, string prefabName)
    //{
    //    GameObject prefabs = _resourcesManager.LoadResource<GameObject>(GetPath(prefabName), prefab.name, prefabName);
    //    if (prefabs != null)
    //    {
    //        GameObject gameObject = GameObject.Instantiate(prefab, targetCanvas);
    //        return gameObject;
    //    }
    //    return null;
    //}
    #endregion

    public string GetPath(string path)
    {
        return Path.Combine(Application.streamingAssetsPath, path);
    }
    /// <summary>
    /// 往stack里面压一个Panel；
    /// </summary>
    /// <param name="basePanel"></param>
    public void Push(BasePanel basePanel)
    {
        Debug.Log($"{basePanel.uiType.Name}被Push进stack");

        if (stack_ui.Count>0)
        {
            stack_ui.Peek().OnDistroy();
        }

        GameObject ui_object = GetSingleObject(basePanel.uiType);
        dic_uiobject.Add(basePanel.uiType.Name, ui_object);
        basePanel.ActiveObj = ui_object;

        if (stack_ui.Count==0)
        {
            //入栈
            stack_ui.Push(basePanel);
        }
        else
        {
            if (stack_ui.Peek().uiType.Name!=basePanel.uiType.Name)
            {
                stack_ui.Push(basePanel);
            }
        }

        basePanel.OnStart();
    }

    /// <summary>
    /// 出栈
    /// </summary>
    /// <param name="isload"> isload 为真时 Pop全部 ，isload为假时 Pop栈顶</param>
    public void Pop(bool isload)
    {
        if (isload==true)
        {
            if (stack_ui.Count>0)
            {
                stack_ui.Peek().OnDistroy();
                stack_ui.Peek().OnDestroy();
                GameObject.Destroy(dic_uiobject[stack_ui.Peek().uiType.Name]);
                stack_ui.Pop();
                Pop(true);
            }
        }
        else
        {
            if (stack_ui.Count>0)
            {
                stack_ui.Peek().OnDistroy();
                stack_ui.Peek().OnDestroy();
                GameObject.Destroy(dic_uiobject[stack_ui.Peek().uiType.Name]);
                dic_uiobject.Remove(stack_ui.Peek().uiType.Name);

                stack_ui.Pop();
                if (stack_ui.Count>0)
                {
                    stack_ui.Peek().OnEndable();
                }
            }
        }
    }
}
