using ConfigTools;
using EnhancedUI.EnhancedScroller;
using StarForce;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class Shuxinglie
{
    public int id;
    public bool flag;
}
public class EveryDayForm : UGuiForm, IEnhancedScrollerDelegate
{
    // Start is called before the first frame update
    public EnhancedScroller myScroller;
    public QianCellView qianCellView;
    public Text yijian;
    List<int> xin  = new List<int>();
    //List<bool> boollist  = new List<bool>();
    [SerializeField]
    Slider sil;
    List<Shuxinglie> shuxinglie = new List<Shuxinglie>();
    [SerializeField]
    Sprite spriteimage;
    float[] ProcessJindu = new float[] { 0.06f, 0.3f, 0.53f, 0.76f, 1.0f };
    List<int> dayProcess = new List<int>() { 2, 5, 10, 15, 26 };
    [SerializeField]
    List<Image> image=new List<Image>();
    protected override void OnInit(object userData)
    {
      
        MessageCenter<List<bool>>.Instance.AddListener(Define.GaiQiandao,Qian);
        MessageCenter<int>.Instance.AddListener(Define.jindu, RefreshProcess);

        base.OnInit(userData);
        yijian.text = "一键补签";
        Debug.Log("签到数量"+NewTest.instance.sum);
        for (int i = 0; i < NewTest.instance.sum; i++)
        {
            Shuxinglie sx = new Shuxinglie();
            sx.id = i+1;
            sx.flag = NewTest.instance.meibool[i];
            shuxinglie.Add(sx);
           
        }
        myScroller.ReloadData();     //加载数据到Item

        myScroller.Delegate = this;   //必须有这个
        

    }

    private void RefreshProcess(int obj)
    {
       // Debug.Log("刷新进度");
        int proce = -1;
        for (int i = 0; i < dayProcess.Count; i++)
        {
            int index = i;
            if (NewTest.instance.zongDay >= dayProcess[index])
            {
                if (NewTest.instance.zongDay >= 26)
                {
                    sil.value = 1;
                }
                proce++;
            }
            else if (NewTest.instance.zongDay < 2)
            {
                proce = 0;
            }
        }
        //Debug.Log("变档几次:" + proce);
        if (NewTest.instance.zongDay >= 26)
        {
            sil.value = 1;
            //Debug.Log("大于26天的情况");
        }
        else
        {
            if (dayProcess.Contains(NewTest.instance.zongDay))
            {
                //Debug.Log("天数之中");
                sil.value = ProcessJindu[proce];
            }
            else
            {
                //Debug.Log("计算多余");
                if (NewTest.instance.zongDay < 2)
                {
                    //Debug.Log("小于2d的情况");
                    int another_day1 = NewTest.instance.zongDay - 0;//多余的天数
                    sil.value = (another_day1 * 1.0f / (dayProcess[0] - 0)) * (0.06f - 0);
                    //Debug.Log("进度:" + (another_day1 * 1.0f / (dayProcess[0] - 0)) * (0.06f - 0));
                }
                else
                {
                    //Debug.Log("大于2的情况");
                    int another_day = NewTest.instance.zongDay - dayProcess[proce];//多余的天数
                                                                                     //           0.06f                    1.0f          /      (5-2)*(0.3f-0.06f)
                    sil.value = ProcessJindu[proce] + another_day * 1.0f / (dayProcess[proce + 1] - dayProcess[proce]) * (ProcessJindu[proce + 1] - ProcessJindu[proce]);
                    //Debug.Log("进度:" + ProcessJindu[proce] + another_day * 1.0f / (dayProcess[proce + 1] - dayProcess[proce]) * (ProcessJindu[proce + 1] - ProcessJindu[proce]));
                }

            }
        }
      
        for (int i = 0; i < dayProcess.Count; i++)
        {
            int index = i;
            
            if (sil.value>= ProcessJindu[index])
            {
              
                image[index].sprite = spriteimage;
            }
        }
    }
    public void YiJan()
    {
        CSWelfareSignInReward cSWelfareSignInReward = new CSWelfareSignInReward();
        cSWelfareSignInReward.request_type = 1;
        cSWelfareSignInReward.part = 0;
        cSWelfareSignInReward.is_quick_sign = 1;
        NewTest.instance.m_Channel.Send(cSWelfareSignInReward);

    }
    public void Qian(List<bool>isbool)
    {
        shuxinglie.Clear();
        for (int i = 0; i < NewTest.instance.sum; i++)
        {
            Shuxinglie sx = new Shuxinglie();
            sx.id = i + 1;
            sx.flag = NewTest.instance.meibool[i];
            shuxinglie.Add(sx);
        }
        myScroller.ReloadData();     //加载数据到Item
    }
    public void Guan()
    {
        GameEntry.UI.CloseAllLoadedUIForms();
        GameEntry.UI.OpenUIForm(UIFormId.MainForm, this);
    }
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        myScroller.ReloadData();
        myScroller.Delegate = this;   //必须有这个
        MessageCenter<int>.Instance.BroadCast(Define.jindu, 0);

    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {

        QianCellView cellView = scroller.GetCellView(qianCellView) as QianCellView;
        cellView.name = dataIndex.ToString();
        cellView.SetData(ref shuxinglie, dataIndex * linesize);
        return cellView;

    }


    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 150.0f;
    }
    int linesize = 5;
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt(shuxinglie.Count / (float)linesize);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
}
