﻿using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using WLMClient.Locale;
using WLMClient.UI.Windows;
using WLMData.Enums;

namespace WLMClient.Network.PacketHandling
{
    class ReceiveNudge : PacketHandler
    {
        public ReceiveNudge(MainWindow mainWindow) : base(mainWindow) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>(PacketName.sendNudge.ToString(), Nudge);
        }

        protected void Nudge(PacketHeader header, Connection connection, string userID)
        {
            PostLoginAction(userID);
        }

        public override void PostLoginAction(object packet)
        {
            base.PostLoginAction(packet);

            mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                ManageChatWindows.ReceiveNudge((string)packet);
            }));
        }
    }
}