using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMgr :Singleton<ModelMgr>
{
    Dictionary<Type,BaseModel> modelDic = new Dictionary<Type, BaseModel>();

    public void LoadAll()
    {
        //加载方法
       // Load(new  MainModel());  举例
    }

    private void Load(BaseModel model)
    {
        if (!modelDic.ContainsKey(model.GetType()))
        {
            modelDic.Add(model.GetType(), model);
            model.InitModel();
        }
    }

    public T GetModel<T>() where T : BaseModel
    {
        if (modelDic.ContainsKey(typeof(T)))
        {
            return modelDic[typeof(T)] as T;
        }
        else
        {
            return null;
        }
    }
}
