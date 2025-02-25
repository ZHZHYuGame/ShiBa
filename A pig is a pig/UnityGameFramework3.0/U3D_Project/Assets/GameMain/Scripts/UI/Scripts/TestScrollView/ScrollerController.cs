using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField]
    EnhancedScroller enhancedScroller;
    [SerializeField]
    AnimalCellView animalCellViewPrefab;

    List<ScrollerData> datas;

    // Start is called before the first frame update
    void Start()
    {
        datas = new List<ScrollerData>();
        for (int i = 0; i < 100; i++)
        {
            ScrollerData scrollerData = new ScrollerData();
            scrollerData.scrollname = "item" + i;
            datas.Add(scrollerData);
        }
        enhancedScroller.Delegate = this;
        enhancedScroller.ReloadData();
    }


    int linesize = 2;
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        AnimalCellView animalCellView = scroller.GetCellView(animalCellViewPrefab) as AnimalCellView;
        animalCellView.SetData(ref datas, dataIndex * linesize);
        return animalCellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100.0f;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt(datas.Count / (float)linesize);
    }
}