using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

public class MapEditor : EditorWindow
{
    private int[,] map; // 地图数据
    private int mapWidth = 10; // 地图宽度
    private int mapHeight = 10; // 地图高度
    private float cellSize = 1.0f; // 每个单元格的大小
    private Stack<Action> undoStack = new Stack<Action>(); // 撤销栈

    private GameObject mapParent; // 地图对象的父物体
    private Material walkableMaterial; // 可行走区域的材质
    private Material unwalkableMaterial; // 不可行走区域的材质

    [MenuItem("Tools/3D 地图编辑器")]
    public static void ShowWindow()
    {
        GetWindow<MapEditor>("3D 地图编辑器");
    }

    private void OnEnable()
    {
        // 加载材质
        walkableMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Walkable.mat");
        unwalkableMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Unwalkable.mat");

        // 初始化地图
        InitializeMap();
    }

    private void OnGUI()
    {
        GUILayout.Label("3D 地图编辑器", EditorStyles.boldLabel);

        // 地图尺寸设置
        GUILayout.BeginHorizontal();
        GUILayout.Label("地图宽度:");
        mapWidth = EditorGUILayout.IntField(mapWidth);
        GUILayout.Label("地图高度:");
        mapHeight = EditorGUILayout.IntField(mapHeight);
        GUILayout.EndHorizontal();

        // 初始化地图按钮
        if (GUILayout.Button("初始化地图"))
        {
            InitializeMap();
        }

        // 保存和加载地图按钮
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("保存地图"))
        {
            SaveMap("Assets/map.txt");
        }
        if (GUILayout.Button("加载地图"))
        {
            LoadMap("Assets/map.txt");
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
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.transform.position = new Vector3(x * cellSize, 0, y * cellSize);
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
        if (state == 1)
        {
            renderer.material = walkableMaterial;
        }
        else
        {
            renderer.material = unwalkableMaterial;
        }
    }

    private void SaveMap(string filePath)
    {
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
        Debug.Log("地图已保存");
    }

    private void LoadMap(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int y = 0; y < mapHeight; y++)
            {
                string[] values = lines[y].Trim().Split(' ');
                for (int x = 0; x < mapWidth; x++)
                {
                    map[x, y] = int.Parse(values[x]);

                    // 更新单元格材质
                    GameObject cell = mapParent.transform.GetChild(x + y * mapWidth).gameObject;
                    UpdateCellMaterial(cell, map[x, y]);
                }
            }
            Debug.Log("地图已加载");
        }
        else
        {
            Debug.LogWarning("地图文件不存在");
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
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject cell = hit.collider.gameObject;
                int x = Mathf.FloorToInt(cell.transform.position.x / cellSize);
                int y = Mathf.FloorToInt(cell.transform.position.z / cellSize);

                int previousState = map[x, y];
                undoStack.Push(() =>
                {
                    map[x, y] = previousState;
                    UpdateCellMaterial(cell, map[x, y]);
                });

                map[x, y] = map[x, y] == 1 ? 0 : 1; // 切换状态
                UpdateCellMaterial(cell, map[x, y]);

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
        //
        if(mapParent!=null)
        {
            DestroyImmediate(mapParent);
        }
    }
}