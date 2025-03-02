using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

using WLMData.Enums;
using WLMServer.Network.UserData;

namespace WLMServer.Network.PacketHandling
{
    class TransferNudge : PacketHandler
    {
        public TransferNudge(Server server) : base(server) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>(PacketName.sendNudge.ToString(), Transfer);
        }

        protected void Transfer(PacketHeader header, Connection connection, string userID)
        {
            Connection targetUserConnection = server.GetConnectionFromUserID(userID);
            ConnectedUser senderUser = server.GetConnectedUser(connection);

            if (targetUserConnection != null)
            {
                server.SendPacket(targetUserConnection, PacketName.sendNudge.ToString(), senderUser.user.id);
            }
        }
    }
}