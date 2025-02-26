using ILRuntime.Runtime;
using System;
using System.IO;
using UnityEngine;


public class AppDomalinLanch : UnitySingleto<AppDomalinLanch>
{
    //AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
    //大家在正式项目中请全局只创建一个AppDomain
   public ILRuntime.Runtime.Enviorment.AppDomain appdomain;
    public override void Awake()
    {
        base.Awake();
        //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain(ILRuntimeJITFlags.JITOnDemand);
    }
    private void Start()
    { 
      
        LoadHotFixMgr.Instanct.OnLoadHotFix("HotFix_Project", (dllls, dpbs) =>
        {

            OnLoadAssembly(dllls, dpbs);
        });
    } 
    private void OnLoadAssembly(byte[] dlls, byte[] pdbs)
    {
         try
        {
            MemoryStream dllffs=new MemoryStream(dlls);
            MemoryStream pdbffs=new MemoryStream(pdbs);
            ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider pdbReaderProvider =
                new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider();
            appdomain.LoadAssembly(dllffs, pdbffs, pdbReaderProvider); 
        }
        catch (Exception ex)
        {
            Debug.LogError("加载热更新DLL失败=3x"+ex);
        }
        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将
        InitializeILRuntime();//初始化
        
        
    }
    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
    }
}
