using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ETC : MonoBehaviour, IDragHandler, IEndDragHandler
{
    int r = 50;
    Vector3 dir;
    float dis;
    Vector3 startpos;
    RectTransform rect;

    public void OnDrag(PointerEventData eventData)
    {
        dis = Vector3.Distance(Input.mousePosition, startpos);
        if (dis > r)
        {
            dir = Input.mousePosition - startpos;
            transform.position = dir.normalized * r + startpos;
        }
        else
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startpos;
    }

    public float GetDis(string n)
    {
        if (n == "h")
        {
            return rect.anchoredPosition.x / r;
        }
        else if (n == "v")
        {
            return rect.anchoredPosition.y / r;
        }
        return 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        rect = transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
