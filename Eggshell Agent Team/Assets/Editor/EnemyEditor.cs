using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyEditor : EditorWindow
{
    [MenuItem("StageTool/玩家编辑器")]
    public static void Init()
    {
        GetWindow<EnemyEditor>().Show();
    }
    private void OnGUI()
    {
        
    }
}
