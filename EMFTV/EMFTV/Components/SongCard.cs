using System;
using CoreGraphics;
using EMFTV.Models;
using EMFTV.Services;
using Foundation;
using UIKit;

namespace EMFTV.Components
{
    public class SongCard : UIView
    {
        UIImageView SongImageView;
        UILabel SongTitleLabel;
        UILabel SongArtistLabel;
        SongModel Song;

        UIView BackGround;

        public delegate void SongClickedHandler(object sender, SongModel song);
        public event SongClickedHandler SongClicked;

        public SongCard(UIImage i, SongModel song, float x, float y, float w, float h)
        {
            Song = song;
            BackGround = new UIView();
            BackGround.BackgroundColor = UIColor.Black;
            BackGround.Alpha = .2f;
            BackGround.Frame = new CGRect(x: 0, y: 0, width: w, height: h);
            AddSubview(BackGround);

            SongImageView = new UIImageView(Frame);
            SongImageView.Image = i;
            SongImageView.Frame = new CGRect(x: 0, y: 0, width: h, height: h);
            SongImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            // Add the Image View to the parent view
            AddSubview(SongImageView);

            SongTitleLabel = new UILabel();
            SongTitleLabel.Text = song.SongTitle;
            SongTitleLabel.Font = UIFont.SystemFontOfSize(24, UIFontWeight.Bold);
            SongTitleLabel.TextColor = UIColor.White;
            SongTitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            SongTitleLabel.Lines = 2;
            SongTitleLabel.Frame = new CGRect(x: h + 24, y: h/8, width: w-(h+32), height: h/2);
            AddSubview(SongTitleLabel);

            SongArtistLabel = new UILabel();
            SongArtistLabel.Text = song.ArtistName;
            SongArtistLabel.Font = UIFont.SystemFontOfSize(24);
            SongArtistLabel.TextColor = UIColor.White;
            SongArtistLabel.AdjustsFontSizeToFitWidth = false;
            SongArtistLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            SongArtistLabel.Frame = new CGRect(x: h + 24, y: h / 2 + 8, width: w - (h + 24), height: 28);
            AddSubview(SongArtistLabel);
            Init();
        }


        public override bool CanBecomeFocused
        {
            get
            {
                return true;
            }
        }

        #region Override Methods
        public override async void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesBegan(presses, evt);

            foreach (UIPress press in presses)
            {
                // Was the Touch Surface clicked?
                if (press.Type == UIPressType.Select)
                {
                    Animate(.1f, () =>
                    {
                        Transform = CGAffineTransform.MakeScale(1F, 1F);
                    });
                    SongClicked?.Invoke(this, Song);
                    //var x = await MusicService.GetLyricsBySongId(Song.SongId);
                    //System.Console.WriteLine($"Lyrics:{x}");
                }
            }
        }

        public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesCancelled(presses, evt);

            foreach (UIPress press in presses)
            {
                // Was the Touch Surface clicked?
                if (press.Type == UIPressType.Select)
                {
                    SongClicked.Invoke(this, Song);
                }
            }
        }

        public override void PressesChanged(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesChanged(presses, evt);
        }

        public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesEnded(presses, evt);

            foreach (UIPress press in presses)
            {
                // Was the Touch Surface clicked?
                if (press.Type == UIPressType.Select)
                {
                    Animate(.1f, () =>
                    {
                        Transform = CGAffineTransform.MakeScale(1.1f, 1.1f);
                    });
                }
            }
        }
        #endregion

        public override void DidUpdateFocus(UIFocusUpdateContext context, UIFocusAnimationCoordinator coordinator)
        {
            //Console.WriteLine($"prev focus:{context.PreviouslyFocusedItem}, next focus:{context.NextFocusedItem}");
            base.DidUpdateFocus(context, coordinator);
            //Console.WriteLine(context);
            if (!Focused)
            {
                Animate(.2f, () =>
                {
                    Transform = CGAffineTransform.MakeScale(1F, 1F);
                    SongArtistLabel.TextColor = UIColor.White;
                    SongTitleLabel.TextColor = UIColor.White;
                    BackGround.BackgroundColor = UIColor.Black;
                    BackGround.Alpha = .2f;
                });

            }
            else
            {
                Animate(.2f, () =>
                {
                    Transform = CGAffineTransform.MakeScale(1.1f, 1.1f);
                    SongArtistLabel.TextColor = UIColor.Black;
                    SongTitleLabel.TextColor = UIColor.Black;
                    BackGround.BackgroundColor = UIColor.White;
                    BackGround.Alpha = .80f;
                });
            }
        }
    }
}
