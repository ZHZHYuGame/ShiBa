//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;

namespace StarForce
{
    public abstract class CSPacketBase : PacketBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ClientToServer;
            }
        }

        public virtual void Encode()
        {
            MsgAdapter.InitWriteMsg();
            MsgAdapter.WriteBegin((ushort)Id);
        }

        public void ToStream(Stream stream)
        {
            this.Encode();
            ///发送
            MsgAdapter.ToStream(stream);
        }
    }
}
