using GameFramework;
using GameFramework.Network;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCUserEnterGSAckHandler : PacketHandlerBase
{
    public override int Id => 1000;

    public override void Handle(object sender, Packet packet)
    {
        SCUserEnterGSAck packetiml=(SCUserEnterGSAck)packet;
        ColorLog.LogCyan("ÇÐ»»³¡¾°");
        GameEntry.Event.Fire(ChangeSceneEventArgs.EventId,ChangeSceneEventArgs.Create(Test.ins.sceneId));
        CSAllInfoReq msg = ReferencePool.Acquire<CSAllInfoReq>();
        msg.no_chat_record = 0;
        Test.ins.m_Channel.Send(msg);
    }
}
