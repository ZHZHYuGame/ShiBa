using ConfigTools;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SighScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField]
    EnhancedScroller enhancedScroller;
    [SerializeField]
    SighAnimalCellViews animalCellViewPerfab;//预制体

    private void Start()
    {

        enhancedScroller.Delegate = this;
        enhancedScroller.ReloadData();//数据刷新

    }
    private void Update()
    {

    }
    int linesize = 5;
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        SighAnimalCellViews animalCellView = scroller.GetCellView(animalCellViewPerfab) as SighAnimalCellViews;
        //数据赋值
        animalCellView.SetData(ref SighModel.Instance.signInList, dataIndex * linesize);
        return animalCellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 160f;//大小
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt(SighModel.Instance.signInList.Count*1.0f/ linesize);//返回Item的数量。
    }
}
