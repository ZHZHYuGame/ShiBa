using ConfigTools;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QianCellView : EnhancedScrollerCellView
{
    [SerializeField]
    DayItme[] cellView;

    public void SetData(ref List<Shuxinglie> scrollerData, int startIndex)  //设置Prefab UI的数据，例如Text、Image
    {

        Debug.Log(scrollerData.Count);
        for (int i = 0; i < cellView.Length; i++)
        {
            cellView[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < cellView.Length; i++)
        {
            cellView[i].Init(startIndex+i < scrollerData.Count ? scrollerData[startIndex+i] :new Shuxinglie());
        }
    }
}
