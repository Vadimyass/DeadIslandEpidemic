using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class ClientSend : MonoBehaviour
    {
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            Client.instance.tcp.SendData(packet);
        }

        #region Packets

        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int) ClientPackets.welcomeReceived))
            {
                packet.Write(Client.instance.myId);
                packet.Write("Here might be your username");

                SendTCPData(packet);
            }
        }

        #endregion

    }  
}

