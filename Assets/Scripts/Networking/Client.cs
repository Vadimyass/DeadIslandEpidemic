using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Networking
{
    public class Client : MonoBehaviour
    {
        public static Client instance;

        public static int _dataBufferSize = 4096;

        public string ip = "127.0.0.1";
        public int port = 26950;
        public int myId = 0;
        public TCP tcp;

        public delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        void Start()
        {
            tcp = new TCP();

            ConnectToServer();
        }

        public void ConnectToServer()
        {
            InitializeClientData();
            tcp.Connect();
        }


        private void InitializeClientData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
                {
                    {(int)ServerPackets.welcome,ClientHandle.Welcome }
                };

            Debug.Log("Initialized packets.");
        }


        public class TCP
        {
            public TcpClient socket;

            private NetworkStream _stream;
            private Packet _receivedData;
            private byte[] _receiveBuffer;

            public void Connect()
            {
                socket = new TcpClient
                {
                    ReceiveBufferSize = _dataBufferSize,
                    SendBufferSize = _dataBufferSize,
                };

                _receiveBuffer = new byte[_dataBufferSize];

                socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
            }

            private void ConnectCallback(IAsyncResult _result)
            {
                socket.EndConnect(_result);

                if (!socket.Connected)
                {
                    return;
                }

                _stream = socket.GetStream();

                _receivedData = new Packet();
                
                Debug.Log($"Client connected to {socket.Client.RemoteEndPoint}");

                _stream.BeginRead(_receiveBuffer, 0, _dataBufferSize, ReceiveCallBack, null);
            }
            
            private void ReceiveCallBack(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = _stream.EndRead(_result);
    
                    if (_byteLength <= 0)
                    {
                        return;
                    }
    
                    byte[] data = new byte[_byteLength];
                    Array.Copy(_receiveBuffer, data, _byteLength);

                    _receivedData.Reset(HandleData(data));
    
                    _stream.BeginRead(_receiveBuffer, 0, _dataBufferSize, ReceiveCallBack, null);
                }
                catch (Exception _ex)
                {
                    Debug.Log($"Error receiving TCP data: {_ex} ");
                }
            }

            private bool HandleData(byte[] data)
            {
                int packetLenght = 0;

                _receivedData.SetBytes(data);

                if(_receivedData.UnreadLength() >= 4)
                {
                    packetLenght = _receivedData.ReadInt();
                    if(packetLenght <= 0)
                    {
                        return true;
                    }
                }

                while (packetLenght > 0 && packetLenght <= _receivedData.UnreadLength())
                {
                    byte[] packetBytes = _receivedData.ReadBytes(packetLenght);
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        packetHandlers[packetId](packet);
                    }


                    packetLenght = 0;
                    if (_receivedData.UnreadLength() >= 4)
                    {
                        packetLenght = _receivedData.ReadInt();
                        if (packetLenght <= 0)
                        {
                            return true;
                        }
                    }
                }

                if(packetLenght <= 1)
                {
                    return true;
                }

                return false;

            }
        }
    }
}


