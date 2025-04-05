using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
/// <summary>
/// 更新管理
/// </summary>
public class GameUpdateMgr : MonoBehaviour
{
    //加载路径
    string folder = "/PC/";
    // Start is called before the first frame update
    void Start()
    {
        //p目录是否存在，不存在表明是初次运行
        if (!Directory.Exists(GetP))
        {
            //创建文件夹
            Directory.CreateDirectory(GetP);
            //开启协程 开始文件拷贝
            StartCoroutine(CopyFiles());//首次 进行S=>P的复制
            Debug.Log("首次运行,进行S=>P的复制");
        }
        else
        {
            Debug.Log("热更新流程");
            //开启协程 进行热更新
            StartCoroutine(CheckUpdate());
        }
    }
    //文件拷贝
    IEnumerator CopyFiles()
    {
        //读取本地目录下的配置文件
        VersionConfig vcf = JsonConvert.DeserializeObject<VersionConfig>(File.ReadAllText(GetS + "config.txt"));
        //遍历ab包列表 进行拷贝
        for (int i = 0; i < vcf.aBInfos.Count; i++)
        {
            //获取ab包在s目录下的路径
            string sourcePath = GetS + vcf.aBInfos[i].ABName;
            //获取ab包在p目录下的路径
            string destPath = GetP + vcf.aBInfos[i].ABName;
            //进行文件的复制
            File.Copy(sourcePath, destPath);

        }
        //进行配置文件的复制或写入
        File.Copy(GetS + "config.txt", GetP + "config.txt");
        yield return null;
    }

    //热更新
    IEnumerator CheckUpdate()
    {
        //1.先进行本地读取
        VersionConfig localVcf = JsonConvert.DeserializeObject<VersionConfig>(File.ReadAllText(GetP + "config.txt"));
        //2.在进行远程读取
        string remoteJson = string.Empty;
        //获取远程服务器配置文件的路径地址
        string remoteUrl = localVcf.Url + folder + "config.txt";
        //向远程发送一个Http请求 请求路径就是远程配置文件的路径
        UnityWebRequest uwr = UnityWebRequest.Get(remoteUrl);
        yield return uwr.SendWebRequest();
        //请求有响应
        if (uwr.isDone)
        {
            //下载器 下载文本
            remoteJson = uwr.downloadHandler.text;
        }
        //解析下载的文本
        VersionConfig remoteVcf = JsonConvert.DeserializeObject<VersionConfig>(remoteJson);

        //3.本地存字典
        Dictionary<string, ABInfo> loclDic = new Dictionary<string, ABInfo>();
        //遍历本地配置配置文件
        int count = localVcf.aBInfos.Count;
        for (int i = 0; i < count; i++)
        {
            loclDic.Add(localVcf.aBInfos[i].ABName, localVcf.aBInfos[i]);
        }
        //4.声明新增更新列表集合
        List<ABInfo> updataeList = new List<ABInfo>();
        //5.本地和远程版本比对，遍历远程配置列表
        //本地版本号小于远程服务器版本号
        if (localVcf.Code < remoteVcf.Code)
        {
            for (int i = 0; i < remoteVcf.aBInfos.Count; i++)
            {
                //6.本地存在远程服务器上的文件，则对比本地和远程服务器的MD5值，不一样，添加到新增更新列表中
                if (loclDic.ContainsKey(remoteVcf.aBInfos[i].ABName))
                {
                    //则对比本地和远程服务器的文件的MD5值,不一样
                    if (loclDic[remoteVcf.aBInfos[i].ABName].Md5.CompareTo(remoteVcf.aBInfos[i].Md5) != 0)
                    {
                        //添加到更新列表中
                        updataeList.Add(remoteVcf.aBInfos[i]);
                    }
                }
                else
                {
                    //7.本地不存在远程服务器上的文件，则直接添加到新增更新列表中
                    updataeList.Add(remoteVcf.aBInfos[i]);
                }
            }
            //8.遍历新增更新列表，通过unitywebrequest 进行下载，存储到p目录下
            for (int i = 0; i < updataeList.Count; i++)
            {
                //获取下载的远程服务器的地址
                string dowmLoadUrl = localVcf.Url + folder + updataeList[i].ABName;
                UnityWebRequest duwr = UnityWebRequest.Get(dowmLoadUrl);
                yield return duwr.SendWebRequest();
                if (duwr.isDone)
                {
                    //获取存入文件的路径地址
                    string savePath = GetP + updataeList[i].ABName;
                    //写入内容
                    File.WriteAllBytes(savePath, duwr.downloadHandler.data);
                }
            }
            //9.完成后，更新远程配置文件到本地配置文件
            File.WriteAllText(GetP + "config.txt", remoteJson);
        }
        else
        {
            Debug.Log("直接进入游戏");
        }
        yield return null;
        Debug.Log("进入游戏");
    }

    /// <summary>
    /// s路径
    /// </summary>
    public string GetS
    {
        get
        {
            return Application.streamingAssetsPath + folder;
        }
    }
    /// <summary>
    /// p路径
    /// </summary>
    public string GetP
    {
        get
        {
            return Application.persistentDataPath + folder;
        }
    }
}