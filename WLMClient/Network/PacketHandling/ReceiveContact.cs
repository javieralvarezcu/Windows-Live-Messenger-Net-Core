﻿using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Linq;
using WLMClient.Locale;
using WLMClient.UI.Windows;
using WLMData.Data.Packets;
using WLMData.Enums;

namespace WLMClient.Network.PacketHandling
{
    class ReceiveContact : PacketHandler
    {
        public ReceiveContact(MainWindow mainWindow) : base(mainWindow) { }

        public override void InitializePacket()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<UserInfo>(PacketName.sendContact.ToString(), IncomingContact);
        }

        protected void IncomingContact(PacketHeader header, Connection connection, UserInfo userInfo)
        {
            UserInfo foundUser = Personal.USER_CONTACTS.FirstOrDefault(x => x.id.Trim() == userInfo.id.Trim());

            if (foundUser == null || !(foundUser.name == userInfo.name & foundUser.status == userInfo.status &
                foundUser.comment == userInfo.comment & foundUser.blocked == userInfo.blocked & foundUser.avatar == userInfo.avatar))
            {
                PostLoginAction(userInfo);
            }
        }

        public override void PostLoginAction(object packet)
        {
            base.PostLoginAction(packet);

            mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                mainWindow.GetMainPage().UpdateContact((UserInfo)packet);
            }));
        }
    }
}
