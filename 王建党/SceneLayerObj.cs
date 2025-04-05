using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLayerObj : MonoBehaviour
{
    public List<GameObject> objList = new List<GameObject>();
    public string id;
    public string path;
    public string position;
    public string width;
    public string type;

    /// <summary>
    /// 加载地图块区域
    /// </summary>
    public void Loading()
    {
        string[] objStrList = path.Split('_');
        int index = 0;
        foreach (var item in objStrList)
        {
            string[] pList = position.Split('|');
            //协同程序

            GameObject o = Resources.Load<GameObject>($"SceneLayerPerfab/{item}");
            if (o)
            {
                GameObject obj = Instantiate(o);//$"SceneLayerPerfab/{item}"
                if (obj != null)
                {
                    SetPosition(obj, pList[index]);
                    SetWidth(obj);
                    index++;
                    objList.Add(obj);
                }
            }
           
        }
    }

    private void SetPosition(GameObject obj, string position)
    {
        string[] posList = position.Split('_');
        obj.transform.position = new Vector3(int.Parse(posList[0]), int.Parse(posList[1]), int.Parse(posList[2]));
    }

    private void SetWidth(GameObject obj)
    {
        obj.transform.localScale = new Vector3(int.Parse(width), int.Parse(width), int.Parse(width));
    }
    /// <summary>
    /// 显示地图块
    /// </summary>
    public void Display()
    {
        foreach (var obj in objList)
        {
            obj.gameObject.SetActive(true);
        }
        if (objList.Count == 0)
        {
            Loading();
        }
    }
    /// <summary>
    /// 卸载地图块
    /// </summary>
    public void Unload()
    {
        Debug.Log($"1 移除地图的物体");
        foreach (var obj in objList)
        {
            Debug.Log($"2 移除地图的物体 = {obj}");
            obj.gameObject.SetActive(false);
        }
    }
}
