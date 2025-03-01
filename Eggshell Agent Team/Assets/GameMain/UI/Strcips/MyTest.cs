using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest
{
    //测试脚本
    public void OnRun(AppDomain AppDomain)
    {
        AppDomain.Invoke("HotFix_Project.Scripts.Test", "TestA",null,null);

        ILTypeInstance type = AppDomain.Instantiate("HotFix_Project.Scripts.Test");
        AppDomain.Invoke("HotFix_Project.Scripts.Test", "TestB",type,1);
    }
}
