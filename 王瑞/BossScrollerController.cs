cuoyao using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BossScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField]
    EnhancedScroller enhancedScroller;
    [SerializeField]
    BossAnimalCellViews animalCellViewPerfab;//预制体
    List<BossData> datas;
    void Start()
    {
        datas = new List<BossData>();
        datas.Add(new BossData( "褒姒·国威",101,0,390, 1739761514));
        datas.Add(new BossData("钟无艳·国威", 102,0,420, 1739761514));
        datas.Add(new BossData("蒙恬·国威", 104,0,450, 1739761514));
        datas.Add(new BossData("落魄将军·国威", 105,0,480, 1739761514));
        datas.Add(new BossData("山大王·国威", 116,1,500, 1739803922));
        enhancedScroller.Delegate = this;
        enhancedScroller.ReloadData();//数据刷新  
    }

    
    void Update()
    {
        
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        BossAnimalCellViews animalCellView = scroller.GetCellView(animalCellViewPerfab) as BossAnimalCellViews;
        animalCellView.SetData(datas[dataIndex]);
        return animalCellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 160f;//大小
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return datas.Count;//返回Item的数量。
    }
    
}
