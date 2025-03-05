using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;
    public float playerWH;
    public float terrainWH;
    public GameObject prefab;
    //显示的地形块
    public Dictionary<Vector2, GameObject> showDic = new Dictionary<Vector2, GameObject>();
    //对象池
     Queue<GameObject> pool = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos != player.transform.position)
        {
            //需要显示的列表
            List<Vector2> showList = new List<Vector2>();
            //创建玩家区域
            Rect playerRect = new Rect(player.transform.position.x, player.transform.position.z, playerWH, playerWH);
            //获取玩家所在
            int x = (int)(player.transform.position.x / terrainWH);
            int z = (int)(player.transform.position.z / terrainWH);
            showList.Add(new Vector2(x, z));
            //右
            if (IsLerp(playerRect, new Rect((x + 1) * terrainWH, z * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x + 1, z));
            }
            //左
            if (IsLerp(playerRect, new Rect((x - 1) * terrainWH, z * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x - 1, z));
            }
            //前
            if (IsLerp(playerRect, new Rect(x * terrainWH, (z + 1) * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x, z + 1));
            }
            //后
            if (IsLerp(playerRect, new Rect(x * terrainWH, (z - 1) * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x, z - 1));
            }
            //右前
            if (IsLerp(playerRect, new Rect((x + 1) * terrainWH, (z + 1) * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x + 1, z + 1));
            }
            //左前
            if (IsLerp(playerRect, new Rect((x - 1) * terrainWH, (z + 1) * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x - 1, z + 1));
            }
            //右后
            if (IsLerp(playerRect, new Rect((x + 1) * terrainWH, (z - 1) * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x + 1, z - 1));
            }
            //左后
            if (IsLerp(playerRect, new Rect((x - 1) * terrainWH, (z - 1) * terrainWH, terrainWH, terrainWH)))
            {
                showList.Add(new Vector2(x - 1, z - 1));
            }
            //需要删掉的集合
            List<Vector2> deslist = new List<Vector2>();
            //从正在显示的里面找到不需要显示的
            foreach (var item in showDic.Keys)
            {
                if (!showList.Contains(item))
                {
                    //隐藏并存入对象池
                    showDic[item].SetActive(false);
                    pool.Enqueue(showDic[item]);
                    deslist.Add(item);
                }
            }
            //从字典中删除
            foreach (var item in deslist)
            {
                showDic.Remove(item);
            }
            //找到需要显示但没显示的
            foreach (var item in deslist)
            {
                if (!showList.Contains(item))
                {
                    GameObject terrain;
                    if (pool.Count > 0)
                    {
                        terrain = pool.Dequeue();
                        terrain.SetActive(true);
                    }
                    else
                    {
                        terrain = Instantiate(prefab);
                    }
                    terrain.transform.position = new Vector3(item.x * terrainWH, 0, item.y * terrainWH);
                    showDic.Add(item, terrain);
                }
            }
        }
        playerPos = player.transform.position;
    }
    private bool IsLerp(Rect a, Rect b)
    {
        float aMinX = a.x - a.width / 2;
        float aMaxX = a.x + a.width / 2;
        float aMinZ = a.y - a.height / 2;
        float aMaxZ = a.y + a.height / 2;

        float bMinX = a.x + a.width / 2;
        float bMaxX = a.x + a.width / 2;
        float bMinZ = b.y + a.height / 2;
        float bMaxZ = b.y + a.height / 2;

        if (aMinX < bMaxX && bMinX < aMaxX && aMinZ < bMaxZ && bMinZ < aMaxZ)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}