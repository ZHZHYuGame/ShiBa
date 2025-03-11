using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedpointSystem;

public class RootPanel : MonoBehaviour
{
    public GameObject Canvas;
    public MenuPanel menuPanel;
    public TopPanel levelPanel;

    private void Awake()
    {
        // if(跨过每月最后一天的0点)..
        //RedPointSystem.Instance.AddNode(RedPointKey.Play_LEVEL1_HOME);

        // if(任务完成，可以领奖)...
        //RedPointSystem.Instance.AddNode(RedPointKey.Play_LEVEL1_SHOP);

        // if(...)
        //RedPointSystem.Instance.AddNode(RedPointKey.Play_LEVEL2_HOME);

        // if(...)
        //RedPointSystem.Instance.AddNode(RedPointKey.Play_LEVEL2_SHOP);
        //初始的时候直接让充值提示显示  红点直接显示出来
        RedPointSystem.Instance.AddNode(RedPointKey.Play_LEVEL1_TOP);
    }

    private void Start()
    {
        menuPanel.gameObject.SetActive(true);
        //levelPanel.gameObject.SetActive(false);
    }
}
