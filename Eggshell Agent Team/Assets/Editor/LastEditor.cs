using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LastEditor : EditorWindow
{
    
    [MenuItem("Tools/编辑器")]
    public void Init()
    {
        GetWindow<LastEditor>("编辑器");
    }
    string[] mapTypes = new string[] { "九宫格", "长方形", "正方形" };

    List<Material> materials;

    GameObject mapParent;
    Material selectedMaterial;
    Material defaultMaterial; 

    string mapDataFolder = "Assets/GameMain/Maps";
    string materialFolder = "Assets/GameMain/Materials"; 


    private void OnEnable()
    {
        LoadMaterials();
    }

    private void LoadMaterials()
    {
        materials = new List<Material>();

        defaultMaterial = AssetDatabase.LoadAssetAtPath<Material>(Path.Combine(materialFolder, "Default.mat"));

        selectedMaterial = defaultMaterial; 

    }

    private void OnGUI()
    {
        // 材质按钮
        GUILayout.BeginHorizontal();
        GUILayout.Label("材质选择", EditorStyles.boldLabel);
        string[] materialPaths = Directory.GetFiles(materialFolder, "*.mat");
        foreach (string path in materialPaths)
        {
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material != null)
            {
                if (GUILayout.Button(material.name,GUILayout.Width(100),GUILayout.Height(100)))
                {
                    selectedMaterial = material; // 切换选中的材质
                }
            }
        }
        GUILayout.EndHorizontal();


    }
}
