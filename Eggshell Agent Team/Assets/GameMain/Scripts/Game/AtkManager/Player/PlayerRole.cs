using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家移动
/// </summary>
public class PlayerRole : MonoSingleton<PlayerRole>
{
    int mapScale;
    int pyl;
    int x = 0;
    int y = 0;
    public ETC etc;
    public float moveSpeed = 0.5f;
    private Vector3 lastMoveDirection = Vector3.forward;

    private float mapWidth=0;
    private float mapHeight=0;
    bool isClomp = false;
    // Start is called before the first frame update
    void Start()
    {
        mapScale = MapManager.Instance.oneMapScale * 10;
        pyl = mapScale / 2;
        GetComponent<BoxCollider>().isTrigger = true;
        MapManager.Instance.CreatMap(x, y);
        GameObject[] etcs = GameObject.FindGameObjectsWithTag("Etc");
        etc = etcs[0].GetComponent<ETC>();
        mapWidth = MapManager.Instance.mapWidth/2;
        mapHeight = MapManager.Instance.mapHeight/2;
        if(mapHeight!=0||mapWidth!=0)
        {
            isClomp = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = etc.GetDis("h");
        float v = etc.GetDis("v");
        

        Vector3 pos = new Vector3(h* moveSpeed, v* moveSpeed, 0);
        Vector3 moveDrection = Camera.main.transform.TransformDirection(pos);
        moveDrection.z = 0;
        moveDrection.Normalize();//向量归一化
        if (pos != Vector3.zero)
        {
            transform.position += pos * Time.deltaTime * 5;
            float angle = Mathf.Atan2(moveDrection.y,moveDrection.x)*Mathf.Rad2Deg;//计算子弹发射角度
             // 检查地图更新  
            if (Mathf.Floor((transform.position.x + pyl) / mapScale) != x ||
                Mathf.Floor((transform.position.y + pyl) / mapScale) != y)
            {
                x = (int)Mathf.Floor((transform.position.x + pyl) / mapScale);
                y = (int)Mathf.Floor((transform.position.y + pyl) / mapScale);
                MapManager.Instance.CreatMap(x, y);
            }
        }
        // 检查玩家是否超出地图边界
        if(isClomp)
        {
            MapClomp();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //武器等级升级
            transform.GetComponent<WeaponOrbitController>().currentLevel++;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    // 检查玩家是否超出地图边界
    private void MapClomp()
    {
        
        if(mapWidth!=0)
        {
            if(transform.position.x<-mapWidth)
            {
                transform.position = new Vector3(-mapWidth, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > mapWidth)
            {
                transform.position = new Vector3(mapWidth, transform.position.y, transform.position.z);
            }
        }
        if(mapHeight!=0)
        {
            if (transform.position.y < -mapHeight)
            {
                transform.position = new Vector3(transform.position.x, -mapHeight, transform.position.z);
            }
            else if (transform.position.y > mapHeight)
            {
                transform.position = new Vector3(transform.position.x, mapHeight, transform.position.z);
            } 
        }

    }
}
