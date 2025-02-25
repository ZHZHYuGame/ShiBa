using StarForce;

public class CSLoginReq : CSPacketBase
{

    public int rand_1;
    public uint login_time;
    public string key = "";
    public string plat_name = "";
    public int rand_2 = 0;
    public short plat_fcm = 0;
    public short plat_server_id = 0;



	public override int Id => 7056;


    public override void Encode()
    {
        base.Encode();

        MsgAdapter.WriteInt(this.rand_1);

        MsgAdapter.WriteUInt(this.login_time);

        MsgAdapter.WriteStrN(this.key, 32);

        MsgAdapter.WriteStrN(this.plat_name, 64);

        MsgAdapter.WriteInt(this.rand_2);

        MsgAdapter.WriteShort(this.plat_fcm);

        MsgAdapter.WriteShort(this.plat_server_id);
    }

	public override void Clear()
	{
		
	}
}

