using UnityEngine;

public static class ColorLog
{
    public static void Log(string logText , string colorStr)
    {
        Debug.Log($"<color={colorStr}>{logText}</color>");
    }

    public static void Log(string logText , Color color)
    {
        var r = Mathf.RoundToInt(color.r * 255);
        var g = Mathf.RoundToInt(color.g * 255);
        var b = Mathf.RoundToInt(color.b * 255);

        // 将整数值转换为 16 进制字符串并格式化为两位数
        Log(logText, $"#{r:X2}{g:X2}{b:X2}");
    }

    public static void LogCyan(string logText)
    {
        Log(logText,"cyan");
    }
    
    public static void LogRed(string logText)
    {
        Log(logText,"red");
    }
    
    public static void LogYellow(string logText)
    {
        Log(logText,"yellow");
    }
    
    public static void LogOrange(string logText)
    {
        Log(logText,"orange");
    }
}