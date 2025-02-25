using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityGameFramework.Runtime;
public class Test : MonoBehaviour
{
    public GameFramework.Network.INetworkChannel m_Channel;

    public NetworkChannelHelper m_NetworkChannelHelper;
    internal string play_name= "dev_127";
    internal string role_name= "白年4";
    internal int sceneId;
    SCLoginAck sCLoginAck;
    public static Test ins;
    string ip="127.0.0.1";
    int port = 10300;
    internal List<Role> roleList=new List<Role>();

    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
		// 获取框架网络组件
		NetworkComponent network = StarForce.GameEntry.Network;
        // 创建频道
        m_NetworkChannelHelper = new NetworkChannelHelper();
        m_Channel = network.CreateNetworkChannel("testName", ServiceType.Tcp, m_NetworkChannelHelper);
        ConnectToServer(port,SocketEnum.Login);
        StarForce.GameEntry.Event.Subscribe(SocketConnectEventArgs.EventId, SocketConnectSuccess);
    }
    public void SocketConnectSuccess(object sender, GameEventArgs e)
    {
        SocketConnectEventArgs param = e as SocketConnectEventArgs;
        if (param.socketEnum == SocketEnum.Login)
        {
            CSRoleReq msg = ReferencePool.Acquire<CSRoleReq>();
            msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            msg.key = "";
            msg.plat_name = Test.ins.play_name;
            msg.plat_fcm = 0;
            msg.plat_server_id = 1;
            msg.role_id = 1;
            m_Channel.Send(msg);
        }
        else if (param.socketEnum == SocketEnum.GameServer)
        {
            ColorLog.LogRed("你好世界"); 
            CSUserEnterGSReq msg = ReferencePool.Acquire<CSUserEnterGSReq>();
            msg.scene_id = sCLoginAck.scene_id;
            msg.scene_key = 0;
            msg.last_scene_id = sCLoginAck.last_scene_id;
            msg.role_id = sCLoginAck.role_id;
            msg.role_name = role_name;
            msg.time = (int)sCLoginAck.time;
            msg.is_login = 1;
            msg.server_id = 1;
            msg.key = sCLoginAck.key;
            msg.plat_name = play_name;
            msg.is_micro_pc = 0;
            msg.plat_spid = "dev";
            Test.ins.m_Channel.Send(msg);
        }
    }

    public void ConnectToServer(int port, SocketEnum socketEnum)
    {
        if (m_Channel != null)
        {
            m_NetworkChannelHelper.SetServerType(socketEnum);
            m_Channel.Connect(IPAddress.Parse(ip), port);
        }
    }
    public void DestoryChannel(string channelName,int port, SCLoginAck packetImpl)
    {
        NetworkComponent network = StarForce.GameEntry.Network;
        bool flag = network.DestroyNetworkChannel(channelName);
        sCLoginAck = packetImpl;
        sceneId = sCLoginAck.scene_id;
        if (flag)
        {
            m_Channel = null;
            m_NetworkChannelHelper = null;
            m_NetworkChannelHelper = new NetworkChannelHelper();

            m_Channel = network.CreateNetworkChannel("GameServer", ServiceType.Tcp, m_NetworkChannelHelper);
            ConnectToServer(port,SocketEnum.GameServer);
        }
        // 创建频道
    }
    
}
