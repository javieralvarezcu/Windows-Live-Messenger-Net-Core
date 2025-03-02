﻿using System;
using System.Configuration;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using WLMClient.UI.Controls;
using WLMData.Enums;

namespace WLMClient.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Login pageLogin = null;
        private Main pageMain = null;

        public MainWindow()
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            Config.Properties.SERVER_ADDRESS = ConfigurationManager.AppSettings["server_address"];
            Config.Properties.SERVER_PORT = Convert.ToInt32(ConfigurationManager.AppSettings["server_port"]);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            InitializeComponent();

            Layout.Images.Load();
            Network.Client.Load(this);

            pageLogin = new Login();
            controlPanel.Children.Add(pageLogin);

            this.Icon = new BitmapImage(new Uri(Resource.Images.Identifiers.APP_ICON_STATUS_OFFLINE, UriKind.Absolute));

            StartUserStatusWorker();
        }

        private void StartUserStatusWorker()
        {
            Thread statusWorker = new Thread(UserStatusWorker);
            statusWorker.Start();
        }

        private void UserStatusWorker()
        {
            int olderUserStatus = 0;
            bool isUserStatusAway = false;

            while (true)
            {
                if (pageMain != null)
                {
                    int lastUpdate = (int)Locale.LastUserInput.GetLastInputTime();

                    if (lastUpdate > 150)
                    {
                        if (Locale.Personal.USER_INFO.status != (int)UserStatus.Offline && !isUserStatusAway)
                        {
                            olderUserStatus = Locale.Personal.USER_INFO.status;

                            Locale.Personal.USER_INFO.status = Convert.ToInt16(UserStatus.Away);
                            Network.Client.SendUserUpdate();

                            isUserStatusAway = true;
                        }
                    }
                    else
                    {
                        if (isUserStatusAway)
                        {
                            Locale.Personal.USER_INFO.status = olderUserStatus;
                            Network.Client.SendUserUpdate();

                            isUserStatusAway = false;
                        }
                    }
                }

                Thread.Sleep(2500);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (pageMain != null)
            {
                pageMain.WindowSizeChanged(this.ActualWidth, this.ActualHeight);
            }

            if (pageLogin != null)
            {
                pageLogin.WindowSizeChanged(this.ActualHeight);
            }
        }

        public void LoginSuccess()
        {
            ClearLogin();

            pageMain = new Main();
            controlPanel.Children.Add(pageMain);

            UpdateLayout();
            pageMain.UpdateLayout();
            pageMain.WindowSizeChanged(this.ActualWidth, this.ActualHeight);
        }

        public void ClearLogin()
        {
            controlPanel.Children.Remove(pageLogin);
            pageLogin = null;
            controlPanel.Children.Clear();
        }

        public void ConnectionClosedLogOut()
        {
            if (pageLogin == null)
            {
                Locale.ManageChatWindows.CloseAllOpenChatWindows();

                pageMain = null;
                controlPanel.Children.Clear();

                pageLogin = new Login();
                controlPanel.Children.Add(pageLogin);

                UpdateLayout();
                pageLogin.UpdateLayout();
                pageLogin.WindowSizeChanged(this.ActualHeight);

                this.Icon = new BitmapImage(new Uri(Resource.Images.Identifiers.APP_ICON_STATUS_OFFLINE, UriKind.Absolute));
            }
            else
            {
                MessageBox.Show("Windows Live Messenger was not able to contact the server. Please check your internet connection.", "Unable to connect to server.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsPageMainNull()
        {
            bool returnValue = false;

            if (pageMain == null)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public Main GetMainPage()
        {
            return pageMain;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pageMain != null)
            {
                this.WindowState = WindowState.Minimized;

                e.Cancel = true;
            }
            else
            {
                Locale.ManageChatWindows.CloseAllOpenChatWindows();

                Environment.Exit(0);
            }
        }
    }
}
