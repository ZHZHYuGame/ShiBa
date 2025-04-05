using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;


public class ABManager : MonoBehaviour
{
    [MenuItem("AssetBundle/打包AB包")]
    public static void PackAssetBundle()
    {

        //AssetBundleMgr
        //AB包输出目录文件夹
        string outPath = $"{Application.streamingAssetsPath}/ABPacks";
        //AB包读取目录文件夹
        string inPath = $"{Application.dataPath}/Resources";
        //获取所有的资源文件
        string[] allFiles = Directory.GetFiles(inPath, "*.*", SearchOption.AllDirectories);
        //打包
        foreach (var f in allFiles)
        {
            //获取每个资源的扩展名
            string e = Path.GetExtension(f);
            //Debug.Log(e);
           
            //print(f);
            if (e != ".meta")
            {
                string fPath = f.Replace(Application.dataPath, "Assets").Replace(@"\", "/");
                
                string rName = Path.GetFileName(f).Split('.')[0];
                AssetImporter p = AssetImporter.GetAtPath(fPath);
               
                if (e == ".png")
                {
                    p.assetBundleName = "Texture2D/" + rName;//"Texture2D/png";//;
                    p.assetBundleVariant = "u3d";
                }
                else
                {
                    p.assetBundleName = rName;
                    p.assetBundleVariant = "u3d";
                }
            }
        }
        //检查文件夹的
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        //清除打过的AB资源
        string[] outAllFiles = Directory.GetFiles(outPath, "*.*", SearchOption.AllDirectories);
        foreach (var f in outAllFiles)
        {
            print(f);
            File.Delete(f);
        }

        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        //弹出AB输出文件夹
        Process.Start(outPath);
    }

}
