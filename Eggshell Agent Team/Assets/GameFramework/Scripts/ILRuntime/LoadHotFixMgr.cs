using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadHotFixMgr : UnitySingleto<LoadHotFixMgr>
{
    private string DLLPath = $"{Application.streamingAssetsPath}/HotFix/";

    public void OnLoadHotFix(string filename, Action<byte[], byte[]> OnDatas)
    {
       StartCoroutine(LoadHotFixAssembly(filename,OnDatas));
    }
    private IEnumerator LoadHotFixAssembly(string filename, Action<byte[], byte[]> OnDatas)
    {
        string UrlDll = DLLPath + filename + ".dll";
        UnityWebRequest webRequest=UnityWebRequest.Get(UrlDll);
        yield return webRequest.SendWebRequest();
        byte[] dll = null;
        if(webRequest.isNetworkError)
        {
            Debug.LogError("加载热更新的DLL失败");
        }
        else
        {
            dll = webRequest.downloadHandler.data;
        }

       string UrlPdb= DLLPath + filename + ".pdb";
        UnityWebRequest pdbRequest = UnityWebRequest.Get(UrlPdb);
        yield return pdbRequest.SendWebRequest();
        byte [] pdb = null;
        if (pdbRequest.isNetworkError)
        {
            Debug.LogError("加载热更新的PDB失败");
        }
        else
        {
            pdb = pdbRequest.downloadHandler.data;
        }
       if(OnDatas!=null)
        {
            OnDatas(dll, pdb);
        }
       webRequest.Dispose();
       pdbRequest.Dispose();
    }
    
}
