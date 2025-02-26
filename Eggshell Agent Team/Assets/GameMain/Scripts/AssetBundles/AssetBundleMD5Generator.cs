using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 生成MD5文件
/// </summary>
public class AssetBundleMD5Generator
{
    [MenuItem("Tools/Generate AssetBundle MD5")]

    public static void GenerateMD5ForAssetBundles()
    {
        //获取AB包文件夹的路径
        string assetBundleOutputPath = Path.Combine(Application.streamingAssetsPath,"AssetBundels");
        //检查assetBundleOutputPath路径是否存在
        if (!Directory.Exists(assetBundleOutputPath))
        {
            Debug.LogError("AssetBundle路径不存在:"+assetBundleOutputPath);
            return;
        }
        //遍历AssetBundle文件夹中的所有文件   
        //assetBundleOutputPath :文件路径  “*”: 匹配所有文件 SearchOption.AllDirectories：包括子文件夹中的路径
        string[] assetBundleFiles = Directory.GetFiles(assetBundleOutputPath,"*",SearchOption.AllDirectories);
        foreach (string file in assetBundleFiles)
        {
            //跳过.meta和.md5文件
            if (file.EndsWith(".meta") || file.EndsWith(".md5"))
            {
                continue;
            }
            //计算md5值
            string md5Hash = CalculateMD5(file);
            //保存md5值
            string md5FilePath = file + ".md5";
            //写入文件
            File.WriteAllText(md5FilePath, file);
        }
    }

    private static string CalculateMD5(string file)
    {
        //使用MD5计算文件的哈希值
        //创建MD5实例
        using (var md5 = MD5.Create())
        //以只读的方式打开指定路径的文件
        using (var stream = File.OpenRead(file))
        {
            //计算当前文件的哈希值
            byte[]hashBytes = md5.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                //将每个字节转换为两位的十六进制字符串。
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
