using StarForce;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MainForm : UGuiForm
{
    // Start is called before the first frame update
    [SerializeField]
     List<Toggle> Toggle = new List<Toggle>();
    [SerializeField]
    Button button;
    [SerializeField]
    Text GoldText;
    [SerializeField]
    Text VitalityText;
    [SerializeField]
    Text DiamondText;
    [SerializeField]
    int Diamond;
    [SerializeField]
    int Gold;
    [SerializeField]
    int Vitality;
    [SerializeField]
    Text playname;
    [SerializeField]
    Text Buttontext;
    [SerializeField]
    Text nengText;
    [SerializeField]
    Text LevelText;
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        playname.text = "内向且紧张";
        Buttontext.text = "开始游戏";
        nengText.text = "×5";
        LevelText.text = "87";
        for (int i = 0; i < Toggle.Count; i++)
        {
            int index = i;
            Toggle[index].transform.GetChild(2).gameObject.SetActive(false);
            Toggle[index].onValueChanged.AddListener((X) =>
            {
                if (X == true)
                {
                    Toggle[index].transform.GetChild(1).transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    Toggle[index].transform.GetChild(2).gameObject.SetActive(true);

                }
                else
                {
                    Toggle[index].transform.GetChild(1).transform.localScale = new Vector3(1f, 1f, 1f);
                    Toggle[index].transform.GetChild(2).gameObject.SetActive(false);
                }
            });
        }
        Toggle[2].isOn = true;
        Toggle[2].transform.GetChild(1).transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Toggle[2].transform.GetChild(2).gameObject.SetActive(true);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        VitalityText.text = Vitality + "/" + 30;
        GoldText.text = Gold + "k";
        DiamondText.text = Diamond.ToString();
       
    }
}
