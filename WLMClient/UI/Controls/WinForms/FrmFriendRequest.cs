using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WLMClient.Layout;
using WLMClient.Network;
using WLMData.Data.Packets;

namespace WLMClient.UI.Controls.WinForms
{
    public partial class FrmFriendRequest : Form
    {
        private string userID = "";
        private bool friendRequestAnswered = false;

        public FrmFriendRequest(UserInfo userInfo)
        {
            InitializeComponent();

            userID = userInfo.id;
            lblText.Text =
                "'" + userInfo.name + "' (" + userInfo.id + ") has added you to his/her contact list." + "\n\n"
                + "Do you want to add this person to your own contact list?";

            CroppedBitmap defaultImg = LoadResource.GetDefaultAvatarImage();

            if (userInfo.avatar != "")
            {
                BitmapImage userImg = LoadResource.GetAvatar(userInfo.avatar);

                if (userImg != null)
                {
                    imgUserAvatar.Image = BitmapImage2Bitmap(userImg);
                }
                else
                {
                    imgUserAvatar.Image = BitmapImage2Bitmap(defaultImg);
                }
            }
            else
            {
                imgUserAvatar.Image = BitmapImage2Bitmap(defaultImg);
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);

                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        private Bitmap BitmapImage2Bitmap(CroppedBitmap bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            friendRequestAnswered = true;

            Client.SendFriendRequestResponse(userID, WLMData.Enums.FriendRequestResponseCode.decline);

            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            friendRequestAnswered = true;

            Client.SendFriendRequestResponse(userID, WLMData.Enums.FriendRequestResponseCode.accept);

            this.Close();
        }

        private void frmFriendRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (friendRequestAnswered == false)
            {
                Client.SendFriendRequestResponse(userID, WLMData.Enums.FriendRequestResponseCode.decline);
            }
        }
    }
}
