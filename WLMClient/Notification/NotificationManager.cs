using WLMClient.UI.Windows;

namespace WLMClient.Notification
{
    class NotificationManager
    {
        public static void Showpopup(string line1, string line2, ChatWindow chatWindow)
        {
            Popup popup = new Popup(line1, line2, chatWindow);
            popup.Show();
        }
    }
}
