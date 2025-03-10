using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoSingleton<PlayerControl>
{
    public MapManager mapManager;
    Camera cam;
    int mapScale;
    int pyl;
    int x = 0;
    int y = 0;
    float v;
    float h;
    float speed = 10;

    private void Start()
    {
        cam = Camera.main;
        mapManager.Init(new Map());
        mapScale = mapManager.oneMapScale * 10;
        pyl = mapScale / 2;
        mapManager.CreatMap(x, y);
        cam.orthographicSize = 8;
    }

    void Update()
    {
        v = Input.GetAxis("Vertical"); // 上下移动  
        h = Input.GetAxis("Horizontal"); // 左右移动  

        if (v != 0 || h != 0)
        {
            // 计算移动方向  
            Vector3 moveDirection = new Vector3(h, v, 0).normalized;

            // 移动角色  
            transform.position += moveDirection * speed * Time.deltaTime;

            // 检查地图更新  
            if (Mathf.Floor((transform.position.x + pyl) / mapScale) != x ||
                Mathf.Floor((transform.position.y + pyl) / mapScale) != y)
            {
                x = (int)Mathf.Floor((transform.position.x + pyl) / mapScale);
                y = (int)Mathf.Floor((transform.position.y + pyl) / mapScale);
                mapManager.CreatMap(x, y);
            }
        }
    }
}