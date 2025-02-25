using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
public class CSRoleReq : CSPacketBase
{
    public override int Id => 7057;
    public int rand_1 = 0;
    public uint login_time = 0;
    public string key = "";
    public string plat_name = "";
    public short plat_server_id = 0;
    public sbyte plat_fcm = 0;
    public int rand_2 = 0;
    public int role_id = 0;

    //public int rand_1;
    //public uint login_time;
    //public string key;
    //public string plat_name ;
    //public short plat_server_id ;
    //public sbyte plat_fcm ;
    //public int rand_2 ;
    //public int role_id;
    public override void Encode()
    {
        base.Encode();
        MsgAdapter.WriteInt(rand_1);
        MsgAdapter.WriteUInt(login_time);
        MsgAdapter.WriteStrN(key, 32);
        MsgAdapter.WriteStrN(plat_name, 64);
        MsgAdapter.WriteShort(plat_server_id);
        MsgAdapter.WriteChar(0);
        MsgAdapter.WriteChar(plat_fcm);
        MsgAdapter.WriteInt(rand_2);
        MsgAdapter.WriteInt(role_id);
    }

    public override void Clear()
    {
        
    }
}
