using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// 资源加载
/// </summary>
public class ResourcesDownLoader : MonoBehaviour
{
    //private string assetBundleUrl = "http://10.161.57.63/AssetBundles/myassetbundle";
    private string assetBundleUrl = "http://10.161.57.63/";
    private string md5Url = "http://10.161.57.63/AssetBundles/myassetbundle.md5";

    private void Start()
    {
        StartCoroutine(DownloadAndVerifyAssetBundle());
    }
    IEnumerator DownloadAndVerifyAssetBundle()
    {
        //获取资源保存路径
        string savePath = Path.Combine(Application.persistentDataPath, "myassetbundle");
        //下载资源文件
        yield return StartCoroutine(DownLoadFile(assetBundleUrl,savePath));
        //MD5文件的保存路径
        string md5SavePath = savePath + ".md5";
        //下载MD5文件
        yield return StartCoroutine(DownLoadFile(md5Url, md5SavePath));
        //验证MD5值
        if (VerifyMD5(savePath, md5SavePath))
        {
            Debug.Log("资源文件完整性校验通过！");
        }
        else
        {
            Debug.LogError("资源文件完整性校验失败，文件可能已损坏！");
        }
    }

    IEnumerator DownLoadFile(string assetBundleUrl, string savePath)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(assetBundleUrl))
        {
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Debug.LogError("下载失败: " + webRequest.error);
                yield break;
            }
            else
            {
                // 请求成功，处理响应
                File.WriteAllBytes(savePath,webRequest.downloadHandler.data);
                Debug.Log("文件下载完成:"+savePath);
            }
        }
    }
    private bool VerifyMD5(string filePath, string md5FilePath)
    {
        //计算文件的Md5值
        string calculatedMD5 = CalculateMD5(filePath);
        //读取服务器提供的MD5值   Trim()去除首尾空白字符
        string expectedMD5 = File.ReadAllText(md5FilePath).Trim();
        // 输出日志
        Debug.Log("计算MD5: " + calculatedMD5);
        Debug.Log("期望MD5: " + expectedMD5);
        //对比MD5值
        return calculatedMD5 == expectedMD5;
    }

    private string CalculateMD5(string filePath)
    {
        //创建MD5实例
        using (var md5 = MD5.Create())
        //读取文件
        using (var stream = File.OpenRead(filePath))
        {
            byte[] bytes = md5.ComputeHash(stream);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
    private void LoadAssetBundle(string path)
    {
        //加载AssetBundle
        AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null)
        {
            Debug.LogError("加载AssetBundle失败: " + path);
            return;
        }
        //加载资源
        GameObject prefab = assetBundle.LoadAsset("MyPrefab",typeof(GameObject)) as GameObject;
        if (prefab == null)
        {
            Debug.LogError("加载资源失败: MyPrefab");
            return;
        }
        Instantiate(prefab);
        //释放AssetBundle
        assetBundle.Unload(false);
    }
}
