using System;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    One,
    Two,
    Three
}

public class MapManager : MonoSingleton<MapManager>
{
    [Range(1, 10)]
    public int oneMapScale = 1; // 地图比例
    int w, h; // 地图块的宽度和高度数量
    public bool isMove = true; // 是否动态生成地图块
    public MapType currType = MapType.One; // 当前地图类型
    GameObject[,] mapPool; // 地图块池
    [SerializeField]
    Sprite[] sprites; // 地图块精灵
    public GameObject map; // 地图块预制体
    int mapTileSize = 10; // 每个地图块的大小
    public void Init(Map data)
    {

        InitMap();
    }
    private void InitMap()
    {
        // 根据当前的地图类型设置宽高和初始化地图块  
        switch (currType)
        {
            case MapType.One:
                w = 3; // 初始宽度  
                h = 3; // 初始高度  
                break;
            case MapType.Two:
                w = 1; // 初始宽度  
                h = 3; // 初始高度  

                break;
            case MapType.Three:
                w = 5; // 固定宽度  
                h = 5; // 固定高度  
                isMove = false;
                break;
            default:
                break;
        }
        map.GetComponent<SpriteRenderer>().sprite = sprites[0];
        mapPool = new GameObject[w, h]; // 初始化地图池  
        // 创建地图块并隐藏  
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                mapPool[i, j] = Instantiate(map, transform);
                int mapX = 0 - 2 + i; // 将中心对齐到x  
                int mapY = 0 - 2 + j; // 将中心对齐到y  

                UpdateMapTile(mapPool[i, j], mapX, mapY);
                mapPool[i, j].transform.localScale = isMove ? Vector3.zero : Vector3.one * oneMapScale; // 初始化为缩放为零  
            }
        }
    }

    public void CreatMap(int x, int y)
    {
        if (!isMove) return;
        List<GameObject> viewMap = new List<GameObject>();
        // 根据当前地图类型判断显示方式  
        switch (currType)
        {
            case MapType.One:
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int mapX = x + i;
                        int mapY = y + j;

                        UpdateMapTile(mapPool[i + 1, j + 1], mapX, mapY);
                        viewMap.Add(mapPool[i + 1, j + 1]);
                    }
                }
                break;

            case MapType.Two:
                for (int j = -1; j <= 1; j++)
                {
                    int mapX = x;
                    int mapY = y + j;

                    UpdateMapTile(mapPool[0, j + 1], mapX, mapY);
                    viewMap.Add(mapPool[0, j + 1]);
                }
                break;

            case MapType.Three:

                break;
        }

        // 隐藏不在视图中的地图块  
        for (int i = 0; i < mapPool.GetLength(0); i++)
        {
            for (int j = 0; j < mapPool.GetLength(1); j++)
            {
                if (!viewMap.Contains(mapPool[i, j]))
                {
                    mapPool[i, j].transform.localScale = Vector3.zero; // 隐藏该地图块  
                }
            }
        }
    }
    private void UpdateMapTile(GameObject tile, int x, int y)
    {
        tile.transform.position = new Vector3(x * 10 * oneMapScale, y * 10 * oneMapScale, 0);
        tile.transform.localScale = Vector3.one * oneMapScale; // 恢复地图块的缩放  
    }


}