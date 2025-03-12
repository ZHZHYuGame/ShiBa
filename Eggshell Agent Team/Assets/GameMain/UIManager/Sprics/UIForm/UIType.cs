using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerType
{
    Top,          //----------------顶层）：这是最高层级
    Upper,        //----------------（上层）用于次要的弹出窗口、工具提示
    Normal,       //----------------（普通层）：这是大多数 UI 元素的默认层级
    Hud,          //----------------通常用于覆盖式元素，这些元素在不中断用户与主要内容交互的情况下提供信息。例如状态栏、进度指示器或其他信息覆盖层。
    Etc
}

public class UIType 
{
    private string path;
    private string name;
    private LayerType LayerType;

    public string Path { get => path; }
    public string Name { get => name; }
    public LayerType UILayerType { get => LayerType; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ui_path">对应Panel的路径</param>
    /// <param name="ui_name">对应Panel的名称</param>
    /// <param name="ui_layerType">对应Panel的层级</param>


    public UIType(string ui_path,string ui_name,LayerType ui_layerType)
    {
        path = ui_path;
        name = ui_name;
        LayerType = ui_layerType;
    }
}
