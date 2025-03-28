using ConfigTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SevenDayItem : MonoBehaviour
{
    [SerializeField] private Text day,names;
    [SerializeField] private Image icon,yesImage;

    internal void Init(Sign sign, bool v)
    {
        switch (sign.login_daycount)
        {
            case 1:
                day.text = "第一天";
                break;
            case 2:
                day.text = "第二天";
                break;
            case 3:
                day.text = "第三天";
                break;
            case 4:
                day.text = "第四天";
                break;
            case 5:
                day.text = "第五天";
                break;
            case 6:
                day.text = "第六天";
                break;
            case 7:
                day.text = "第七天";
                break;
        }
        names.text = sign.reward_text;
        if (v)
        {
            yesImage.gameObject.SetActive(true);
        }
        else
        {
            yesImage.gameObject.SetActive(false);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
