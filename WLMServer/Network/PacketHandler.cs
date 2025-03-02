namespace WLMServer.Network
{
    class PacketHandler
    {
        public Server server { get; private set; }

        public PacketHandler(Server server)
        {
            this.server = server;

            InitializePacket();
        }

        public virtual void InitializePacket() { }
    }
}
