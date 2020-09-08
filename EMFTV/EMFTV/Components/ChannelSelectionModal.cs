using System;
using CoreGraphics;
using EMFTV.Global;
using EMFTV.Models;
using UIKit;

namespace EMFTV.Components
{
    public class ChannelSelectionModal: UIViewController
    {
        UIButton ChannelsButton;

        public delegate void ChannelSelectedHandler(object sender, Channel ch);
        public event ChannelSelectedHandler ChannelSelected;

        public ChannelSelectionModal(UIImage img)
        {
            UIImageView BG = new UIImageView(View.Frame);
            BG.Image = img;
            BG.Frame = new CGRect(x: 0, y: 0, width: Variables.ScreenWidth, height:Variables.ScreenHeight);

            // Add the Image View to the parent view
            View.AddSubview(BG);
            var blur = UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark);
            float x = 0;
            float y = 0;
            float width = (float)Variables.ScreenWidth;
            float height = (float)Variables.ScreenHeight;
            var blurView = new UIVisualEffectView(blur);
            blurView.Frame = new CGRect(x, y, width, height);

            View.Add(blurView);
            ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            int ChannelCount = 1;
            foreach (Channel ch in Application.Config.Channels)
            {
                if (ch.Device == DeviceType.Ios)
                {
                    ChannelsButton = new UIButton(UIButtonType.System);
                    ChannelsButton.SetTitle(ch.Title, UIControlState.Normal);
                    ChannelsButton.TitleLabel.Font = UIFont.SystemFontOfSize(24, UIFontWeight.Bold);
                    ChannelsButton.Frame = new CGRect(x: Variables.ScreenWidth / 2 - 270, y: ((Variables.ScreenHeight / 2) + (ChannelCount * 132) - 48), width: 540, height: 96);
                    ChannelsButton.PrimaryActionTriggered += async (sender, e) =>
                    {
                        //await DisplayChannelModal();
                        Variables.CurrentChannel = ch;
                        ChannelSelected.Invoke(this,ch);
                        Console.WriteLine($"{ch.Title} selected!");
                    };
                    View.AddSubview(ChannelsButton);

                    ChannelCount--;
                }
            }
        }
    }
}
