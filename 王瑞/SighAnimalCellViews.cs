using ConfigTools;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SighAnimalCellViews : EnhancedScrollerCellView
{
    [SerializeField]
    SighCellView[] cellView;
    public void SetData(ref List<SighData> scrollerDatas, int statIndex)
    {
        for (int i = 0; i < cellView.Length; i++)
        {
            //Êý¾Ý´«µÝ
            cellView[i].SetData(statIndex + i < scrollerDatas.Count ? scrollerDatas[statIndex + i] : null);
        }
    }
}
