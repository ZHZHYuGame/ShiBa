//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Network;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class SCProfNumInfoHandler : PacketHandlerBase
    {
        public override int Id
        {
            get
            {
                return 7006;
            }
        }

        public override void Handle(object sender, Packet packet)
        {
            SCProfNumInfo packetImpl = (SCProfNumInfo)packet;
            Log.Info("Receive packet '{0}'.", packetImpl.Id.ToString());
        }
    }
}
