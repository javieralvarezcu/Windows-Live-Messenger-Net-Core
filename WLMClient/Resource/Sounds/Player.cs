using System;
using System.Media;
using System.Windows;
using System.Windows.Resources;

namespace WLMClient.Resource.Sounds
{
    class Player
    {
        public static void PlaySound(string url)
        {
            Uri uri = new Uri(url);
            StreamResourceInfo streamResourceIfno = Application.GetResourceStream(uri);
            SoundPlayer sound = new SoundPlayer(streamResourceIfno.Stream);
            sound.Play();
        }
    }
}
