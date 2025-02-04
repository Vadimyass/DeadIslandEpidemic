﻿using System.Collections;
using System.Collections.Generic;
using Gameplay.Character.AnimationControllers;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.instance.myId);
            packet.Write("KEKKEKE123");

            SendTCPData(packet);
        }
    }

    /// <summary>Sends player input to the server.</summary>
    /// <param name="inputs"></param>
    public static void PlayerMovement(Vector3 positionInput,Vector3 movementInput)
    {
        using (Packet packet = new Packet((int)ClientPackets.playerMovement))
        {
            packet.Write(positionInput);
            packet.Write(movementInput);
            
            SendUDPData(packet);
        }
    }

    public static void PlayerAnimationBool(AnimationNameType animationNameType, bool animationBool)
    {
        using (Packet packet = new Packet((int)ClientPackets.playerAnimationBool))
        {
            packet.Write(animationNameType.ToString());
            packet.Write(animationBool);

            SendTCPData(packet);
        }
    }
    
    public static void PlayerAnimationTrigger(AnimationNameType animationNameType)
    {
        using (Packet packet = new Packet((int)ClientPackets.playerAnimationTrigger))
        {
            packet.Write(animationNameType.ToString());

            SendTCPData(packet);
        }
    }

    public static void SendPlayerRotation(Quaternion quaternionInput)
    {
        using (Packet packet = new Packet((int)ClientPackets.playerRotation))
        {
            packet.Write(quaternionInput);

            SendTCPData(packet);
        }
    }

    public static void SendInvokeFirstSkill(Vector3 positionToLook)
    {
        using (Packet packet = new Packet((int)ClientPackets.firstSkillInvokation))
        {
            packet.Write(positionToLook);
            
            SendTCPData(packet);
        }
    }
    
    #endregion
}
