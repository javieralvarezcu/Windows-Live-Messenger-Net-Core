using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using WLMClient.Locale;
using WLMClient.UI.Windows;
using WLMData.Data.Packets;
using WLMData.Enums;

namespace WLMClient.Network.PacketHandling
{
    class ReceiveMessage : PacketHandler
    {
        public ReceiveMessage(MainWindow mainWindow) : base(mainWindow) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<Message>(PacketName.sendMessage.ToString(), IncomingMessage);
        }

        protected void IncomingMessage(PacketHeader header, Connection connection, Message message)
        {
            PostLoginAction(message);
        }

        public override void PostLoginAction(object packet)
        {
            base.PostLoginAction(packet);

            mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                ManageChatWindows.ReceiveMessage((Message)packet);
            }));
        }
    }
}