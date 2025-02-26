using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class WorldEditor : EditorWindow
{
    int[,] map;
    int mapWidth = 20;
    int mapHeight = 20;
    float zoomLevel = 1.0f;

    Stack<Action> undoStack = new Stack<Action>();

    int selectedToolIndex = 0;
    string[] toolName = new string[] { "地图编辑", "种怪编辑" };

    int selectMonsterIndex = 0;
    string[] monsterName = new string[] { };

    Texture2D mapTexture;
    Vector2 scrollPostion;

    [MenuItem("Tools/自定义编辑器")]
    static public void Init()
    {
        GetWindow<WorldEditor>("自定义编辑器").Show();
    }
    private void OnEnable()
    {
        // 加载地图图片
        mapTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/UI/Texture2D/look.png");
        if (mapTexture == null)
        {
            Debug.LogWarning("地图图片未找到，请确保路径正确：Assets / UI / Texture2D / look.png");
        }

        // 初始化地图
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
        if (map == null)
        {
            map = new int[mapWidth, mapHeight];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    map[x, y] = 1; 
                }
            }
        }
    }


    private void DrawMonsterEditor()
    {
        
    }

    private void DrawMapEditor()
    {
        GUILayout.Label("地图编辑",EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("缩放:");
        zoomLevel = GUILayout.HorizontalSlider(zoomLevel, 0.5f, 2.0f);
        GUILayout.EndHorizontal();

        int buttonSize = (int)(20 * zoomLevel);
        float mapDisplayWidth = mapWidth * buttonSize;
        float mapDisplayHeight = mapHeight * buttonSize;
        scrollPostion = GUILayout.BeginScrollView(scrollPostion, GUILayout.Width(1000), GUILayout.Height(1000));

        for (int y = 0; y < mapHeight; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < mapWidth; x++)
            {
                GUI.backgroundColor = map[x, y] == 1 ? Color.green : Color.red;
                if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
                {
                    int currState = map[x, y];
                    undoStack.Push(() =>
                    {
                        if(x>=0&&x<mapWidth&&y>=0&&y<mapHeight)
                        {
                            map[x, y] = currState;
                        }
                        else
                        {
                            Debug.LogWarning($"无效的地图坐标({x},{y})");
                        }
                    });
                    map[x, y] = map[x, y] == 1 ? 0 : 1;
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();
        GUI.backgroundColor= Color.white;
        if(GUILayout.Button("保存地图",GUILayout.Width(120)))
        {
            SaveMap("Assets/Maps/map.txt");
        }
        if(GUILayout.Button("加载地图", GUILayout.Width(120)))
        {
            LoadMap("Assets/Maps/map.txt");
        }
        if (GUILayout.Button("撤销", GUILayout.Width(120)))
        {
            UnDo();
        }
        GUILayout.EndHorizontal();
    }

    private void UnDo()
    {
        if(undoStack.Count > 0)
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
        if(File.Exists(filePath))
        {
            string[] lines=File.ReadAllLines(filePath);
            for (int x = 0; x < mapWidth; x++)
            {
                string[] values = lines[x].Trim().Split(' ');
                for (int y = 0; y < mapHeight; y++)
                {
                    map[x, y] = int.Parse(values[x]);
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
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                mapData.Append(map[x, y] + " ");
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
