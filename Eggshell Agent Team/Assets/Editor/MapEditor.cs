using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

public class MapEditor : EditorWindow
{
    private int[,] map; // 地图数据
    private int mapWidth = 3; // 地图宽度
    private int mapHeight = 3; // 地图高度
    private float cellSize = 1.0f; // 每个单元格的大小
    private Stack<Action> undoStack = new Stack<Action>(); // 撤销栈

    private GameObject mapParent; // 地图对象的父物体
    private Material selectedMaterial; // 当前选中的材质
    private Material defaultMaterial; // 默认材质

    private bool isEditMaterialMode = false; // 是否处于材质编辑模式
    private string[] mapTypes = new string[] { "九宫格类型", "前中后长方形", "单个正方形" };
    private int selectedMapType = 0; // 当前选中的地图类型

    private string mapDataFolder = "Assets/GameMain/Maps"; // 地图数据文件夹
    private string materialFolder = "Assets/GameMain/Materials"; // 材质文件夹

    [MenuItem("Tools/地图编辑器")]
    public static void ShowWindow()
    {
        GetWindow<MapEditor>("地图编辑器");
    }

    private void OnEnable()
    {
        // 加载材质
        LoadMaterials();
        // 初始化地图
        InitializeMap();
    }

    private void OnGUI()
    {
        GUILayout.Label("地图编辑器", EditorStyles.boldLabel);

        // 地图类型选择
        GUILayout.BeginHorizontal();
        GUILayout.Label("地图类型:");
        selectedMapType = EditorGUILayout.Popup(selectedMapType, mapTypes);

        GUILayout.EndHorizontal();

        // 初始化地图按钮
        if (GUILayout.Button("初始化地图"))
        {
            InitializeMap();
        }

        // 材质编辑模式切换
        isEditMaterialMode = GUILayout.Toggle(isEditMaterialMode, "材质编辑模式");

        // 显示材质按钮
        GUILayout.Label("材质选择", EditorStyles.boldLabel);
        string[] materialPaths = Directory.GetFiles(materialFolder, "*.mat");
        foreach (string path in materialPaths)
        {
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material != null)
            {
                if (GUILayout.Button(material.name))
                {
                    selectedMaterial = material; // 切换选中的材质
                }
            }
        }

        // 保存和加载地图按钮
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("保存地图"))
        {
            SaveMap();
        }
        if (GUILayout.Button("加载地图"))
        {
            LoadMap();
        }
        GUILayout.EndHorizontal();

        // 撤销按钮
        if (GUILayout.Button("撤销"))
        {
            Undo();
        }
    }

    private void InitializeMap()
    {
        // 根据地图类型设置尺寸
        switch (selectedMapType)
        {
            case 0: // 九宫格类型
                mapWidth = 3;
                mapHeight = 3;
                break;
            case 1: // 前中后长方形
                mapWidth = 3;
                mapHeight = 1;
                break;
            case 2: // 单个正方形
                mapWidth = 1;
                mapHeight = 1;
                break;
        }

        // 删除旧地图
        if (mapParent != null)
        {
            DestroyImmediate(mapParent);
        }

        // 创建地图父物体
        mapParent = new GameObject("Map");
        map = new int[mapWidth, mapHeight];

        // 生成地图
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cell.transform.position = new Vector3(x * cellSize, y * cellSize, 0);
                cell.transform.parent = mapParent.transform;

                // 设置初始状态
                map[x, y] = 1; // 初始化为可行走区域
                UpdateCellMaterial(cell, map[x, y]);
            }
        }
    }

    private void UpdateCellMaterial(GameObject cell, int state)
    {
        Renderer renderer = cell.GetComponent<Renderer>();
        renderer.material = selectedMaterial != null ? selectedMaterial : defaultMaterial;
    }

    private void SaveMap()
    {
        if (!Directory.Exists(mapDataFolder))
        {
            Directory.CreateDirectory(mapDataFolder);
        }

        string filePath = Path.Combine(mapDataFolder, "map.txt");
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                sb.Append(map[x, y] + " ");
            }
            sb.AppendLine();
        }

        File.WriteAllText(filePath, sb.ToString());
        AssetDatabase.Refresh();
        Debug.Log("地图已保存: " + filePath);
    }

    private void LoadMap()
    {
        string filePath = Path.Combine(mapDataFolder, "map.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int y = 0; y < mapHeight; y++)
            {
                string[] values = lines[y].Trim().Split(' ');
                for (int x = 0; x < mapWidth; x++)
                {
                    map[x, y] = int.Parse(values[x]);
                }
            }
            Debug.Log("地图已加载: " + filePath);
            GenerateMapInScene(); // 重新生成地图
        }
        else
        {
            Debug.LogWarning("地图文件不存在: " + filePath);
        }
    }

    private void GenerateMapInScene()
    {
        // 删除旧地图
        if (mapParent != null)
        {
            DestroyImmediate(mapParent);
        }

        // 创建地图父物体
        mapParent = new GameObject("Map");

        // 生成地图
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Plane);
                cell.transform.position = new Vector3(x * cellSize, 1, y * cellSize);
                cell.transform.parent = mapParent.transform;

                // 应用材质
                UpdateCellMaterial(cell, map[x, y]);
            }
        }
    }

    private void Undo()
    {
        if (undoStack.Count > 0)
        {
            Action undoAction = undoStack.Pop();
            undoAction.Invoke();
        }
        else
        {
            Debug.LogWarning("没有可撤销的操作");
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        // 处理场景视图中的点击事件
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0 && isEditMaterialMode)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject cell = hit.collider.gameObject;
                int x = Mathf.FloorToInt(cell.transform.position.x / cellSize);
                int y = Mathf.FloorToInt(cell.transform.position.y / cellSize);

                // 记录当前材质
                Material previousMaterial = cell.GetComponent<MeshRenderer>().material;
                undoStack.Push(() =>
                {
                    cell.GetComponent<Renderer>().material = previousMaterial; // 撤销操作
                });

                // 应用选中的材质
                cell.GetComponent<Renderer>().material = selectedMaterial;

                e.Use(); // 标记事件已处理
            }
        }
    }

    private void OnFocus()
    {
        // 注册场景视图的回调
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDestroy()
    {
        // 取消注册场景视图的回调
        SceneView.duringSceneGui -= OnSceneGUI;

        // 删除地图父物体
        if (mapParent != null)
        {
            DestroyImmediate(mapParent);
        }
    }

    private void LoadMaterials()
    {
        // 加载默认材质
        defaultMaterial = AssetDatabase.LoadAssetAtPath<Material>(Path.Combine(materialFolder, "Default.mat"));

        // 加载其他材质
        selectedMaterial = defaultMaterial; // 默认使用默认材质
    }
}