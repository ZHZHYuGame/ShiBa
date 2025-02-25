using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField]
    Text test;
    [SerializeField]
    Button login;
    [SerializeField]
    Image icon;

    ScrollerData itemData;
    public void SetData(ScrollerData scrollerData)
    {
        test.text = scrollerData.scrollname;
        if (scrollerData==null)
        {
            return;
        }
        itemData = scrollerData;
    }
}
