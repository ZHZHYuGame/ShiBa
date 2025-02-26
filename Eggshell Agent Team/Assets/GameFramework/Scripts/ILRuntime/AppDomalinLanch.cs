using ILRuntime.Runtime;
using System;
using System.IO;
using UnityEngine;


public class AppDomalinLanch : UnitySingleto<AppDomalinLanch>
{
    //AppDomain��ILRuntime����ڣ��������һ���������б��棬������Ϸȫ�־�һ��������Ϊ��ʾ�����㣬ÿ���������涼��������һ��
    //�������ʽ��Ŀ����ȫ��ֻ����һ��AppDomain
   public ILRuntime.Runtime.Enviorment.AppDomain appdomain;
    public override void Awake()
    {
        base.Awake();
        //����ʵ����ILRuntime��AppDomain��AppDomain��һ��Ӧ�ó�����ÿ��AppDomain����һ��������ɳ��
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
            Debug.LogError("�����ȸ���DLLʧ��=3x"+ex);
        }
        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ����������ڻ��������ڴ棬��ʽ����ʱ�뽫
        InitializeILRuntime();//��ʼ��
        
        
    }
    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //����Unity��Profiler�ӿ�ֻ���������߳�ʹ�ã�Ϊ�˱�����쳣����Ҫ����ILRuntime���̵߳��߳�ID������ȷ���������к�ʱ�����Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //������һЩILRuntime��ע�ᣬHelloWorldʾ����ʱû����Ҫע���
    }
}
