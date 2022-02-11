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

        // Update is called once per frame
        void Update()
        {

        }

        public void ConnectToServer()
        {
            tcp.Connect();
        }


        public class TCP
        {
            public TcpClient socket;

            private NetworkStream _stream;
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
    
                    byte[] _data = new byte[_byteLength];
                    Array.Copy(_receiveBuffer, _data, _byteLength);
    
                    _stream.BeginRead(_receiveBuffer, 0, _dataBufferSize, ReceiveCallBack, null);
                }
                catch (Exception _ex)
                {
                    Debug.Log($"Error receiving TCP data: {_ex} ");
                }
            }
        }
    }
}


