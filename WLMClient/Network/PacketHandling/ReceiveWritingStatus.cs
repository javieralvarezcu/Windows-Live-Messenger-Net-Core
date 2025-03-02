using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using WLMClient.Locale;
using WLMClient.UI.Windows;
using WLMData.Data.Packets;
using WLMData.Enums;

namespace WLMClient.Network.PacketHandling
{
    class ReceiveWritingStatus : PacketHandler
    {
        public ReceiveWritingStatus(MainWindow mainWindow) : base(mainWindow) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<WritingStatus>(PacketName.sendWritingStatus.ToString(), Status);
        }

        protected void Status(PacketHeader header, Connection connection, WritingStatus writingStatus)
        {
            PostLoginAction(writingStatus);
        }

        public override void PostLoginAction(object packet)
        {
            base.PostLoginAction(packet);

            mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                ManageChatWindows.ReceiveWritingStatus((WritingStatus)packet);
            }));
        }
    }
}