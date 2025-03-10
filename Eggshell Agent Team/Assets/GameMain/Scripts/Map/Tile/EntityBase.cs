using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实体对象
public class EntityBase 
{
    public uint index;//唯一标识
    GameObject obj;
    public EntityBase(GameObject obj)
    {
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
       //ChunkController.Instance.GetOrCreateChunk(chunkPos).AddEntity(this);
    }

    public void Destory()
    {
        //对象池回收
        Object.Destroy(obj);
    }

}
