using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABTest1 : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        //关于ab包的依赖  一个资源身上用到了别的AB包中的资源 这个时候如果加载自己的AB包
        //通过他创建对象 就会出现资源丢失的情况  需要把依赖包一起加载才能正常

        //加载ab包
        AssetBundle ab= AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+"model");
        //加载ab包资源
        //只使用名字加载 会出现同名不同类型 容易分不清
        //推荐泛型加载 或者指定类型
        //ab.LoadAsset("Cube");
        GameObject go =Instantiate( ab.LoadAsset<GameObject>("Cube"));//泛型加载
        // GameObject go =Instantiate( ab.LoadAsset("Cube", typeof(GameObject)) as GameObject);//指定类型加载

        //ab包不能重复加载
        //卸载场景上所有加载的ab包 参数为true会把场景中所有ab包卸载
        AssetBundle.UnloadAllAssetBundles(false);
        //加载大点的资源 异步加载
        StartCoroutine(LoadABRes("ui","1001"));
        

    }
    IEnumerator LoadABRes(string ABName,string resName)
    {
        //第一步 加载ab包
        AssetBundleCreateRequest abcr= AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
        yield return abcr;
        //第二步 加载资源
        AssetBundleRequest abq= abcr.assetBundle.LoadAssetAsync<Sprite>(resName);
        yield return abq;
        img.sprite=abq.asset as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
