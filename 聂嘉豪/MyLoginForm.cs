using GameFramework;
using GameFramework.Network;
using StarForce;
using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class MyLoginForm : UGuiForm
{

    [SerializeField]
    Image img;
    [SerializeField]
    Text text;
    [SerializeField]
    Button btn_login, btn_create;
    [SerializeField]
    InputField inp;

    string str;

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    //private GameFramework.Network.INetworkChannel m_Channel;

    //private NetworkChannelHelper m_NetworkChannelHelper;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // 获取框架网络组件

        //NetworkComponent network = StarForce.GameEntry.Network;

        //// 创建频道

        //m_NetworkChannelHelper = new NetworkChannelHelper();
        //m_Channel = network.CreateNetworkChannel("testName", ServiceType.Tcp, m_NetworkChannelHelper);
       // m_Channel.Connect(IPAddress.Parse("127.0.0.1"), 10300);

        btn_login.onClick.AddListener(() =>
        {
            //   StarForce.GameEntry.UI.OpenUIForm(UIFormId.SelectRole, this);


            // CSLoginReq msg = ReferencePool.Acquire<CSLoginReq>();
            // msg.rand_1 = UnityEngine.Random.Range(1000000, 10000000);
            // msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            // msg.key = "";
            // msg.plat_name = "dev_123";
            // msg.rand_2 = UnityEngine.Random.Range(1000000, 10000000);
            // msg.plat_fcm = 0;
            // msg.plat_server_id = 1;
            //Test.ins.m_Channel.Send(msg);

            //CSRoleReq msg = ReferencePool.Acquire<CSRoleReq>();
            //msg.rand_1 = UnityEngine.Random.Range(1000000, 10000000);
            //msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            //msg.key = "";
            //msg.plat_name = "dev_123";
            //msg.rand_2 = UnityEngine.Random.Range(1000000, 10000000);
            //msg.plat_fcm = 0;
            //msg.role_id = 1;
            //Test.ins.m_Channel.Send(msg);

            CSLoginReq msg = ReferencePool.Acquire<CSLoginReq>();


            msg.rand_1 = UnityEngine.Random.Range(1000000, 10000000);
            msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            msg.key = "";
            msg.plat_name = "dev_123";
            msg.rand_2 = UnityEngine.Random.Range(1000000, 10000000);
            msg.plat_fcm = 0;
            msg.plat_server_id = 1;
            Test.ins.m_Channel.Send(msg);
        });
        btn_create.onClick.AddListener(() =>
        {
            StarForce.GameEntry.UI.OpenUIForm(UIFormId.CreateRole, this);

            //CSCreateRoleReq msg = ReferencePool.Acquire<CSCreateRoleReq>();

            //msg.plat_name = inp.text;
            //msg.role_name = inp.text;
            //msg.login_time = 0;
            //msg.key = "";
            //msg.plat_server_id = 0;
            //msg.plat_fcm = 0;
            //msg.avatar = 0;
            //msg.sex = 0;
            //msg.prof = 0;
            //msg.camp_type = 0;
            //msg.plat_spid = "";
            //m_Channel.Send(msg);
        });

    
  
}

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    private void OnGUI()
    {
    //    if (GUI.Button(new Rect(100, 100, 200, 40), "链接"))
    //    {
    //        // m_Channel.Connect(IPAddress.Parse("127.0.0.1"), 10300);
    //        //m_Channel.Connect(IPAddress.Parse("10.161.8.190"), 10300);
    //        m_Channel.Connect(IPAddress.Parse("127.0.0.1"), 10300);
    //    }
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
