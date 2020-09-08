using System;
using System.Drawing;
using CoreGraphics;
using EMFTV.Global;
using EMFTV.Models;
using UIKit;

namespace EMFTV.Components
{
    public class LyricModal : UIViewController
    {
        UIViewController LyricsModal;

        public delegate void ChannelSelectedHandler(object sender, Channel ch);
        public event ChannelSelectedHandler ChannelSelected;

        public LyricModal(string lyrics, UIImage img, SongModel song)
        {

            UIImageView BG = new UIImageView(View.Frame);
            BG.Image = img;
            BG.Frame = new CGRect(x: 0, y: 0, width: Variables.ScreenWidth, height: Variables.ScreenHeight);

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

            UIImageView SongImageView = new UIImageView(View.Frame);
            SongImageView.Image = img;
            SongImageView.Frame = new CGRect(x: Variables.ScreenHeight * .15, y: Variables.ScreenHeight * .15, width: Variables.ScreenHeight * .55, height: Variables.ScreenHeight * .55);
            // Add the Image View to the parent view
            View.AddSubview(SongImageView);

            UILabel SongTitleLabel = new UILabel();
            SongTitleLabel.Text = song.SongTitle;
            SongTitleLabel.Font = UIFont.SystemFontOfSize(36, UIFontWeight.Bold);
            SongTitleLabel.TextAlignment = UITextAlignment.Center;
            SongTitleLabel.TextColor = UIColor.White;
            SongTitleLabel.Frame = new CGRect(x: Variables.ScreenHeight * .15, y: Variables.ScreenHeight * .75 - 50, width: Variables.ScreenHeight * .55, height: 100);
            View.AddSubview(SongTitleLabel);

            UILabel SongArtistLabel = new UILabel();
            SongArtistLabel.Text = song.ArtistName;
            SongArtistLabel.Font = UIFont.SystemFontOfSize(36);
            SongArtistLabel.TextAlignment = UITextAlignment.Center;
            SongArtistLabel.TextColor = UIColor.White;
            SongArtistLabel.Frame = new CGRect(x: Variables.ScreenHeight * .15, y: Variables.ScreenHeight * .81 - 50, width: Variables.ScreenHeight * .55, height: 100);
            View.AddSubview(SongArtistLabel);

            UITextView LyricsView = new UITextView(new RectangleF((float)Variables.ScreenWidth * .4625f, (float)Variables.ScreenHeight * .12f, (float)Variables.ScreenWidth * .5f, (float)Variables.ScreenHeight - (float)Variables.ScreenHeight * .15f));
            LyricsView.UserInteractionEnabled = true;
            LyricsView.ScrollEnabled = true;
            LyricsView.Text = lyrics;
            LyricsView.TextColor = UIColor.White;
            LyricsView.SetContentOffset(new CGPoint(0, 400), true);
            View.AddSubview(LyricsView);
        }
    }
}


