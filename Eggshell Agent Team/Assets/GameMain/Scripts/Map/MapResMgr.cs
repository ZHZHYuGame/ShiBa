using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResMgr : MonoBehaviour
{
    MapManager mapMgr;
    Dictionary<string,MapTile>  MapTiles = new Dictionary<string, MapTile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/// <summary>
/// 地图块
/// </summary>
public class MapTile
{
    Dictionary<int,EntityControl> tileDic =new Dictionary<int, EntityControl>();
    public void AddEntity(EntityControl entity)
    {
        if(tileDic.ContainsKey(entity.index)) return;
        tileDic.Add(entity.index,entity);
    }

    public void RemoveEntity(int id)
    {
        if (!tileDic.ContainsKey(id)) return;
        tileDic.Remove(id);
    }
}
public class EntityControl 
{
    public int index;//唯一标识
    public Transform tran;//位置与比例
    public EntityControl(Transform tran)
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
}