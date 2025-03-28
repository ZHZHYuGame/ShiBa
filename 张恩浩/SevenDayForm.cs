using ConfigTools;
using GameFramework;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SevenDayForm : UGuiForm
{
    [SerializeField] private Transform content;
    List<Toggle> dayPerfabs;//预制件集合
    [SerializeField] public Button cloBtn;
    [SerializeField] public Image image1,image2,image3,image4,image5;
    [SerializeField] public Button btn;
    [SerializeField] private Text text;
    [SerializeField] private Text istBtn,noBtn;
    List<bool> isSevenDay = new List<bool>();

    int indexs=0;
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        //获取签到数据
        
        
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        isSevenDay = userData as List<bool>;
        Debug.Log(isSevenDay.Count);
        //按钮赋值
        text.text = "领取奖励";
        noBtn.text = "已领取";
        dayPerfabs = new List<Toggle>();
        int j = 0;
        //读表加载
        foreach (var item in DataMgr.Instance.signData)
        {
            
            GameObject perfab = Instantiate(Resources.Load<GameObject>("day"),content);
            perfab.GetComponent<SevenDayItem>().Init(item.Value, isSevenDay[j]);
            perfab.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
            dayPerfabs.Add(perfab.GetComponent<Toggle>());//添加到集合
            j++;
        }

        //关闭按钮
        cloBtn.onClick.AddListener(() =>
        {
            GameEntry.UI.CloseUIForm(this);
        });
        //toggle组
        for (int i = 0; i < dayPerfabs.Count; i++)
        {
            int index = i;
            
            dayPerfabs[index].onValueChanged.AddListener((ist) =>
            {
               

                if (ist)
                {
                    //签过到
                    if (isSevenDay[index]) { btn.interactable = false; noBtn.gameObject.SetActive(true); istBtn.gameObject.SetActive(false); }
                    else { btn.interactable = true; noBtn.gameObject.SetActive(false); istBtn.gameObject.SetActive(true); }
                    indexs = index;
                    //获取数据 签到数据
                    Sign sign = DataMgr.Instance.signData[index + 1];
                    //获取礼包
                    gift1 gift1 = DataMgr.Instance.gift1Data[int.Parse(sign.reward_item)];

                    image1.sprite = Resources.Load<Sprite>("Item/Item_" + DataMgrTool.Instance.IsBconsumeOrZconsume(gift1.item_1_id));
                    image2.sprite = Resources.Load<Sprite>("Item/Item_" + DataMgrTool.Instance.IsBconsumeOrZconsume(gift1.item_2_id));
                    image3.sprite = Resources.Load<Sprite>("Item/Item_" + DataMgrTool.Instance.IsBconsumeOrZconsume(gift1.item_3_id));
                    image4.sprite = Resources.Load<Sprite>("Item/Item_" + DataMgrTool.Instance.IsBconsumeOrZconsume(gift1.item_4_id));
                    image5.sprite = Resources.Load<Sprite>("Item/Item_" + DataMgrTool.Instance.IsBconsumeOrZconsume(gift1.item_5_id));



                }
            });
        }
        //签到按钮
        btn.onClick.AddListener(() =>
        {
            //发签到消息
            CSFetchSevenDayLoginReward msg = ReferencePool.Acquire<CSFetchSevenDayLoginReward>();
            msg.fetch_day = indexs+1;
            NetWork.ins.m_Channel.Send(msg);
            //更新页面
        });
    }


    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
}
