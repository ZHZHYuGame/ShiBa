using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityIndex
{
    static uint index = 0;
    static readonly object lockObj=new object();
    public static uint Index
    {
        get 
        {
            lock(lockObj)
            {
                return index++;
            }
        }
    }
}

//实体对象
public abstract class EntityBase 
{
    public uint index;//唯一标识
    GameObject obj;
    public virtual void Init(GameObject obj)
    {
        index = EntityIndex.Index;
        this.obj = obj;
        FindChunkByPos();
    }

    public virtual void Show()
    {
        obj.transform.localScale = Vector3.one;
    }

    public virtual void Hide()
    {
        obj.transform.localScale = Vector3.zero;
    }

    private void FindChunkByPos()
    {
       ChunkVector2 chunkPos= ChunkController.Instance.GetCurrentChunkVector(obj.transform.position);
       ChunkController.Instance.GetOrCreateChunk(chunkPos).AddEntity(this);
    }

    public void OnDestory()
    {
        //对象池回收
        lock(this)
        {
            if (obj == null) return;
            Object.Destroy(obj);
            obj = null;
        }

    }

}
