//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Network;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class SCRoleListAckHandler : PacketHandlerBase
    {
        public override int Id
        {
            get
            {
                return 7001;
            }
        }

        public override void Handle(object sender, Packet packet)
        {
            SCRoleListAck packetImpl = (SCRoleListAck)packet;
            Log.Info("Receive packet '{0}'.", packetImpl.Id.ToString());
            Test.ins.roleList = packetImpl.role_list;
            if(packetImpl.result ==0 && packetImpl.count > 0)//有角色
			{
                Test.ins.role_name = packetImpl.role_list[0].role_name;
                CSRoleReq msg = ReferencePool.Acquire<CSRoleReq>();
                msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
                msg.key = "";
                msg.plat_name = Test.ins.play_name;
                msg.plat_fcm = 0;
                msg.plat_server_id = 1;
                msg.role_id = packetImpl.role_list[0].role_id;
                Test.ins.m_Channel.Send(msg);
                //GameEntry.UI.OpenUIForm(UIFormId.SelectRoleView,this);
            }
            else if(packetImpl.result == -6)//创角
			{
                GameEntry.UI.OpenUIForm(UIFormId.CreatRoleView,this);
            }                            

        }
    }
}
