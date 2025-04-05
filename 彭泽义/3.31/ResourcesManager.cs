using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//表示场景中物体的状态
public enum SceneObjStatus
{
    Old, //本次刷新没有加载
    Loading,//加载中
    New    //本次刷新加载
}
//场景中的物体类
public class ScenObj
{
    public ObjData data;//物体数据
   public SceneObjStatus status;//物体状态
    public GameObject obj;//物体对象
    public ScenObj(ObjData data)
    {
        this.data = data;
        this.obj = null;
    }
}
//资源对象类
public class ResourcesObj
{
    public GameObject obj;//资源对象
    private int insNum;//实例数量
    public ResourcesObj(GameObject obj)
    {
        this.obj=obj;
        this.insNum = 0;
    }
    public void CreateIns()
    {
        ++insNum;
    }
    public void DelIns()
    {
        --insNum;
    }
    public bool CheckInsZero()
    {
        return insNum <= 0;
    }
}
//资源管理类
public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance;//单利实例
    public float delTime = 2;//延迟删除时间
    private Dictionary<string, ScenObj> activeObjDic;//活跃物体字典（suid为键）
    private Dictionary<string, ScenObj> inActiveObjDic;//非活跃物体字典（suid为键）
    private List<string> delKeysList;//待删除的键列表
    private Dictionary<string, ResourcesObj> resourcesObjDic;//资源对象字典（resPath为键）
    public Dictionary<string, ScenObj> ActiveObjDic
    {
        get
        {
            if (activeObjDic == null)
            {
                activeObjDic = new Dictionary<string, ScenObj>();
            }
            return activeObjDic;
        }
        set { activeObjDic = value; }
    }

    public Dictionary<string, ScenObj> InActiveObjDic
    {
        get
        {
            if (inActiveObjDic == null)
            {
                inActiveObjDic = new Dictionary<string, ScenObj>();
            }
            return inActiveObjDic;
        }
        set { inActiveObjDic = value; }
    }

    public List<string> DelKeysList
    {
        get
        {
            if (delKeysList == null)
            {
                delKeysList = new List<string>();
            }
            return delKeysList;
        }
        set { delKeysList = value; }
    }

    public Dictionary<string, ResourcesObj> ResourcesObjDic
    {
        get
        {
            if (resourcesObjDic == null)
            {
                resourcesObjDic = new Dictionary<string, ResourcesObj>();
            }
            return resourcesObjDic;
        }
        set { resourcesObjDic = value; }
    }
    private void Awake()
    {
        Instance=this;//设置单利实例
    }
    private void OnEnable()
    {
        StartCoroutine(IEDel());//启动携程进行延迟删除操作
    }

    private IEnumerator IEDel()
    {
        while (true)
        {
            bool bDel = false;
            foreach (var pair in InActiveObjDic)
            {
                ResourcesObj resourceObj;
                if (ResourcesObjDic.TryGetValue(pair.Value.data.resPath, out resourceObj))
                {
                    resourceObj.DelIns();
                    if (resourceObj.CheckInsZero())
                    {
                        bDel = true;
                        resourceObj.obj = null;
                        ResourcesObjDic.Remove(pair.Value.data.resPath);
                    }
                }
                Destroy(pair.Value.obj);
            }
            InActiveObjDic.Clear();
            if (bDel)
            {
                Resources.UnloadUnusedAssets(); // 卸载未使用的资源
            }
            yield return new WaitForSeconds(delTime);
        }
    }
    private void Update()
    {
        
    }
    //检查物体是否处于活跃状态
    public ScenObj CheckIsActive(string sUid)
    {
        ScenObj obj;
        if(activeObjDic.TryGetValue(sUid,out obj))
        {
            return obj;
        }return null;
    }
    //检查物体是否处于非活跃状态
    public ScenObj CheckIsInActive(string sUid)
    {
        ScenObj obj;
        if(inActiveObjDic.TryGetValue(sUid,out obj))
        {
            return (obj);
        }
        return null;
    }
    //将物体从非活跃状态移动到活跃状态
    private bool MoveToActive(ObjData obj)
    {
        ScenObj scenObj;
        if(InActiveObjDic.TryGetValue(obj.sUid,out scenObj))
        {
            scenObj.obj.SetActive(true);
            scenObj.status = SceneObjStatus.New;
            activeObjDic.Add(obj.sUid, scenObj);
            InActiveObjDic.Remove(obj.sUid);
            return true;
        }
       return false;
    }
    //创建物体对象
    private void CreateObj(GameObject prefab,ScenObj scenObj)
    {
        scenObj.obj=Instantiate(prefab);
        scenObj.obj.transform.position = scenObj.data.pos;
        scenObj.obj.transform.rotation = scenObj.data.rotation;
    }
    //加载物体
    public void Load(ObjData obj)
    {
        if(CheckIsActive(obj.sUid)!=null)
        {
            return;
        }
        if(!MoveToActive(obj))
        {
            ScenObj scenobj = new ScenObj(obj);
            scenobj.status = SceneObjStatus.New;
            GameObject resObj = null;
            ResourcesObj resourceObj;
            if(resourcesObjDic.TryGetValue(obj.resPath,out resourceObj))
            {
                resObj = resourceObj.obj;
                resourceObj.CreateIns();
            }else
            {
                resObj = Resources.Load<GameObject>(obj.resPath);
            }
            CreateObj(resObj, scenobj);
            activeObjDic.Add(obj.sUid, scenobj);
        }
       
    }
    //异步加载物体
    public void LoadAsync(ObjData obj)
    {
        if(CheckIsActive(obj.sUid)!=null)
        {
            return;
        }
        if(!MoveToActive(obj))
        {
            StartCoroutine(IELoad(obj));
        }
    }
    private IEnumerator IELoad(ObjData obj)
    {
        ScenObj scenObj=new ScenObj(obj);
        scenObj.status = SceneObjStatus.Loading;
        activeObjDic.Add(obj.sUid, scenObj);
        GameObject resObj = null;
        ResourcesObj resourceObj;
        if(ResourcesObjDic.TryGetValue(obj.resPath,out resourceObj))
        {
            resObj= resourceObj.obj;
            resourceObj.CreateIns();
        }else
        {
            ResourceRequest request = Resources.LoadAsync<GameObject>(obj.resPath);
            yield return request;
            resObj = request.asset as GameObject;
        }
        CreateObj(resObj, scenObj);
        scenObj.status = SceneObjStatus.New;
    }
    //刷新物体状态
    public void RefreshStatus()
    {
        delKeysList.Clear();
        foreach (var pair in activeObjDic)
        {
            ScenObj scenObj = pair.Value;
            if(scenObj.status==SceneObjStatus.Old)
            {
                delKeysList.Add(pair.Key);
            }
            else if(scenObj.status==SceneObjStatus.New)
            {
                scenObj.status = SceneObjStatus.Old;
            }
        }
        for(int i=0;i<delKeysList.Count;++i)
        {
            MoveToActive(ActiveObjDic[delKeysList[i]].data);
        }
    }
}
