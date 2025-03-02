using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using WLMClient.UI.Windows;

namespace WLMClient.Network.PacketHandling
{
    class ConnectionClosed : PacketHandler
    {
        public ConnectionClosed(MainWindow mainWindow) : base(mainWindow) { }

        private NetworkComms.ConnectionEstablishShutdownDelegate closeHandler;

        public override void InitializePacket()
        {
            closeHandler = new NetworkComms.ConnectionEstablishShutdownDelegate((Connection connection) =>
            {
                mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
                {
                    mainWindow.ConnectionClosedLogOut();
                }));
            });

            Open();
        }

        public void Open()
        {
            NetworkComms.AppendGlobalConnectionCloseHandler(closeHandler);
        }

        public void Close()
        {
            NetworkComms.RemoveGlobalConnectionCloseHandler(closeHandler);
        }
    }
}
