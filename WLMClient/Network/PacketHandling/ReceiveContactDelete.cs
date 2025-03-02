using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using WLMClient.UI.Windows;
using WLMData.Data.Packets;
using WLMData.Enums;

namespace WLMClient.Network.PacketHandling
{
    class ReceiveContactDelete : PacketHandler
    {
        public ReceiveContactDelete(MainWindow mainWindow) : base(mainWindow) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<UserInfo>(PacketName.sendContactDelete.ToString(), IncomingDeleteContact);
        }

        protected void IncomingDeleteContact(PacketHeader header, Connection connection, UserInfo userInfo)
        {
            PostLoginAction(userInfo);
        }

        public override void PostLoginAction(object packet)
        {
            base.PostLoginAction(packet);

            mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                mainWindow.GetMainPage().RemoveContact(((UserInfo)packet).id);
            }));
        }
    }
}
