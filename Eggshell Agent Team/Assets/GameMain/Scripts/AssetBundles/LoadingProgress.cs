using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgress : BasePanel
{
    public static Slider progressBar;//进度条Slider
    public Text progressText;//进度百分比文本
    public static float rotationSpeed = 100f;
    public static Image image;
    public float progress;
    private static string name = "LoadPanel";
    private static string path = "Panel/LoadPanel";
    private static LayerType layerType = LayerType.Normal;

    public static readonly UIType uIType = new UIType(path, name, layerType);

    public LoadingProgress() : base(uIType)
    {
    }
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log(2);
        progressBar = UIMethod.Ins.GetOrAddSingleComponentInChild<Slider>(ActiveObj, "Slider");
        image = UIMethod.Ins.GetOrAddSingleComponentInChild<Image>(ActiveObj, "Image");
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    public static void Update()
    {
        // 每帧绕 Y 轴旋转
        image.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    /// <summary>
    /// 更新进度条
    /// </summary>
    public static void UpdateProgress(float progress)
    {
        Debug.Log(1);
        progressBar.value = progress;
       // progressText.text = $"{progress * 100:F0}%"; // 显示百分比
        if (progressBar.value >= 1)
        {
            GameMgr.GetInstance().UIManager_Root.Pop(false);
            GameMgr.GetInstance().UIManager_Root.Push(new MainPanel());
        }
    }

}
