using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCLoginAck : SCPacketBase
{
    public override int Id => 7000;

    public int role_id;
    public short result;
    public sbyte is_merged_server;
    public int scene_id;
    public int last_scene_id;
    public string key;
    public uint time;
    public string gs_hostname;
    public ushort gs_port;
    public ushort gs_index;
    public uint server_time;

    public override void Decode()
    {
        base.Decode();
        role_id = MsgAdapter.ReadInt();
        result = MsgAdapter.ReadShort();
        MsgAdapter.ReadChar();
        is_merged_server = MsgAdapter.ReadChar();
        scene_id = MsgAdapter.ReadInt();
        last_scene_id = MsgAdapter.ReadInt();
        key = MsgAdapter.ReadStrN(32);
        time = MsgAdapter.ReadUInt();
        gs_hostname = MsgAdapter.ReadStrN(64);
        gs_port = MsgAdapter.ReadUShort();
        gs_index = MsgAdapter.ReadUShort();
        server_time = MsgAdapter.ReadUInt();
    }

    public override void Clear()
    {
        
    }

}
