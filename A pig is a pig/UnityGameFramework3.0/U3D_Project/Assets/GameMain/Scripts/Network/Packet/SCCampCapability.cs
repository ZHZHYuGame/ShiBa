using StarForce;
using System.Collections.Generic;


public class SCCampCapability : SCPacketBase
{
    public int[] capability_list;

	public override int Id => 7008;

    public override void Decode()
    {
        capability_list = new int[4];
        for (int i = 0; i <= 3; i++)
        {
            this.capability_list[i] = MsgAdapter.ReadInt();
        }
    }

	public override void Clear()
	{
		
	}
}

