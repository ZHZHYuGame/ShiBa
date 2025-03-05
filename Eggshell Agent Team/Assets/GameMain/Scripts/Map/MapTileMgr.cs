using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileMgr
{

    MapManager mapMgr;
    Dictionary<string,MapTile>  MapTiles = new Dictionary<string, MapTile>();
    //添加地图块
    public void Create(int x,int y)
    {
        MapTile tile = new MapTile(x,y);
        if(!MapTiles.ContainsKey(tile.name))
        {
            MapTiles.Add(tile.name, tile);
        }
    }
    //获取单个地图块
    public MapTile GetMapTile(int x,int y)
    {
        string key = x + "_" + y;
        if(MapTiles.ContainsKey(key))
        return MapTiles[key];
        else
        Debug.Log("地图块未加载");
        return null;
    }
    //卸载
    public void RemoveTile(int x,int y)
    {
        string key = x + "_" + y;
        if (MapTiles.ContainsKey(key))
        {
            MapTiles[key].DelAllEntity();
            MapTiles.Remove(key);
        }
        else
        {
            Debug.Log("地图块未加载");
        }
    }
}
