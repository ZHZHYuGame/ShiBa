using GameFramework.Network;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SCLoginAckHandler : PacketHandlerBase
{
    public override int Id => 7000;

    public override void Handle(object sender, Packet packet)
    {
        SCLoginAck packetImpl = (SCLoginAck)packet;
        Log.Info(packetImpl.role_id);
        Test.ins.DestoryChannel("testName", packetImpl.gs_port,packetImpl);
    }
}
