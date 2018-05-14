using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameCaro
{
    class SocketManager
    {
        #region Client
        Socket client;
        public bool ConnectServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), PORT);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(iep);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region Server
        Socket server;
        public void CreateServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), PORT);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(iep);
            server.Listen(10); //đợi kết nối từ client trong 10s

            //Tạo ra luồng riêng để chạy
            Thread acceptClient = new Thread(() =>
            {
                client = server.Accept();
            });
            
            acceptClient.IsBackground = true;  //khi tắt chương trình thì luồng này củng ngắt theo
            acceptClient.Start();      
        }
        #endregion

        #region Both
        public string IP = "127.0.0.1";
        public int PORT = 9999;
        public const int BUFFER = 1024;
        /// <summary>
        /// Kiểm tra có phải là server không
        /// </summary>
        public bool isServer = true;

        public bool Send(object data)
        {
            byte[] sendData = SerializeDate(data); //chuyển object data sang mảng byte và lưu lại
            
            return SendData(client, sendData);
        }

        public object Receive()
        {
            byte[] receiveData = new byte[BUFFER];
            bool isReceive = ReceiveData(client, receiveData);

            return DeserializeDate(receiveData);
        }


        private bool SendData(Socket targer, byte[] data)
        {
            return targer.Send(data) == 1 ? true : false;
        }

        private bool ReceiveData(Socket targer, byte[] data)
        {
            return targer.Receive(data) == 1 ? true : false;
        }

        /// <summary>
        /// Chuyển đổi 1 đối tượng sang mảng byte
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public byte[] SerializeDate(Object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }

        /// <summary>
        /// Giải nén 1 mảng byte thành 1 đối tượng object
        /// </summary>
        /// <param name="theByteArray"></param>
        /// <returns></returns>
        public object DeserializeDate(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf = new BinaryFormatter();
            ms.Position = 0;
            return bf.Deserialize(ms);
        }

        /// <summary>
        /// Lấy địa chỉ IPv4 trong máy
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if(ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        #endregion
    }
}
