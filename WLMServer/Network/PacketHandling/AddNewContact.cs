using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

using WLMData.Enums;
using WLMServer.Network.UserData;

namespace WLMServer.Network.PacketHandling
{
    class AddNewContact : PacketHandler
    {
        public AddNewContact(Server server) : base(server) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>(PacketName.requestNewContact.ToString(), AddContact);
        }

        protected void AddContact(PacketHeader header, Connection connection, string userID)
        {
            if (server.accountManager.IsUserInDatabase(userID))
            {
                ConnectedUser senderUser = server.GetConnectedUser(connection);
                ContactList senderContactList = senderUser.contactList;

                if (!senderContactList.IsUserInContactList(userID))
                {
                    senderContactList.AddNewUser(userID, false, false);

                    server.accountManager.SaveContactData(senderUser.user.id, senderContactList.CreateData());
                    server.accountManager.AddNewFriendRequest(senderUser.user.id, userID);

                    Connection UserConnection = server.GetConnectionFromUserID(userID);
                    if (UserConnection != null)
                    {
                        server.SendFriendRequestToUser(UserConnection, senderUser.user);
                    }

                    server.SendUsersContactList(connection, senderUser.user.id);
                }
            }
        }
    }
}
