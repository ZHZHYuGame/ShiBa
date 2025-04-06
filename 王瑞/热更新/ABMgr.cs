using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AB包数据
public class ABData
{
    //AB包
    public AssetBundle AB { get; set; }
    //使用次数
    public int RefCount { get; set; }

    public ABData(AssetBundle aB, int refCount)
    {
        AB = aB;
        RefCount = refCount;
    }
    //卸载AB
    public void UnLoad()
    {
        if (AB != null)
        {
            AB.Unload(true);
        }
    }
}

/// <summary>
/// AB管理
/// </summary>
public class ABMgr
{
    //存储所有加载的AB包     路径    ab包
    public static Dictionary<string, ABData> dic = new Dictionary<string, ABData>();

    //是否是p路径
    public static bool isPath = false;
    //获取加载AB包的路径
    public static string GetPath
    {
        get
        {
            string path = string.Empty;
            if (isPath)
            {
                //                 P目录可以进行读写操作
                path = Application.persistentDataPath + "/PC/";
            }
            else
            {
                //                 S目录只有只读权限
                path = Application.streamingAssetsPath + "/PC/";
            }
            return path;
        }
    }

    //资源清单
    public static AssetBundleManifest abmi;
    /// <summary>
    /// 初始化加载资源清单
    /// </summary>
    /// <param name="abmiABName">StreamingAssets下的PC目录</param>
    public static void Init(string abmiABName)
    {
        //如果不包含资源清单
        if (!dic.ContainsKey(abmiABName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(GetPath + abmiABName);
            //加载依赖资源清单
            abmi = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            ABData aBData = new ABData(ab, 1);
            dic.Add(abmiABName, aBData);
        }
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="abName">ab包的名称</param>
    /// <param name="assetName">从加载ab包中需要夹杂的资源的名称</param>
    /// <returns></returns>
    public static T Load<T>(string abName, string assetName) where T : Object
    {
        #region 处理依赖
        //获取abName这个ab包的所有依赖的包
        string[] strs = abmi.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //不包含就加载
            if (!dic.ContainsKey(strs[i]))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(GetPath + strs[i]);
                ABData aBData = new ABData(ab, 1);
                dic.Add(strs[i], aBData);
            }
            else
            {
                //包含依赖的ab包,引用次数+1
                dic[strs[i]].RefCount++;
            }
        }
        #endregion
        //处理没有依赖的AB包
        if (!dic.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(GetPath + abName);
            ABData aBData = new ABData(ab, 1);
            dic.Add(abName, aBData);
        }

        //加载具体的资源
        return dic[abName].AB.LoadAsset<T>(assetName);
    }

    /// <summary>
    /// 卸载AB包
    /// </summary>
    /// <param name="abName">需要卸载的ab包</param>
    public static void UnLoad(string abName)
    {
        #region 处理依赖
        //获取abName这个ab包的所有依赖的包
        string[] strs = abmi.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //包含就加载
            if (dic.ContainsKey(strs[i]))
            {
                //包含依赖的ab包,引用次数-1
                dic[strs[i]].RefCount--;
                //该包没有依赖在使用
                if (dic[strs[i]].RefCount <= 0)
                {
                    dic[strs[i]].UnLoad();
                    dic.Remove(strs[i]);
                }
            }

        }
        #endregion
        //处理没有依赖的AB包
        //包含就加载
        if (dic.ContainsKey(abName))
        {
            //包含依赖的ab包,引用次数-1
            dic[abName].RefCount--;
            //该包没有依赖在使用
            if (dic[abName].RefCount <= 0)
            {
                dic[abName].UnLoad();
                dic.Remove(abName);
            }
        }
    }

}
