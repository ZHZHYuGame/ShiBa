using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;


public class WorldEditor : EditorWindow
{
    enum MapType
    {
        World,
        run,
        small
    }

    Dictionary<int, Dictionary<int, int>> map; // 使用嵌套字典存储地图数据
    int mapWidth = 1;
    int mapHeight = 1;
    float zoomLevel = 1.0f;

    Stack<Action> undoStack = new Stack<Action>();

    private GameObject mapParent; // 地图对象的父物体

    int selectedToolIndex = 0;
    string[] toolName = new string[] { "地图编辑", "种怪编辑" };

    int selectMonsterIndex = 0;
    string[] monsterName = new string[] { };

    Sprite mapIcon;
    Vector2 scrollPosition;


    [MenuItem("Tools/自定义编辑器")]
    static public void Init()
    {
        GetWindow<WorldEditor>("自定义编辑器").Show();
    }

    private void OnEnable()
    {
        map = new Dictionary<int, Dictionary<int, int>>();
        InitializeMap();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        DrawSidebar();
        GUILayout.Box("", GUILayout.Width(1), GUILayout.ExpandHeight(true));
        DrawToolPanel();
        GUILayout.EndHorizontal();
    }

    private void DrawToolPanel()
    {
        GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        switch (selectedToolIndex)
        {
            case 0:
                DrawMapEditor();
                break;
            case 1:
                DrawMonsterEditor();
                break;
            default:
                break;
        }
        GUILayout.EndVertical();
    }

    private void InitializeMap()
    {
        // 动态补充地图内容
        for (int x = 0; x < mapWidth; x++)
        {
            if (!map.ContainsKey(x))
            {
                map[x] = new Dictionary<int, int>();
            }

            for (int y = 0; y < mapHeight; y++)
            {
                if (!map[x].ContainsKey(y))
                {
                    map[x][y] = 1; // 默认值为 1（可行走区域）
                }
            }
        }
    }

    private void DrawMonsterEditor()
    {
        // 种怪编辑功能（待实现）
    }

    private void DrawMapEditor()
    {
        GUILayout.Label("地图编辑", EditorStyles.boldLabel);

        // 缩放控制
        GUILayout.BeginHorizontal(GUILayout.Width(500));
        GUILayout.Label("缩放:");
        zoomLevel = GUILayout.HorizontalSlider(zoomLevel, 0.5f, 2.0f);
        GUILayout.EndHorizontal();

        // 地图尺寸设置
        GUILayout.BeginHorizontal(GUILayout.Width(500));
        GUILayout.Label("地图宽度:");
        int newMapWidth = EditorGUILayout.IntField(mapWidth);
        GUILayout.Label("地图高度:");
        int newMapHeight = EditorGUILayout.IntField(mapHeight);

        if (newMapWidth != mapWidth || newMapHeight != mapHeight)
        {
            mapWidth = newMapWidth;
            mapHeight = newMapHeight;
            InitializeMap(); // 动态更新地图尺寸
        }
        GUILayout.EndHorizontal();

        // 地图图片选择
        mapIcon = (Sprite)EditorGUILayout.ObjectField(mapIcon, typeof(Sprite), false, GUILayout.Width(100), GUILayout.Height(100));

        // 计算按钮大小
        int buttonSize = (int)(30 * zoomLevel);

        // 开始滚动视图
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(300), GUILayout.Height(300));

        // 绘制地图网格
        for (int y = 0; y < mapHeight; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < mapWidth; x++)
            {
                int cellState = map.ContainsKey(x) && map[x].ContainsKey(y) ? map[x][y] : 1; // 获取单元格状态

                GUI.backgroundColor = cellState == 1 ? Color.green : Color.red;
                if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
                {
                    int currState = cellState;
                    undoStack.Push(() =>
                    {
                        if (map.ContainsKey(x) && map[x].ContainsKey(y))
                        {
                            map[x][y] = currState; // 撤销操作
                        }
                    });
                    map[x][y] = cellState == 1 ? 0 : 1; // 切换状态
                }
            }
            GUILayout.EndHorizontal();
        }

        // 结束滚动视图
        GUILayout.EndScrollView();

        // 保存、加载和撤销按钮
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("保存地图", GUILayout.Width(120)))
        {
            SaveMap("Assets/GameMain/Maps/map.txt");
        }
        if (GUILayout.Button("加载地图", GUILayout.Width(120)))
        {
            LoadMap("Assets/GameMain/Maps/map.txt");
         
        }
        if (GUILayout.Button("撤销", GUILayout.Width(120)))
        {
            UnDo();
        }
        GUILayout.EndHorizontal();
    }

    private void UnDo()
    {
        if (undoStack.Count > 0)
        {
            Action unAction = undoStack.Pop();
            unAction.Invoke();
        }
        else
        {
            Debug.LogWarning("没有可撤销的操作");
        }
    }

    private void LoadMap(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            map.Clear();
            for (int y = 0; y < lines.Length; y++)
            {
                string[] values = lines[y].Trim().Split(' ');
                for (int x = 0; x < values.Length; x++)
                {
                    if (int.TryParse(values[x], out int cellState))
                    {
                        if (!map.ContainsKey(x))
                        {
                            map[x] = new Dictionary<int, int>();
                        }
                        map[x][y] = cellState;
                    }
                }
            }
            Debug.Log("地图已加载");
        }
        else
        {
            Debug.LogWarning("地图不存在");
        }
    }

    private void SaveMap(string filePath)
    {
        StringBuilder mapData = new StringBuilder();
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                int cellState = map.ContainsKey(x) && map[x].ContainsKey(y) ? map[x][y] : 1; // 获取单元格状态
                mapData.Append(cellState + " ");
            }
            mapData.AppendLine();
        }
        File.WriteAllText(filePath, mapData.ToString());
        AssetDatabase.Refresh();
        Debug.Log("地图已保存");
    }

    private void DrawSidebar()
    {
        GUILayout.BeginVertical(GUILayout.Width(120));

        GUILayout.Label("功能菜单", EditorStyles.boldLabel);

        for (int i = 0; i < toolName.Length; i++)
        {
            if (GUILayout.Button(toolName[i]))
            {
                selectedToolIndex = i;
            }
        }
        GUILayout.EndVertical();
    }
}