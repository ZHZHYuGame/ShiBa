using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class ChackAssetsUpdate : MonoBehaviour
{
    private void Start()
    {
        //开启协程
        StartCoroutine(FetchRemoteLabelDownloadSize());
    }

    private IEnumerator FetchRemoteLabelDownloadSize()
    {
        //获取标签为all的所有资源
        AsyncOperationHandle<long> downloadSizeOpHandle = Addressables.GetDownloadSizeAsync("all");
        yield return downloadSizeOpHandle;//获取到downloadSizeOpHandle
        //判断资源状态是否获取完毕
        if (downloadSizeOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            //下载进度
            Debug.LogFormat($"Download Size:{downloadSizeOpHandle.Result.ToString()}");
            if(downloadSizeOpHandle.Result<=0)
            {
                Debug.Log("资源没有更新");
                //进入游戏
                EnterGame();

            }
            else
            {
                Debug.Log("资源有更新");
                //资源下载
                StartCoroutine(DownloadDependencies());
            }
            Addressables.Release(downloadSizeOpHandle);//资源释放
        }
    }

    //资源下载
    private IEnumerator DownloadDependencies()
    {
        AsyncOperationHandle remoteAssetsDownloadDependenciesOpHandle = Addressables.DownloadDependenciesAsync("all");
        //判断这个句柄是否下载完成
        while (!remoteAssetsDownloadDependenciesOpHandle.IsDone)//未下载完
        {
            //获取当前下载多少
            var downloadedBytes = remoteAssetsDownloadDependenciesOpHandle.GetDownloadStatus().DownloadedBytes;
            //获取总资源多少
            var totalBytes = remoteAssetsDownloadDependenciesOpHandle.GetDownloadStatus().TotalBytes;
            Debug.Log($"当前下载: {Mathf.Round(downloadedBytes / 1048579f * 100) / 100}M / 资源总量: {Mathf.Round(totalBytes/1048579f*100)/100}M");
            //slider.value=remoteAssetsDownloadDependenciesOpHandle.PercentComplete;//这个可能不准
            //下载百分比
            var status = remoteAssetsDownloadDependenciesOpHandle.GetDownloadStatus();
            float progress = status.Percent;
            Debug.Log("下载百分比:"+ progress);
            yield return null;
        }
        //下载完成 判断是否下载完成
        if (remoteAssetsDownloadDependenciesOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            //资源释放
            Addressables.Release(remoteAssetsDownloadDependenciesOpHandle);
            //进入游戏
            EnterGame();
        }
    }

    private  async void EnterGame()
    {
        List<string> aotDllList = new List<string>
        {
            "System.Core.dll",
            "System.dll",
            "mscorlib.dll",
            "Unity.Addressables.dll",
            //"Unity.InputSystem.dll",
            "Unity.ResourceManager.dll",
            "UnityEngine.CoreModule.dll"
        };
        foreach (var aotDllName in aotDllList)
        {
            byte[] dllBytes = File.ReadAllBytes($"{Application.streamingAssetsPath}/{aotDllName}");
            //byte[] dllBytes = ReadBytesFromStreamingAssets(aotDllName);
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
            //Debug.Log($"LoadMetadataForAOTAssembly:[aotDllName}. ret: {err)”);
        }
        //Dll文件的加载
        AsyncOperationHandle<TextAsset> loadDllAsync = Addressables.LoadAssetAsync<TextAsset>("HotUpdate.dll");
        await loadDllAsync.Task;
        Assembly hotUpdateAss = Assembly.Load(loadDllAsync.Result.bytes);
        Debug.Log("跳转场景,开始游戏");
        //进行转场 场景激活
        //LoadSceneMode.Single 加载场景模式 
        //Single 模式加载标准 Unity 场景，该场景随后会单独显示在 Hierarchy 窗口中。
        //Additive 加载一个场景，该场景显示在 Hierarchy 窗口中，而另一个场景处于活动状态。
        AsyncOperationHandle<SceneInstance> lasrLoadHandle = Addressables.LoadSceneAsync("TestScene", LoadSceneMode.Single);
        //lasrLoadHandle.Completed += (AsyncOperationHandle<SceneInstance> op) =>
        //{
        //    if (op.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        //生成一个空对象
        //        GameObject kong = new GameObject("GameMain");
        //        //挂载脚本
        //        kong.AddComponent<GameMain>();
        //    }
        //};

    }
}
