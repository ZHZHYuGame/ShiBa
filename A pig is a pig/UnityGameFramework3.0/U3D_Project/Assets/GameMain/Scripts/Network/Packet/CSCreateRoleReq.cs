using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSCreateRoleReq : CSPacketBase
{
    public override int Id => 7150;

    public string plat_name = "";
	public string role_name = "";
	public uint login_time = 0;
	public string key = "";
	public short plat_server_id = 0;
	public sbyte plat_fcm = 0;
	public sbyte avatar = 0;
	public sbyte sex = 0;
	public sbyte prof = 0;
	public sbyte camp_type = 0;
	public string plat_spid = "";
	


	public override void Encode()
	{
		base.Encode();
        MsgAdapter.WriteStrN(plat_name, 64);
		MsgAdapter.WriteStrN(role_name, 32);
		MsgAdapter.WriteUInt(login_time);
		MsgAdapter.WriteStrN(key, 32);
		MsgAdapter.WriteShort(plat_server_id);
		MsgAdapter.WriteChar(plat_fcm);
		MsgAdapter.WriteChar(avatar);
		MsgAdapter.WriteChar(sex);
		MsgAdapter.WriteChar(prof);
		MsgAdapter.WriteChar(camp_type);
		MsgAdapter.WriteChar(0);
		MsgAdapter.WriteStrN(plat_spid, 4);
	}
	public override void Clear()
	{
		
	}
}
