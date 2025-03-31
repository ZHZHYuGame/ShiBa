using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

public class NetManager : Singleton<NetManager>
{
    Socket mainsocket;
    byte[] data = new byte[2048];
    Queue<byte[]> que = new Queue<byte[]>();
    public void Init()
    {
        mainsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        mainsocket.BeginConnect("127.0.0.1", 10001, AsyConnect, null);
    }

    private void AsyConnect(IAsyncResult ar)
    {
        try
        {
            mainsocket.EndConnect(ar);
            Console.WriteLine("成功连接服务器");
            mainsocket.BeginReceive(data, 0, data.Length, SocketFlags.None, AsyReceive, null);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private void AsyReceive(IAsyncResult ar)
    {
        try
        {
            int len = mainsocket.EndReceive(ar);
            if (len > 0)
            {
                byte[] rdata = new byte[len];
                Buffer.BlockCopy(data, 0, rdata, 0, len);
                while (rdata.Length > 4)
                {
                    int bodylen = BitConverter.ToInt32(rdata, 0);
                    byte[] bodydata = new byte[bodylen];
                    Buffer.BlockCopy(rdata, 4, bodydata, 0, bodylen);

                    que.Enqueue(bodydata);

                    int sylen = rdata.Length - 4 - bodylen;
                    byte[] sydata = new byte[sylen];
                    Buffer.BlockCopy(rdata, 4 + bodylen, sydata, 0, sylen);
                    rdata = sydata;
                }
            }
            mainsocket.BeginReceive(data, 0, data.Length, SocketFlags.None, AsyReceive, null);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public void AsySend(int id, byte[] senddata)
    {
        int bodylen = 4 + senddata.Length;
        byte[] enddata = new byte[0];
        enddata = enddata.Concat(BitConverter.GetBytes(bodylen)).Concat(BitConverter.GetBytes(id)).Concat(senddata).ToArray();
        mainsocket.BeginSend(enddata, 0, enddata.Length, SocketFlags.None, AsySendCall, null);
    }

    private void AsySendCall(IAsyncResult ar)
    {
        try
        {
            int len = mainsocket.EndSend(ar);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    public void GetUpData()
    {
        if (que.Count > 0)
        {
            byte[] rdata = que.Dequeue();
            int id = BitConverter.ToInt32(rdata, 0);
            byte[] enddata = new byte[rdata.Length - 4];
            Buffer.BlockCopy(rdata, 4, enddata, 0, enddata.Length);
            MessageManager<byte[]>.Ins.OnBoardCast(id, enddata);
        }
    }
}
