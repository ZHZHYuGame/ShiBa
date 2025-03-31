using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayItme : MonoBehaviour
{
    // Start is called before the first frame update
    public Image dangqian;
    public Image buqian;
    public Button btn;
    public int today;
    public bool flag;
    public Image yiqian;
    public Shuxinglie shuxinglie;
    public void Init(Shuxinglie shu)
    {
        shuxinglie = shu;
        
        dangqian.gameObject.SetActive(false);
        buqian.gameObject.SetActive(false);
        yiqian.gameObject.SetActive(false);
       
        if (shu.id ==0 )
        {
            gameObject.SetActive(false);
        }
        if (shuxinglie.id == NewTest.instance.Day)
        {
            if (shuxinglie.flag == true)
            {
                yiqian.gameObject.SetActive(true);
            }
            else
            {
                dangqian.gameObject.SetActive(true);
            }
         
        }
        if (shuxinglie.id < NewTest.instance.Day)
        {
            if (shuxinglie.flag == false)
            {
                buqian.gameObject.SetActive(true);
            }
            else
            {
                yiqian.gameObject.SetActive(true);
            }
        }

    }

   

    void Start()
    {
        btn=GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            if (shuxinglie.id < NewTest.instance.Day&& shuxinglie.flag==false)
            {
                CSWelfareSignInReward cSWelfareSignInReward = new CSWelfareSignInReward();
                cSWelfareSignInReward.request_type = 1;
                cSWelfareSignInReward.part = (short)shuxinglie.id;
                cSWelfareSignInReward.is_quick_sign = 0;
                NewTest.instance.m_Channel.Send(cSWelfareSignInReward);
            }
            else if(shuxinglie.id == NewTest.instance.Day)
            {
                CSWelfareSignInReward cSWelfareSignInReward = new CSWelfareSignInReward();
                cSWelfareSignInReward.request_type = 1;
                cSWelfareSignInReward.part = (short)shuxinglie.id;
                cSWelfareSignInReward.is_quick_sign = 0;
                NewTest.instance.m_Channel.Send(cSWelfareSignInReward);
            }
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
