using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class GameHotMgr : MonoBehaviour
{
    /// <summary>
    /// 资源服务器的版本文件
    /// </summary>
    string serverIP = "127.0.0.1/2203_Game/Version.txt";
    /// <summary>
    /// 服务器的版本号
    /// </summary>
    Version serverVersion;
    /// <summary>
    /// 服务器的资源清单文件
    /// </summary>
    string serverManifest;
    /// <summary>
    /// 服务器的资源清单缓存
    /// </summary>
    Dictionary<string, AssetItem> serverAssetDict = new Dictionary<string, AssetItem>();
    /// <summary>
    /// 本地的资源清单缓存
    /// </summary>
    Dictionary<string, AssetItem> localAssetDict = new Dictionary<string, AssetItem>();
    /// <summary>
    /// 下载队列
    /// </summary>
    Queue<AssetItem> assetQue = new Queue<AssetItem>();

    // Start is called before the first frame update
    void Start()
    {
        //下载第一个资源
        LoadXCAsset(serverIP, (data) =>
        {
            //线上最新版本号
            string verStr = Encoding.UTF8.GetString(data);
            serverVersion = new Version(verStr);
            //本地设备上的版本号
            string localVerStr = File.Exists($"{Application.persistentDataPath}/Version.txt") == false ? "" : File.ReadAllText($"{Application.persistentDataPath}/Version.txt");
            Version localVersion = null;
            //第1次玩
            if (localVerStr == "")
            {
                //全部资源下载
                GameFirstLoadHandle();
            }
            else  //游戏已存在，可能会触发热更新
            {
                localVersion = new Version(localVerStr);
                //有版本的区别，有资源需要更新
                if (serverVersion.bigValue > localVersion.bigValue)
                {
                    GameHotHandle();
                }
                else
                {
                    //有版本的区别，有资源需要更新
                    if (serverVersion.smallValue > localVersion.smallValue)
                    {
                        GameHotHandle();
                    }
                }
            }

        });
    }

    void GameFirstLoadHandle()
    {
        //服务器的资源清单文件
        string manifestPath = "127.0.0.1/2203_Game/AssetList.txt";
        LoadXCAsset(manifestPath, (data) =>
        {
            //服务器的资源清单文件内容
            serverManifest = Encoding.UTF8.GetString(data);
            //把文件内容拆分成数组
            string[] assetStrList = serverManifest.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //服务器资源缓存管理
            foreach (var aStr in assetStrList)
            {
                AssetItem a = new AssetItem(aStr);
                //记录服务器的清单资源
                //serverAssetDict.Add(a.assetName, a);

                assetQue.Enqueue(a);
            }
            DownLoadAsset(assetQue.Dequeue());
        });
    }
    /// <summary>
    /// 热更新资源
    /// </summary>
    private void GameHotHandle()
    {
        //服务器的资源清单文件
        string manifestPath = "127.0.0.1/2203_Game/AssetList.txt";
        LoadXCAsset(manifestPath, (data)=>
        {
            //服务器的资源清单文件内容
            string manifestStr = Encoding.UTF8.GetString(data);
            //把文件内容拆分成数组
            string[] assetStrList = manifestStr.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //服务器资源缓存管理
            foreach (var aStr in assetStrList)
            {
                AssetItem a = new AssetItem(aStr);
                //记录服务器的清单资源
                serverAssetDict.Add(a.assetName, a);
            }
            //本地资源清单内容
            string localManifestStr = File.ReadAllText($"{Application.persistentDataPath}/AssetList.txt");
            //把文件内容拆分成数组
            assetStrList = localManifestStr.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //本地资源缓存管理
            foreach (var aStr in assetStrList)
            {
                AssetItem la = new AssetItem(aStr);
                //记录本地的清单资源
                localAssetDict.Add(la.assetName, la);
            }

            CompareManifest();
            DownLoadAsset(assetQue.Dequeue());
        });
    }
    /// <summary>
    /// 对比服务器与本地的资源，找到需要热更新的资源
    /// 数据结构--队列Queue
    /// </summary>
    private void CompareManifest()
    {
        foreach (var sAsset in serverAssetDict)
        {
            //如果本地存在这个资源
            if (localAssetDict.ContainsKey(sAsset.Key))
            {
                //资源有变化，需要热更新的资源
                if (sAsset.Value.md5 != localAssetDict[sAsset.Key].md5)
                {
                    assetQue.Enqueue(sAsset.Value);
                }
            }
            else
            {
                //资源是新增加的，需要热更新的资源
                assetQue.Enqueue(sAsset.Value);
            }
        }
    }

    void LoadXCAsset(string path, Action<byte[]> completeAction)
    {
        StartCoroutine(LoadAsset(path, completeAction));
    }

    /// <summary>
    /// 资源下载执行函数
    /// </summary>
    /// <param name="path">下载的资源路径</param>
    /// <param name="completeAction">下载的过程</param>
    /// <param name="errorStr">下载异常错误提示</param>
    IEnumerator LoadAsset(string path, Action<byte[]> completeAction, Action<string> errorStr = null)
    {
        //Unity下载资源服务器的API  url -> 网页地址
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        //下载请求结果
        UnityWebRequestAsyncOperation op = uwr.SendWebRequest();
        Thread.Sleep(50);
        //一次下载成功的KB
        if (op.isDone)
        {
            completeAction?.Invoke(uwr.downloadHandler.data);
        }

        yield return new WaitForSeconds(1);
    }

    void DownLoadAsset(AssetItem a)
    {
        //服务器的资源路径
        string serverAssetPath = $"127.0.0.1/2203_Game/{a.assetName}";
        LoadXCAsset(serverAssetPath, (data) =>
        {
            //本地的资源路径
            string localAssetPath = $"{Application.persistentDataPath}/{a.assetName}";
            //文件是否存在
            if (File.Exists(localAssetPath))
            {
                File.Delete(localAssetPath);
            }
            //判断文件夹是否存在，不存在创建
            //if (!Directory.Exists(localAssetPath))
            //{
            //    Directory.CreateDirectory(localAssetPath);
            //}
            //把资源写入到本地路径下
        
            File.WriteAllBytes(localAssetPath, data);
            //下载完当前资源后，继续下载
            if (assetQue.Count > 0)
            {
                DownLoadAsset(assetQue.Dequeue());
                Debug.Log($"下载队列下载中，还剩{assetQue.Count}个资源");
            }
            else
            {
                //下载完成，进入游戏或者提示退出游戏，然后大退
                Debug.Log("全部资源下载完毕");
                //记录更新版本号与资源清单
                SaveVersion();
                SaveAssetManifest();
            }
        });
    }

    void SaveVersion()
    {
        File.WriteAllText($"{Application.persistentDataPath}/Version.txt", serverVersion.ver);
    }

    void SaveAssetManifest()
    {
        File.WriteAllText($"{Application.persistentDataPath}/AssetList.txt", serverManifest);
    }
}


public class Version
{
    public int bigValue;
    public int middleValue;
    public int smallValue;
    public string ver;

    public Version(string vStr)
    {
        this.ver = vStr;
        string[] vList = vStr.Split('.');
        this.bigValue = int.Parse(vList[0]);
        this.smallValue = int.Parse(vList[1]);
    }
}

public class AssetItem
{
    public string assetName;
    public string md5;

    public AssetItem(string assetStr)
    {
        string[] assetList = assetStr.Split('|');
        this.assetName = assetList[0];
        this.md5 = assetList[1];
    }
}