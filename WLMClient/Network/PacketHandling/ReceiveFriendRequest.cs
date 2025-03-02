using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using WLMClient.UI.Controls.WinForms;
using WLMClient.UI.Windows;
using WLMData.Data.Packets;
using WLMData.Enums;

namespace WLMClient.Network.PacketHandling
{
    class ReceiveFriendRequest : PacketHandler
    {
        public ReceiveFriendRequest(MainWindow mainWindow) : base(mainWindow) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<UserInfo>(PacketName.sendFriendRequestToUser.ToString(), IncomingFriendRequest);
        }

        protected void IncomingFriendRequest(PacketHeader header, Connection connection, UserInfo userInfo)
        {
            PostLoginAction(userInfo);
        }

        public override void PostLoginAction(object packet)
        {
            base.PostLoginAction(packet);

            mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                FrmFriendRequest friendRequest = new FrmFriendRequest((UserInfo)packet);
                friendRequest.ShowDialog();
            }));
        }
    }
}