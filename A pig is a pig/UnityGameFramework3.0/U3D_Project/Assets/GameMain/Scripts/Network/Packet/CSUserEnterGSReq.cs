using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSUserEnterGSReq : CSPacketBase
{
    public override int Id => 1050;

    public int scene_id;
    public int scene_key;
    public int last_scene_id;
    public int role_id;
    public string role_name;
    public int time;
    public sbyte is_login;
    public short server_id;
    public string key;
    public string plat_name;
    public int is_micro_pc;
    public string plat_spid;

    
    public override void Encode()
    {
        base.Encode();
        MsgAdapter.WriteInt(scene_id);
        MsgAdapter.WriteInt(scene_key);
        MsgAdapter.WriteInt(last_scene_id);
        MsgAdapter.WriteInt(role_id);
        MsgAdapter.WriteStrN(role_name, 32);
        MsgAdapter.WriteInt(time);
        MsgAdapter.WriteChar(is_login);
        MsgAdapter.WriteChar(0);
        MsgAdapter.WriteShort(server_id);
        MsgAdapter.WriteStrN(key, 32);
        MsgAdapter.WriteStrN(plat_name, 64);
        MsgAdapter.WriteInt(is_micro_pc);
        MsgAdapter.WriteStrN(plat_spid, 4);
    }

    public override void Clear()
    {
       
    }

}
