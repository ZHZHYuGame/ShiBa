using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSAllInfoReq : CSPacketBase
{
    public override int Id => 1454;

    public int no_chat_record;
    public override void Encode()
    {
        base.Encode();
    }

    public override void Clear()
    {
        MsgAdapter.WriteInt(no_chat_record);
    }
}
