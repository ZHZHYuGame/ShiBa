using RedpointSystem;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : BasePanel
{
    public GameObject BackBtn;
    public MenuPanel menuPanel;
    private static string name = "TopPanel";
    private static string path = "Panel/TopPanel";
    private static LayerType layerType = LayerType.Normal;
    public static readonly UIType uIType = new UIType(path, name, layerType);
    public TopPanel() : base(uIType)
    {
    }

    void Start()
    {
       // Level1Container.SetActive(false);
        BackBtn.GetComponent<Button>().onClick.AddListener(OnBackClick);
        //Level1Btn.GetComponent<Button>().onClick.AddListener(OnLevel1Click);
        RedPointSystem.Instance.DeleteNode(RedPointKey.Play_LEVEL1_TOP);
        //InitRedPointState();
    }
    //关闭界面
    void OnBackClick()
    {
        //this.gameObject.SetActive(false);
        GameMgr.GetInstance().UIManager_Root.Pop(false);
        menuPanel.gameObject.SetActive(true);
    }

    void OnLevel1Click()
    {
        //Level1Container.gameObject.SetActive(!Level1Container.gameObject.activeSelf);
    }


    void OnLevel1HomeBtn()
    {
        RedPointSystem.Instance.DeleteNode(RedPointKey.Play_LEVEL1_TOP);
    }
   

    /*void InitRedPointState()
    {
        // Level1Btn
        RefreshRedPointState(
            RedPointSystem.Instance.GetRedpointNum(RedPointKey.Play_LEVEL1_TOP),
            Level1Btn.transform.Find("RedPoint")
        );
        RedPointSystem.Instance.SetCallBack(RedPointKey.Play_LEVEL1_TOP, (int redNum) =>
        {
            RefreshRedPointState(redNum, Level1Btn.transform.Find("RedPoint"));
        });

       
    }*/

    void RefreshRedPointState(int redNum, Transform redPoint)
    {
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