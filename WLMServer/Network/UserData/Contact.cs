﻿namespace WLMServer.Network.UserData
{
    class Contact
    {
        public string id { get; set; }
        public bool isBlocked { get; set; }
        public bool isAccepted { get; set; }

        public Contact(string id, bool isBlocked, bool isAccepted)
        {
            this.id = id;
            this.isBlocked = isBlocked;
            this.isAccepted = isAccepted;
        }
    }
}
