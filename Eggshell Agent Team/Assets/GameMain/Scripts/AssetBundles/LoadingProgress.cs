using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    public Slider progressBar;//进度条Slider
    public Text progressText;//进度百分比文本
    public float progress;

    /// <summary>
    /// 更新进度条
    /// </summary>
    public void UpdateProgress(float progress)
    {
        progressBar.value = progress;
        progressText.text = $"{progress * 100:F0}%"; // 显示百分比
    }
   
}
