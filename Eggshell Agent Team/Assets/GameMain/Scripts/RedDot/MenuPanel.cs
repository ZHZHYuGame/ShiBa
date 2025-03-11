using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RedpointSystem;

public class MenuPanel : MonoBehaviour
{
    public GameObject top_upBtn;
    public TopPanel LevelPanel;

    void Start()
    {
        top_upBtn.GetComponent<Button>().onClick.AddListener(OnPlay);
        InitRedPointState();//初始化红点状态
    }

    void OnPlay()
    {
        this.gameObject.SetActive(false);
        GameMgr.GetInstance().UIManager_Root.Push(new TopPanel());
        //LevelPanel.gameObject.SetActive(true);
    }

    void InitRedPointState()
    {
        int redNum = RedPointSystem.Instance.GetRedpointNum(RedPointKey.Play);//获取红点状态
        RefreshRedPointState(redNum);
        RedPointSystem.Instance.SetCallBack(RedPointKey.Play, RefreshRedPointState);
    }

    void RefreshRedPointState(int redNum)
    {
        //红点数量是0就隐藏  否则就显示
        Transform redPoint = top_upBtn.transform.Find("RedPoint");
        Transform redNumText = redPoint.transform.Find("Num");
        if (redNum <= 0)
        {
            redPoint.gameObject.SetActive(false);
        }
        else
        {
            redPoint.gameObject.SetActive(true);
            redNumText.GetComponent<Text>().text = redNum.ToString();
        }
    }
}