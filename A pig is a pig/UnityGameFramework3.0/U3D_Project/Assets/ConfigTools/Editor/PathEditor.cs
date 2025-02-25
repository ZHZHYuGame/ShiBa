using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ConfigToolPath", menuName = "Assets/ConfigToolPath")]
public class ConfigToolPath : ScriptableObject
{
    public string excelPath; 
    public string generateScriptPath;
    public string generateBytePath;
    public string zfcToolsPath;
}


[CustomEditor(typeof(ConfigToolPath))]
public class PathEditor : Editor
{
    
    // AssetData assetData = AssetDatabase.LoadAssetAtPath<AssetData>(assetDataPath);
    public override void OnInspectorGUI()
    {
        var toolsPath = (ConfigToolPath)target;

        using (new EditorGUILayout.HorizontalScope())
        {
            toolsPath.excelPath = EditorGUILayout.TextField("Excel文件路径", toolsPath.excelPath);
            if (GUILayout.Button("Path",GUILayout.Width(70)))
            {
                // 弹出选择路径的对话框
                var path = EditorUtility.OpenFolderPanel("选择路径", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    toolsPath.excelPath = path;
                }
            }
        }
        
        using (new EditorGUILayout.HorizontalScope())
        {
            toolsPath.generateScriptPath = EditorGUILayout.TextField("Script生成路径", toolsPath.generateScriptPath);
            if (GUILayout.Button("Path",GUILayout.Width(70)))
            {
                // 弹出选择路径的对话框
                var path = EditorUtility.OpenFolderPanel("选择路径", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    toolsPath.generateScriptPath = path;
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            toolsPath.generateBytePath = EditorGUILayout.TextField("byte文件导出路径", toolsPath.generateBytePath);
            if (GUILayout.Button("Path",GUILayout.Width(70)))
            {
                // 弹出选择路径的对话框
                var path = EditorUtility.OpenFolderPanel("选择路径", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    toolsPath.generateBytePath = path;
                }
            }
        }
        
        using (new EditorGUILayout.HorizontalScope())
        {
            toolsPath.zfcToolsPath = EditorGUILayout.TextField("zfc工具路径", toolsPath.zfcToolsPath);
            if (GUILayout.Button("Path",GUILayout.Width(70)))
            {
                // 弹出选择路径的对话框
                var path = EditorUtility.OpenFolderPanel("选择路径", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    toolsPath.zfcToolsPath = path;
                }
            }
        }


    }
}