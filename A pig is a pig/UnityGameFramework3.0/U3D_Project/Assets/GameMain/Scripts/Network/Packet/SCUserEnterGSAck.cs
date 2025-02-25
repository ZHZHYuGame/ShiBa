using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCUserEnterGSAck : SCPacketBase
{
    public override int Id => 1000;

    public override void Decode()
    {
        base.Decode();
    }

    public override void Clear()
    {
        
    }

}
