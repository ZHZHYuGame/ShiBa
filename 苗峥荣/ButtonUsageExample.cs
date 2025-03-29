using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 模拟Button类
public class SimpleButton : MonoBehaviour
{
    public UnityEvent onClick;

    private void OnMouseDown()
    {
        if (onClick != null)
        {
            onClick.Invoke();
        }
    }
}

// 使用示例
public class ButtonUsageExample : MonoBehaviour
{
    public SimpleButton simpleButton;

    private void Start()
    {
        simpleButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}
