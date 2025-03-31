using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 月考一服务器
{
    internal class NetManager : Singleton<NetManager>
    {
        Socket mainsocket;
        public List<Client> clients = new List<Client>();
        public void Init()
        {
            mainsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mainsocket.Bind(new IPEndPoint(IPAddress.Any, 10001));
            mainsocket.Listen(1000);
            mainsocket.BeginAccept(AsyAccept, null);
            Console.WriteLine("服务器已开启");
        }

        private void AsyAccept(IAsyncResult ar)
        {
            try
            {
                Socket socket = mainsocket.EndAccept(ar);
                IPEndPoint ip = socket.RemoteEndPoint as IPEndPoint;
                Client cli = new Client();
                cli.socket_cli = socket;
                cli.port = ip.Port;
                Console.WriteLine(ip.Port + "已连接");
                clients.Add(cli);
                mainsocket.BeginAccept(AsyAccept, null);
                cli.socket_cli.BeginReceive(cli.data_cli, 0, cli.data_cli.Length, SocketFlags.None, AsyReceive, cli);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void AsyReceive(IAsyncResult ar)
        {
            try
            {
                Client cli = (Client)ar.AsyncState;
                int len = cli.socket_cli.EndReceive(ar);
                if (len > 0)
                {
                    byte[] rdata = new byte[len];
                    Buffer.BlockCopy(cli.data_cli, 0, rdata, 0, len);
                    while (rdata.Length > 4)
                    {
                        int bodylen = BitConverter.ToInt32(rdata, 0);
                        byte[] bodydata = new byte[bodylen];
                        Buffer.BlockCopy(rdata, 4, bodydata, 0, bodylen);

                        int mesid = BitConverter.ToInt32(bodydata, 0);
                        byte[] mesinfo = new byte[bodydata.Length - 4];
                        Buffer.BlockCopy(bodydata, 4, mesinfo, 0, mesinfo.Length);

                        MesData mesdata = new MesData();
                        mesdata.data = mesinfo;
                        mesdata.cli = cli;

                        MessageManager<MesData>.Ins.OnBoardCast(mesid, mesdata);

                        int sylen = rdata.Length - 4 - bodylen;
                        byte[] sydata = new byte[sylen];
                        Buffer.BlockCopy(rdata, 4 + bodylen, sydata, 0, sylen);
                        rdata = sydata;
                    }
                }
                cli.socket_cli.BeginReceive(cli.data_cli, 0, cli.data_cli.Length, SocketFlags.None, AsyReceive, cli);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void AsySend(int id, byte[] senddata, Client cli)
        {
            int bodylen = 4 + senddata.Length;
            byte[] enddata = new byte[0];
            enddata = enddata.Concat(BitConverter.GetBytes(bodylen)).Concat(BitConverter.GetBytes(id)).Concat(senddata).ToArray();
            cli.socket_cli.BeginSend(enddata, 0, enddata.Length, SocketFlags.None, AsySendCall, cli);
        }

        private void AsySendCall(IAsyncResult ar)
        {
            try
            {
                Client cli = (Client)ar.AsyncState;
                int len = cli.socket_cli.EndSend(ar);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
