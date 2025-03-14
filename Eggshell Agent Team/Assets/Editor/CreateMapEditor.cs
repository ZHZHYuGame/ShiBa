using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;



// 地图编辑器窗口
public class CreateMapEditor : EditorWindow
{
    // 存储地图数据的字典
    static Dictionary<int, MapData> mapDictionary;

    // 菜单项，打开地图编辑器窗口
    [MenuItem("Tools/地图编辑器")]
    public static void Init()
    {
        GetWindow<CreateMapEditor>().Show(); // 显示地图编辑器窗口
        LoadMapData();
    }

    // 编辑器窗口中的变量
    private int mapId=1; // 当前编辑的地图ID
    private MapType mapType;
    private Sprite sprite; // 背景图片
    private int oneMapScale;//地图比例
    private int mapLength;//地图边长
    private int mapWidth;
    private int mapHeight;
    private Vector2 scrollPosition; // 滚动视图的滚动位置

    // 加载地图数据
    private static void LoadMapData()
    {
        mapDictionary = ConfigMgr.GetTable<Dictionary<int,MapData>>("MapDatas");
        if (mapDictionary == null)
        {
            mapDictionary = new Dictionary<int, MapData>();
        }
    }
    

    // 绘制GUI
    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition); // 开始滚动视图


        // 输入地图ID
        mapId = EditorGUILayout.IntField("地图ID：", mapId);
        if (!mapDictionary.ContainsKey(mapId)) // 如果字典中没有该ID
        {
            mapDictionary.Add(mapId, new MapData()); // 添加新地图数据
        }

        // 更新地图数据
        mapDictionary[mapId].mapId = mapId; // 设置地图ID
        mapDictionary[mapId].name = EditorGUILayout.TextField("地图名称：", mapDictionary[mapId].name); // 输入地图名称

        // 背景图片
        if (!string.IsNullOrEmpty(mapDictionary[mapId].spritePath))
        {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/GameMain/GameResources/2DSprits/UI/"+ mapDictionary[mapId].spritePath+".png");
           // sprite = ResourcesLoader.LoadResources<Sprite>(Application.streamingAssetsPath + "/ui",mapDictionary[mapId].spritePath, "ui");
        }
        sprite = (Sprite)EditorGUILayout.ObjectField("背景图片", sprite, typeof(Sprite), false);
        if (sprite != null)
        {
            mapDictionary[mapId].spritePath = sprite.name;
        }
        //地图类型
        mapDictionary[mapId].type = (MapType)EditorGUILayout.EnumPopup("地图类型：", mapDictionary[mapId].type);

        mapDictionary[mapId].mapWidth = EditorGUILayout.IntField("地图宽：", mapDictionary[mapId].mapWidth);

        mapDictionary[mapId].mapHeight = EditorGUILayout.IntField("地图高：", mapDictionary[mapId].mapHeight);


        mapDictionary[mapId].oneMapScale = EditorGUILayout.IntField("地图比例：", mapDictionary[mapId].oneMapScale);

        mapDictionary[mapId].mapLength = EditorGUILayout.IntField("地图边长：", mapDictionary[mapId].mapLength);


        // 地图预览
        GUILayout.Label("地图预览", EditorStyles.boldLabel);
        if (sprite != null)
        {
            GUILayout.Label(sprite.texture, GUILayout.Width(200), GUILayout.Height(200));
        }

        //// 敌人分布编辑
        //GUILayout.Label("敌人分布编辑", EditorStyles.boldLabel);
        //if (GUILayout.Button("打开敌人分布编辑工具"))
        //{
        //    EnemyPlacementTool.ShowWindow(mapDictionary[mapId]);
        //}

        // 保存数据按钮
        if (GUILayout.Button("保存数据"))
        {
            ConfigMgr.Save("MapDatas", mapDictionary); // 保存地图数据
            AssetDatabase.Refresh(); // 刷新资产数据库
        }

        GUILayout.EndScrollView(); // 结束滚动视图
    }

}

//// 敌人分布编辑工具
//public class EnemyPlacementTool : EditorWindow
//{
//    private GameObject enemyPrefab; // 敌人预制体
//    private MapData currentMapData; // 当前地图数据

//    public static void ShowWindow(MapData mapData)
//    {
//        EnemyPlacementTool window = GetWindow<EnemyPlacementTool>("敌人分布编辑工具");
//        window.currentMapData = mapData;
//    }

//    private void OnGUI()
//    {
//        enemyPrefab = (GameObject)EditorGUILayout.ObjectField("敌人预制体", enemyPrefab, typeof(GameObject), false);

//        if (GUILayout.Button("放置敌人"))
//        {
//            PlaceEnemy();
//        }

//        if (GUILayout.Button("保存敌人分布"))
//        {
//            SaveEnemyLayout();
//        }
//    }

//    // 在场景中放置敌人
//    private void PlaceEnemy()
//    {
//        if (enemyPrefab == null)
//        {
//            Debug.LogError("请先选择敌人预制体！");
//            return;
//        }

//        // 在场景中实例化敌人
//        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
//        Undo.RegisterCreatedObjectUndo(enemy, "Place Enemy");
//    }

//    // 保存敌人分布
//    private void SaveEnemyLayout()
//    {
//        if (currentMapData == null)
//        {
//            Debug.LogError("当前地图数据未加载！");
//            return;
//        }

//        // 获取场景中所有敌人
//        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//        currentMapData.enemyLayout = new List<EnemyData>();

//        foreach (var enemy in enemies)
//        {
//            EnemyData data = new EnemyData
//            {
//                enemyType = enemy.name,
//                position = enemy.transform.position
//            };
//            currentMapData.enemyLayout.Add(data);
//        }

//        Debug.Log($"敌人分布已保存到地图 {currentMapData.name}");
//    }
//}