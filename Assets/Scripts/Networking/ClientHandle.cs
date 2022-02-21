using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Networking
{
    class ClientHandle
    {
        public static void Welcome(Packet packet)
        {
            string message = packet.ReadString();
            int myId = packet.ReadInt();

            Debug.Log($"Message from server:{message}");
            Client.instance.myId = myId;
            
            ClientSend.WelcomeReceived();
        }
    }
}
