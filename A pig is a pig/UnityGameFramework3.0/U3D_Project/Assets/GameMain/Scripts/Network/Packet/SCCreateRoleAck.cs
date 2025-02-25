using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCCreateRoleAck : SCPacketBase
{
	public int result;
	public int role_id;
	public string role_name;
	public sbyte avatar;
	public sbyte sex;
	public sbyte prof;
	public sbyte camp_type;
	public int level;
	public uint create_time;
	public override int Id =>7100;


	public override void Decode()
	{
		base.Decode();

		result = MsgAdapter.ReadInt();
		role_id = MsgAdapter.ReadInt();
		role_name = MsgAdapter.ReadStrN(32);
		avatar = MsgAdapter.ReadChar();
		sex = MsgAdapter.ReadChar();
		prof = MsgAdapter.ReadChar();
		camp_type = MsgAdapter.ReadChar();
		level = MsgAdapter.ReadInt();
		create_time = MsgAdapter.ReadUInt();
	}
	public override void Clear()
	{
		
	}
}
