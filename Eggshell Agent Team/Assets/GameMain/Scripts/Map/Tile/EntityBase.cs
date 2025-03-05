using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实体对象
public class EntityBase
{
    public int index;//唯一标识
    public Transform tran;
    public EntityBase(Transform tran)
    {
        this.tran = tran;
    }

    public virtual void Show()
    {
        tran.localScale = Vector3.one;
    }

    public virtual void Hide()
    {
        tran.localScale = Vector3.zero;
    }
    public void Destory()
    {
        //对象池回收
        //Object.Destroy(tran.gameObject);
    }
}
