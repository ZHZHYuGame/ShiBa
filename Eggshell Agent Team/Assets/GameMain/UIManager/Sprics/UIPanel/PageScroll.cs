using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PageScroll : MonoBehaviour
{
    public ScrollRect scrollRect;   // 绑定的ScrollRect组件
    public float pageStep = 0.5f;     // 每页的步长（0~1）
    public float smoothTime = 0.5f; // 滑动动画时间
    public GridLayoutGroup contentLayout;
    private bool isDragging = false;
    private float targetPosition;
    public bool isting=false;
    public float shijian;
    void Start()
    {
        // 监听拖动事件
        scrollRect.onValueChanged.AddListener(OnScroll);
        scrollRect.horizontalNormalizedPosition = 0;
        targetPosition = scrollRect.horizontalNormalizedPosition;
        Debug.Log(targetPosition);
        int childCount = contentLayout.transform.childCount;
        pageStep = 1f / (childCount - 1);
        Debug.Log(pageStep);
        SnapToNearestPage();
    }

    void OnScroll(Vector2 pos)
    {
        if (isDragging)
        {
            // 拖动时实时更新目标位置
            targetPosition = scrollRect.horizontalNormalizedPosition;
            Debug.Log(targetPosition);
        }
    }

    void Update()
    {
        // 检测鼠标/触摸释放
        if(isting)
        {
            shijian+=Time.deltaTime;
            if(shijian<0.5f)
             {
                SnapToNearestPage();
             }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isting=true;
           
            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isDragging = false;
            isting=false;
            shijian=0;
        }
        if (!isDragging && scrollRect.velocity.magnitude < 50f)
        {
           if(shijian<0.5f)
             {
             SnapToNearestPage();
             }
            
        }
        // 平滑移动到目标位置
        scrollRect.horizontalNormalizedPosition = 
        Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetPosition, Time.deltaTime / smoothTime);
    }
public void NextPage()
{
    targetPosition = Mathf.Clamp01(targetPosition + pageStep);
}

public void PreviousPage()
{
    targetPosition = Mathf.Clamp01(targetPosition - pageStep);
}
    // 吸附到最近的页
    private void SnapToNearestPage()
    {
        float currentPos = scrollRect.horizontalNormalizedPosition;
        targetPosition = Mathf.Round(currentPos / pageStep) * pageStep;
        targetPosition = Mathf.Clamp01(targetPosition); // 限制在0~1之间
        switch(targetPosition)
        {
             case 0:
             MainPanel.index=0;
             break;
             case 0.5f:
             MainPanel.index=1;
             break;
             case 1f:
             MainPanel.index=2;
             break;
        }
    }
}