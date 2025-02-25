
using GameFramework.Network;
using StarForce;
using System.IO;

public static class MsgAdapter
{
    private static ByteBuffer read_buf = new ByteBuffer(ReadAndWrite.Read);
    private static ByteBuffer write_buf = new ByteBuffer(ReadAndWrite.Write);

    /// <summary>
    /// 每次收到服务器新的消息后，
    /// 要重新把新消息传入这个方法，
    /// 以后再使用readXXX方法解析数据
    /// </summary>
    /// <param name="_read_buf"></param>
    public static void InitReadMsg(Stream sm)
    {
        read_buf.Reset(sm);
    }

    public static void InitWriteMsg()
    {
        write_buf.Reset();
    }

    #region 读数据
    public static sbyte ReadChar()
    {
        return read_buf.ReadSByte();
    }

    public static byte ReadUChar()
    {
        return read_buf.ReadByte();
    }

    public static short ReadShort()
    {
        return read_buf.ReadShort();
    }

    public static ushort ReadUShort()
    {
        return read_buf.ReadUShort();
    }

    public static int ReadInt()
    {
        return read_buf.ReadInt();
    }

    public static uint ReadUInt()
    {
        return read_buf.ReadUInt();
    }

    public static long ReadLL()
    {
        return read_buf.ReadLong();
    }

    public static float ReadFloat()
    {
        return read_buf.ReadFloat();
    }

    public static double ReadDouble()
    {
        return read_buf.ReadDouble();
    }

    public static string ReadStrN(ushort str_len)
    {
        return read_buf.ReadString(str_len);
    }
    #endregion


    #region 写数据
    public static void WriteBegin(ushort msg_type)
    {
        write_buf.WriteUInt(0);
        write_buf.WriteUShort(msg_type);
        write_buf.WriteShort(0);
    }

    public static void WriteChar(sbyte value)
    {
        write_buf.WriteSByte(value);
    }

    public static void WriteUChar(byte value)
    {
        write_buf.WriteByte(value);
    }

    public static void WriteShort(short value)
    {
        write_buf.WriteShort(value);
    }

    public static void WriteUShort(ushort value)
    {
        write_buf.WriteUShort(value);
    }

    public static void WriteInt(int value)
    {
        write_buf.WriteInt(value);
    }

    public static void WriteUInt(uint value)
    {
        write_buf.WriteUInt(value);
    }

    public static void WriteLL(long value)
    {
        write_buf.WriteLong(value);
    }

    public static void WriteFloat(float value)
    {
        write_buf.WriteFloat(value);
    }

    public static void WriteDouble(double value)
    {
        write_buf.WriteDouble(value);
    }

    public static void WriteStrN(string value, ushort len)
    {
        write_buf.WriteString(value, len);
    }

    public static void WriteStr(string value)
    {
        write_buf.WriteString(value);
    }

    #endregion

    public static void ToStream(Stream stream)
    {

         write_buf.ToStream(stream);

    }
}

/***
-- 发送消息
local send_buf = ""
function MsgAdapter.Send(net)
	send_buf = struct.pack(write_fmt, unpack(write_value_list))

    net = net or GameNet.Instance: GetCurNet()

    GameNet.Instance:GetCurNet():SendMsg(send_buf, nil)
end
	**///