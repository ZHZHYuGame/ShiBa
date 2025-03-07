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
    private void Awake()
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
                map.GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case MapType.Two:
                w = 1; // 初始宽度  
                h = 3; // 初始高度  
                oneMapScale = 3;
                map.GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case MapType.Three:
                w = 5; // 固定宽度  
                h = 5; // 固定高度
                map.GetComponent<SpriteRenderer>().sprite = sprites[0];
                isMove = false; // 固定地图块
                break;
            default:
                break;
        }

        mapPool = new GameObject[w, h]; // 初始化地图池  
        // 创建地图块并设置大小
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                mapPool[i, j] = Instantiate(map, transform);
                mapPool[i, j].transform.localScale = Vector3.one * oneMapScale; // 设置地图块大小

                // 以 (0, 0, 0) 为中心生成地图块
                float xPos = (i - (w - 1) / 2f) * mapTileSize * oneMapScale;
                float yPos = (j - (h - 1) / 2f) * mapTileSize * oneMapScale;
                mapPool[i, j].transform.position = new Vector3(xPos, yPos, 0);

                mapPool[i, j].SetActive(!isMove); // 如果 isMove 为 false，则显示地图块
            }
        }
    }

    public void CreatMap(int x, int y)
    {
        if (!isMove) return; // 如果 isMove 为 false，则不需要动态生成地图块

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
                // 固定显示整个地图
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        int mapX = x - 2 + i; // 将中心对齐到x  
                        int mapY = y - 2 + j; // 将中心对齐到y  

                        UpdateMapTile(mapPool[i, j], mapX, mapY);
                        viewMap.Add(mapPool[i, j]);
                    }
                }
                break;
        }

        // 隐藏不在视图中的地图块  
        for (int i = 0; i < mapPool.GetLength(0); i++)
        {
            for (int j = 0; j < mapPool.GetLength(1); j++)
            {
                if (!viewMap.Contains(mapPool[i, j]))
                {
                    mapPool[i, j].SetActive(false); // 隐藏该地图块  
                }
            }
        }
    }

    private void UpdateMapTile(GameObject tile, int x, int y)
    {
        // 以 (0, 0, 0) 为中心更新地图块位置
        float xPos = (x - (w - 1) / 2f) * mapTileSize * oneMapScale;
        float yPos = (y - (h - 1) / 2f) * mapTileSize * oneMapScale;
        tile.transform.position = new Vector3(xPos, yPos, 0);
        tile.SetActive(true); // 显示该地图块
    }

   
}