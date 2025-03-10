using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneEntry : MonoBehaviour
{
    GameObject player;
    Role role;
    public Camera cam;
    public GameObject hpBase;
    public GameObject hurttx;//飘血
    public GameObject bullet;//子弹
    public GameObject expPrefab;//经验
    Canvas canvas;
    AllObjectPool allObjectPool;
    // Start is called before the first frame update
    void Start()
    {
        //画布获取
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        //关卡
        Map map = ConfigMgr.GetDicData<Map>("Map", PlayerPrefs.GetInt("levelIndex"));

        //地图加载
        MapData mapData = ConfigMgr.GetDicData<MapData>("MapDatas",map.Id);
        MapManager.Instance.Init(mapData);

       
        //生成玩家
        role = ConfigMgr.GetListData<Role>("Role",0);
        
        player = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(role.This_object_path));
        Debug.Log(player.name);
        //血条加载
        hpBase = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameMain/GameResources/Prefabs/HpBase.prefab"), canvas.transform);
        hpBase.GetComponent<HpBase>().Init(player, role);
        //怪物生成规则
        player.AddComponent<EnemySpawner>();
        player.GetComponent<EnemySpawner>().Init(map);
        
        //相机加载
        cam.gameObject.AddComponent<CameraMgr>().Init(player.transform);
        //摇杆加载 

        //绑定玩家

        //对象池管理
         allObjectPool = new AllObjectPool(hurttx, bullet, expPrefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
