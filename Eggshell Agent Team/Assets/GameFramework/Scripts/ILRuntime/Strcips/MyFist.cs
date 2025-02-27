

using ILRuntime.Runtime.Enviorment;

public class MyFist 
{
   public  void OnRuan(AppDomain appDomain)
    {
        appDomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
    }
}
