using StarForce;

public class SCProfNumInfo : SCPacketBase
{
    public int prof1_num;
    public int prof2_num;
    public int prof3_num;
    public int prof4_num;

	public override int Id => 7006;


    public override void Decode()
    {
        this.prof1_num = MsgAdapter.ReadInt();

        this.prof2_num = MsgAdapter.ReadInt();

        this.prof3_num = MsgAdapter.ReadInt();

        this.prof4_num = MsgAdapter.ReadInt();

       // UnityLog.Info($"SCProfNumInfo proto : {this.prof1_num}   {this.prof2_num}    {this.prof3_num}   {this.prof4_num} ");
    }

	public override void Clear()
	{
		
	}
}

