using GameFramework;
using GameFramework.Network;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SCCreateRoleAckHandler : PacketHandlerBase
{
	public override int Id => 7100;

	public override void Handle(object sender, Packet packet)
	{
		SCCreateRoleAck sCCreateRoleAck = (SCCreateRoleAck)packet;
		Log.Info("Receive packet '{0}'.", sCCreateRoleAck.Id.ToString());
        if(sCCreateRoleAck.result==0)
        {
            Test.ins.role_name = sCCreateRoleAck.role_name;
            CSRoleReq msg = ReferencePool.Acquire<CSRoleReq>();
            msg.rand_1 = UnityEngine.Random.Range(1000000,1000000);
            msg.login_time = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            msg.key = "";
            msg.plat_name = Test.ins.play_name;
            msg.rand_1 = UnityEngine.Random.Range(1000000, 1000000);
            msg.plat_fcm = 0;
            msg.plat_server_id = 1;
            msg.role_id = sCCreateRoleAck.role_id;
            Test.ins.m_Channel.Send(msg);
        }
        else if(sCCreateRoleAck.result==-2)
        {
            ColorLog.LogCyan("角色创建成功");
           
        }
        else if(sCCreateRoleAck.result==-1)
        {
            ColorLog.LogCyan("角色存在");
        }
        
    }
	
}
