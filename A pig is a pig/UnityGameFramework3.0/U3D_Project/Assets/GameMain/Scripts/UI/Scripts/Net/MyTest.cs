using GameFramework;
using GameFramework.Network;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityGameFramework.Runtime;

public class MyTest : MonoBehaviour
{
    private GameFramework.Network.INetworkChannel m_Channel;

    private NetworkChannelHelper m_NetworkChannelHelper;
    // Start is called before the first frame update
    void Start()
    {
        // 获取框架网络组件

        NetworkComponent network = StarForce.GameEntry.Network;

        // 创建频道

        m_NetworkChannelHelper = new NetworkChannelHelper();

        m_Channel = network.CreateNetworkChannel("testName", ServiceType.Tcp, m_NetworkChannelHelper);



    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 200, 40), "连接"))
        {
            //10.161.23.13
            m_Channel.Connect(IPAddress.Parse("127.0.0.1"), 10300);
        }


        if (GUI.Button(new Rect(100, 200, 200, 40), "发送"))
        {
            CSLoginReq msg = ReferencePool.Acquire<CSLoginReq>();
            msg.rand_1 = UnityEngine.Random.Range(1000000, 10000000);
            msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            msg.key = "";
            msg.plat_name = "dev_123";
            msg.rand_2 = UnityEngine.Random.Range(1000000, 10000000);
            msg.plat_fcm = 0;
            msg.plat_server_id = 1;
            m_Channel.Send(msg);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
