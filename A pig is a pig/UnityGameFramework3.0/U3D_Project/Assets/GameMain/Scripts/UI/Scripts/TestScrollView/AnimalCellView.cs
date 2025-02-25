using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCellView :  EnhancedScrollerCellView
{
    [SerializeField]
    CellView[] cellView;

    public void SetData(ref List<ScrollerData> scrollerDatas,int startIndex)
    {
        for (int i = 0; i < cellView.Length; i++)
        {
            cellView[i].SetData(startIndex + i < scrollerDatas.Count ? scrollerDatas[startIndex + i] : null);
        }
    }
}
