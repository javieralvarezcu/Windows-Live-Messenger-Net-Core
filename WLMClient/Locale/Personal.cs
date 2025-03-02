﻿using System.Collections.Generic;

using WLMClient.UI.Windows;
using WLMData.Data.Packets;

namespace WLMClient.Locale
{
    class Personal
    {
        public static UserInfo USER_INFO { get; set; }
        public static List<UserInfo> USER_CONTACTS { get; set; }
        public static List<ChatWindow> OPEN_CHAT_WINDOWS { get; set; }
    }
}
