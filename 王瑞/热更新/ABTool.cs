using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class ABToopl 
{
    [MenuItem("Tool/选中打包 Pack AB", false, 111)]
    static void AutoPackAB4()
    {
        //获取资源
        Object[] objs = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        for (int i = 0; i < objs.Length; i++)
        {
            //获取资源路径
            string assetPath = AssetDatabase.GetAssetPath(objs[i]);
            //获取资源的导入器
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            assetImporter.assetBundleName = objs[i].name;
            assetImporter.assetBundleVariant = "u3d";
        }
        //打包                          AB包路径     资源包构建选项                                   平台
        BuildPipeline.BuildAssetBundles(GetOutPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);

        //生成配置文件
        MakeVersionConfig();

        //刷新
        AssetDatabase.Refresh();
        //清除标记
        ClearFlag();
    }
    //分类自动打包(不需要选中某个文件打包)
    [MenuItem("Tool/Build AB With Handle4", false, 111)]
    static void HandleAB4()
    {
        //Res文件需要在Assets文件夹下
        //获取Res下的所有文件夹
        string[] dirs = Directory.GetDirectories(Application.dataPath + "/Res/", "*.*", SearchOption.AllDirectories);
        for (int i = 0; i < dirs.Length; i++)
        {

            //获取文件夹名字
            string dirName = Path.GetFileName(dirs[i]);
            //路径
            Debug.Log($"文件夹路径:{dirs[i]} 文件夹名称:{dirName}");
            //获取Res下每个文件夹下的所有文件
            string[] filePaths = Directory.GetFiles(dirs[i], "*.*", SearchOption.AllDirectories);
            for (int j = 0; j < filePaths.Length; j++)
            {
                //排除掉.meta文件
                if (!filePaths[j].EndsWith(".meta"))
                {
                    //获取当前路径
                    string str1 = Application.dataPath;
                    //(获取当前文件的相对路径) 用来资源导入器
                    //将当前路径转换成Assts 在将所有的\转换为/
                    string str2 = filePaths[j].Replace(str1, "Assets").Replace(@"\", "/").Trim();
                    Debug.Log($"str1 = {str1},str2 = {str2}");
                    //获取资源导入器
                    AssetImporter assetImporter = AssetImporter.GetAtPath(str2);
                    assetImporter.assetBundleName = dirName;
                    assetImporter.assetBundleVariant = "u3d";
                }
            }
        }


        //打包                          AB包路径     资源包构建选项                                   平台
        BuildPipeline.BuildAssetBundles(GetOutPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        //生成配置文件
        MakeVersionConfig();
        //刷新
        AssetDatabase.Refresh();
        //清除标记
        ClearFlag();
    }
    /// <summary>
    /// 清除标记
    /// </summary>
    static void ClearFlag()
    {
        Object[] objs = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        for (int i = 0; i < objs.Length; i++)
        {
            //获取资源路径    
            string assetPath = AssetDatabase.GetAssetPath(objs[i]);
            //获取资源的导入器
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            assetImporter.assetBundleName = "";
        }
        //移除未使用的标记
        AssetDatabase.RemoveUnusedAssetBundleNames();
    }
    /// <summary>
    /// 获取AB包输出路径
    /// </summary>
    static string GetOutPath
    {
        get
        {
            //获取文件夹
            string outPath = Application.streamingAssetsPath + "/PC/";
            //如果不包含这个文件
            if (!Directory.Exists(outPath))
            {
                //创建一个文件
                Directory.CreateDirectory(outPath);
            }
            return outPath;
        }
    }
    [MenuItem("Tool/打开P目录", false, 111)]
    static void OpenP()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
    /// <summary>
    /// 生成配置文件
    /// </summary>
    static void MakeVersionConfig()
    {
        //清除打包版本
        //EditorPrefs.DeleteKey("code");
        //累加打包版本
        EditorPrefs.SetInt("code", EditorPrefs.GetInt("code") + 1);
        VersionConfig vcf = new VersionConfig();
        vcf.Code = EditorPrefs.GetInt("code");
        //使用hfs应用获取路径 streamingAssetsPath/
        vcf.Url = "http://10.161.57.63:8080/StreamingAssets/";
        //获取输出路径下面的所有资源
        string[] files = Directory.GetFiles(GetOutPath, "*.*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            //获取后缀名为u3d的文件 
            if (Path.GetExtension(files[i]) == ".u3d" || Path.GetExtension(files[i]) == "")
            {
                //获取文件的名称
                string name = Path.GetFileName(files[i]);
                //获取文件的大小
                int len = File.ReadAllBytes(files[i]).Length;
                //获取文件的md5值
                string md5 = GetFileMd5(files[i]);
                ABInfo abInfo = new ABInfo(name, len, md5);
                vcf.aBInfos.Add(abInfo);
            }
        }
        //对象序列为json字符串
        string json = JsonConvert.SerializeObject(vcf);
        //写入到文本
        File.WriteAllText(GetOutPath + "config.txt", json);
        //刷新
        AssetDatabase.Refresh();

    }
    /// <summary>
    /// 获取文件的md5值
    /// </summary>
    static string GetFileMd5(string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Open);
        MD5 md5 = MD5.Create();
        byte[] bytes = md5.ComputeHash(fs);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString());
        }
        fs.Close();
        return sb.ToString();
    }
}
