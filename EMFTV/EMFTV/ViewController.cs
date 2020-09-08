using System;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;
using EMFTV.Components;
using EMFTV.Global;
using EMFTV.Models;
using EMFTV.Services;
using Foundation;
using UIKit;

namespace EMFTV
{
    public partial class ViewController : UIViewController
    {
        //---------UI------------//
        UIButton ChannelsButton;
        UIButton StartStopButton;
        UIButton LyricsButton;

        UIImageView NowPlayingImageView;
        UIImageView RecentsImageView;
        UIImageView NowPlayingArtistImageView;
        UIImageView DarkGradientImageView;
        UIImageView TagLineImageView;

        UILabel ChannelTitleLabel;
        UILabel LastPlayedLabel;
        UILabel NowPlayingTitleLabel;
        UILabel NowPlayingArtistLabel;

        ChannelSelectionModal ChannelSelectionModal;
        LyricModal LyricModal;

        SongCard Recent1;
        SongCard Recent2;
        SongCard Recent3;
        SongCard Recent4;

        //---------Variables------------//
        int PlayCount = 0;
        SignalRService SR;
        ArtistModel NPArtist;

        public UIFocusGuide FocusGuide;

        public ViewController(IntPtr handle) : base(handle)
        {
            Console.WriteLine("constructor");
        }

        public override async void ViewDidLoad()
        {
            Console.WriteLine("viewdidload");

            Variables.ScreenHeight = View.Frame.Size.Height;
            Variables.ScreenWidth = View.Frame.Size.Width;

            await RemoteConfigService.GetCloudData("serviceconfig");
            await RemoteConfigService.GetAppConfig("kconfig");
            await MusicService.GetAndStoreRecentSongsAsync();
            if (!string.IsNullOrEmpty(Variables.NowPlaying.ArtistId))
            {
                NPArtist = await MusicService.GetArtistById(Convert.ToInt32(Variables.NowPlaying.ArtistId));
            }
            SR = new SignalRService();
            await SR.ConnectAsync();
            SR.SongChanged += SignalR_SongChange;


            if (NPArtist != null)
            {
                NowPlayingArtistImageView = new UIImageView(View.Frame);
                NowPlayingArtistImageView.Image = await Functions.LoadImage(NPArtist.ImageUrl);
                NowPlayingArtistImageView.Frame = new CGRect(x: 0, y: 0, width: Variables.ScreenWidth, height: Variables.ScreenWidth / 2);
                // Add the Image View to the parent view
                View.AddSubview(NowPlayingArtistImageView);

                DarkGradientImageView = new UIImageView(View.Frame);
                DarkGradientImageView.Image = UIImage.FromBundle("dark-gradient.png");
                DarkGradientImageView.Frame = new CGRect(x: 0, y: 0, width: Variables.ScreenWidth, height: Variables.ScreenHeight - ((Variables.ScreenHeight / 5) * 2));
                View.AddSubview(DarkGradientImageView);

                RecentsImageView = new UIImageView(View.Frame);
                UIImage xI = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
                RecentsImageView.Image = xI;
                RecentsImageView.Frame = new CGRect(x: 0, y: Variables.ScreenHeight - ((Variables.ScreenHeight / 5) * 2), width: Variables.ScreenWidth, height: (Variables.ScreenHeight / 5) * 2);

                // Add the Image View to the parent view
                View.AddSubview(RecentsImageView);
                var blur = UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark);
                float x = 0;
                float y = (float)(Variables.ScreenHeight - ((Variables.ScreenHeight / 5) * 2));
                float width = (float)Variables.ScreenWidth;
                float height = (float)(Variables.ScreenHeight / 5 * 2);
                var blurView = new UIVisualEffectView(blur)
                {
                    Frame = new RectangleF(x, y, width, height)
                };

                View.Add(blurView);
            }

            TagLineImageView = new UIImageView(View.Frame);
            TagLineImageView.Image = UIImage.FromBundle("kl-logo-tagline-white.png");
            TagLineImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            TagLineImageView.Frame = new CGRect(x: Variables.ScreenWidth * .042, y: Variables.ScreenHeight * .011, width: Variables.ScreenWidth * .13, height: Variables.ScreenWidth * .13);
            View.AddSubview(TagLineImageView);

            StartStopButton = new UIButton(UIButtonType.System);
            StartStopButton.SetTitle("Play", UIControlState.Normal);
            StartStopButton.TitleLabel.Font = UIFont.SystemFontOfSize(24, UIFontWeight.Bold);
            StartStopButton.TitleEdgeInsets = new UIEdgeInsets(0, -50, 0, -50);
            UIImage SSButtonIconImage = UIImage.FromBundle("play-fill.png");
            StartStopButton.SetImage(SSButtonIconImage, UIControlState.Normal);
            StartStopButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            StartStopButton.ImageEdgeInsets = new UIEdgeInsets(-5, 0, -5, 50);
            StartStopButton.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenHeight * .3) + (Variables.ScreenWidth * .02) + 32, y: Variables.ScreenHeight - ((Variables.ScreenHeight / 5) * 2) - 32, width: 220, height: 64);
            StartStopButton.PrimaryActionTriggered += async (sender, e) =>
            {
                await AudioControl();
            };
            View.AddSubview(StartStopButton);

            LyricsButton = new UIButton(UIButtonType.System);
            LyricsButton.TitleLabel.Font = UIFont.SystemFontOfSize(24, UIFontWeight.Bold);
            LyricsButton.SetTitle("Lyrics", UIControlState.Normal);
            LyricsButton.TitleEdgeInsets = new UIEdgeInsets(0, -50, 0, -50);
            //LyricsButton.ImageEdgeInsets = new UIEdgeInsets(-5, 0, -5, 50);
            //UIImage LyButtonIconImage = UIImage.FromBundle("lyrics.png");
            //LyricsButton.SetImage(LyButtonIconImage, UIControlState.Normal);
            //LyricsButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            LyricsButton.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenHeight * .3) + (Variables.ScreenWidth * .02) + 284, y: Variables.ScreenHeight - ((Variables.ScreenHeight / 5) * 2) - 32, width: 220, height: 64);
            LyricsButton.PrimaryActionTriggered += async (sender, e) =>
            {
                //await DisplayLyricsModal();
            };

            View.AddSubview(LyricsButton);
            ChannelsButton = new UIButton(UIButtonType.System);
            ChannelsButton.SetTitle("Channels", UIControlState.Normal);
            ChannelsButton.TitleLabel.Font = UIFont.SystemFontOfSize(24, UIFontWeight.Bold);
            ChannelsButton.TitleEdgeInsets = new UIEdgeInsets(0, -50, 0, -50);
            //UIImage ChButtonIconImage = UIImage.FromBundle("caret-down.png");
            //ChannelsButton.ImageEdgeInsets = new UIEdgeInsets(-5, 0, -5, 50);
            //ChannelsButton.SetImage(ChButtonIconImage, UIControlState.Normal);
            //ChannelsButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            ChannelsButton.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenHeight * .3) + (Variables.ScreenWidth * .02) + 536, y: Variables.ScreenHeight - ((Variables.ScreenHeight / 5) * 2) - 32, width: 220, height: 64);
            ChannelsButton.PrimaryActionTriggered += async (sender, e) =>
            {
                await DisplayChannelModal();
            };
            View.AddSubview(ChannelsButton);


            NowPlayingImageView = new UIImageView(View.Frame);
            NowPlayingImageView.Image = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
            NowPlayingImageView.Frame = new CGRect(x: Variables.ScreenWidth * .042, y: (Variables.ScreenHeight * .55) - ((Variables.ScreenWidth / 4.5) / 2), width: Variables.ScreenHeight * .3, height: Variables.ScreenHeight * .3);
            // Add the Image View to the parent view
            View.AddSubview(NowPlayingImageView);

            ChannelTitleLabel = new UILabel();
            ChannelTitleLabel.Text = Variables.CurrentChannel.Title.ToUpper();
            ChannelTitleLabel.Font = UIFont.SystemFontOfSize(24);
            ChannelTitleLabel.TextColor = UIColor.White;
            ChannelTitleLabel.Alpha = .5f;
            ChannelTitleLabel.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenHeight * .3) + (Variables.ScreenWidth * .02), y: (Variables.ScreenHeight * .55) - ((Variables.ScreenWidth / 4.5) / 2), width: Variables.ScreenWidth / 2, height: 46);
            View.AddSubview(ChannelTitleLabel);

            NowPlayingTitleLabel = new UILabel();
            NowPlayingTitleLabel.Text = Variables.NowPlaying.SongTitle;
            NowPlayingTitleLabel.Font = UIFont.SystemFontOfSize(60, UIFontWeight.Bold);
            NowPlayingTitleLabel.TextColor = UIColor.White;
            NowPlayingTitleLabel.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenHeight * .3) + (Variables.ScreenWidth * .02), y: ((Variables.ScreenHeight * .55) - ((Variables.ScreenWidth / 4.5) / 2)) + 31, width: (Variables.ScreenWidth / 3) * 2, height: 100);
            View.AddSubview(NowPlayingTitleLabel);

            NowPlayingArtistLabel = new UILabel();
            NowPlayingArtistLabel.Text = Variables.NowPlaying.ArtistName;
            NowPlayingArtistLabel.Font = UIFont.SystemFontOfSize(48);
            NowPlayingArtistLabel.TextColor = UIColor.White;
            NowPlayingArtistLabel.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenHeight * .3) + (Variables.ScreenWidth * .02), y: ((Variables.ScreenHeight * .55) - ((Variables.ScreenWidth / 4.5) / 2)) + 108, width: Variables.ScreenWidth / 2, height: 60);
            View.AddSubview(NowPlayingArtistLabel);

            LastPlayedLabel = new UILabel();
            LastPlayedLabel.Text = "Last Played";
            LastPlayedLabel.Font = UIFont.SystemFontOfSize(36, UIFontWeight.Bold);
            LastPlayedLabel.TextColor = UIColor.White;
            LastPlayedLabel.Frame = new CGRect(x: Variables.ScreenWidth * .042, y: (Variables.ScreenHeight * .7), width: Variables.ScreenWidth / 4, height: 54);
            View.AddSubview(LastPlayedLabel);

            await UpdateLastPlayedList();

            FocusGuide = new UIFocusGuide();
            View.AddLayoutGuide(FocusGuide);

            FocusGuide.LeftAnchor.ConstraintEqualTo(Recent4.LeftAnchor).Active = true;
            FocusGuide.TopAnchor.ConstraintEqualTo(ChannelsButton.TopAnchor).Active = true;
            FocusGuide.WidthAnchor.ConstraintEqualTo(Recent4.WidthAnchor).Active = true;
            FocusGuide.HeightAnchor.ConstraintEqualTo(ChannelsButton.HeightAnchor).Active = true;

            FocusGuide.PreferredFocusedView = StartStopButton;

            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }
        public async Task UpdateLastPlayedList()
        {
            for (int i = 1; i < Variables.PreviouslyPlayed.Count; i++)
            {
                switch (i)
                {
                    case 1:
                        if (Recent1 != null)
                        {
                            Recent1.RemoveFromSuperview();
                            Recent1.Dispose();
                        }
                        UIImage one = await Functions.LoadImage(Variables.PreviouslyPlayed[1].AlbumImage);
                        Recent1 = new SongCard(one, Variables.PreviouslyPlayed[1], (float)(Variables.ScreenWidth * .042f), (float)(Variables.ScreenHeight * .75f), (float)(Variables.ScreenWidth * .20f), (float)(Variables.ScreenHeight * .12f));
                        Recent1.SongClicked += songCardClicked;
                        Recent1.Frame = new CGRect(x: Variables.ScreenWidth * .042, y: (Variables.ScreenHeight * .775), width: (float)(Variables.ScreenWidth * .20f), height: (float)(Variables.ScreenHeight * .12f));
                        //Recent1.PresentLyrics += asynx;
                        View.AddSubview(Recent1);
                        break;
                    case 2:
                        if (Recent2 != null)
                        {
                            Recent2.RemoveFromSuperview();
                            Recent2.Dispose();
                        }
                        UIImage two = await Functions.LoadImage(Variables.PreviouslyPlayed[2].AlbumImage);
                        Recent2 = new SongCard(two, Variables.PreviouslyPlayed[2], (float)(Variables.ScreenWidth * .042f), (float)(Variables.ScreenHeight * .75f), (float)(Variables.ScreenWidth * .20f), (float)(Variables.ScreenHeight * .12f));
                        Recent2.SongClicked += songCardClicked;
                        Recent2.Frame = new CGRect(x: Variables.ScreenWidth * .042 + (Variables.ScreenWidth * .20f) + Variables.ScreenWidth * .03, y: (Variables.ScreenHeight * .775), width: (float)(Variables.ScreenWidth * .20f), height: (float)(Variables.ScreenHeight * .12f));
                        View.AddSubview(Recent2);
                        break;
                    case 3:
                        if (Recent3 != null)
                        {
                            Recent3.RemoveFromSuperview();
                            Recent3.Dispose();
                        }
                        UIImage three = await Functions.LoadImage(Variables.PreviouslyPlayed[3].AlbumImage);
                        Recent3 = new SongCard(three, Variables.PreviouslyPlayed[3], (float)(Variables.ScreenWidth * .042f), (float)(Variables.ScreenHeight * .75f), (float)(Variables.ScreenWidth * .20f), (float)(Variables.ScreenHeight * .12f));
                        Recent3.SongClicked += songCardClicked;
                        Recent3.Frame = new CGRect(x: Variables.ScreenWidth * .042 + ((Variables.ScreenWidth * .20f) * 2) + ((Variables.ScreenWidth * .03) * 2), y: (Variables.ScreenHeight * .775), width: (float)(Variables.ScreenWidth * .20f), height: (float)(Variables.ScreenHeight * .12f));
                        View.AddSubview(Recent3);
                        break;
                    case 4:
                        if (Recent4 != null)
                        {
                            Recent4.RemoveFromSuperview();
                            Recent4.Dispose();
                        }
                        UIImage four = await Functions.LoadImage(Variables.PreviouslyPlayed[4].AlbumImage);
                        Recent4 = new SongCard(four, Variables.PreviouslyPlayed[4], (float)(Variables.ScreenWidth * .042f), (float)(Variables.ScreenHeight * .75f), (float)(Variables.ScreenWidth * .20f), (float)(Variables.ScreenHeight * .12f));
                        Recent4.SongClicked += songCardClicked;
                        Recent4.Frame = new CGRect(x: Variables.ScreenWidth * .042 + ((Variables.ScreenWidth * .20f) * 3) + ((Variables.ScreenWidth * .03) * 3), y: (Variables.ScreenHeight * .775), width: (float)(Variables.ScreenWidth * .20f), height: (float)(Variables.ScreenHeight * .12f));
                        View.AddSubview(Recent4);
                        break;
                }
            }
        }

        async void SignalR_SongChange(object sender, SongModel song)
        {
            Console.WriteLine($"SignalR song change: {song.SongTitle}");
            try
            {
                await Task.Delay(20000);
                await MusicService.GetAndStoreRecentSongsAsync();
                if (!string.IsNullOrEmpty(Variables.NowPlaying.ArtistId))
                {
                    NPArtist = await MusicService.GetArtistById(Convert.ToInt32(Variables.NowPlaying.ArtistId));
                    NowPlayingArtistImageView.Image = await Functions.LoadImage(NPArtist.ImageUrl);
                }

                NowPlayingTitleLabel.Text = Variables.NowPlaying.SongTitle;
                NowPlayingArtistLabel.Text = Variables.NowPlaying.ArtistName;
                NowPlayingImageView.Image = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
                RecentsImageView.Image = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
                await UpdateLastPlayedList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SigR Song change catch:" + e.Message);
            }

        }

        public override void DidUpdateFocus(UIFocusUpdateContext context, UIFocusAnimationCoordinator coordinator)
        {
            base.DidUpdateFocus(context, coordinator);

            // Get next focusable item from context
            var nextFocusableItem = context.NextFocusedView;


            //Anything to process ?
            if (nextFocusableItem == null) return;

            // Decide the next focusable item based on the current
            // item with focus
            if (nextFocusableItem == ChannelsButton)
            {
                // Move from the More Info to Buy button
                FocusGuide.PreferredFocusedView = Recent4;
            }
            else if (nextFocusableItem == Recent4)
            {
                // Move from the Buy to the More Info button
                FocusGuide.PreferredFocusedView = ChannelsButton;
            }
            else
            {
                // No valid move
                FocusGuide.PreferredFocusedView = null;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public async Task DisplayChannelModal()
        {
            if (ChannelSelectionModal == null)
            {

                UIImage xI = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
                ChannelSelectionModal = new ChannelSelectionModal(xI);
                ChannelSelectionModal.ChannelSelected += ChannelSelected;
            }
            await PresentViewControllerAsync(ChannelSelectionModal, true);
        }

        async void songCardClicked(object sender, SongModel song)
        {
            // SONG CLICKED
            Console.WriteLine($"We have Fire baby{song.SongTitle}");
            string lyrics = await MusicService.GetLyricsBySongId(song.SongId);
            if (string.IsNullOrEmpty(lyrics))
            {
                Console.WriteLine("no lyrics");
                return;
            }
            if (LyricModal != null)
            {
                LyricModal.Dispose();
                LyricModal = null;
            }

            if (LyricModal == null)
            {
                UIImage xI = await Functions.LoadImage(song.AlbumImage);
                LyricModal = new LyricModal(lyrics, xI, song);
            }
            await PresentViewControllerAsync(LyricModal, true);

        }

        async void ChannelSelected(object sender, Channel ch)
        {
            // SONG CLICKED
            DismissViewController(true, null);
            await MusicService.GetAndStoreRecentSongsAsync();
            if (!string.IsNullOrEmpty(Variables.NowPlaying.ArtistId))
            {
                NPArtist = await MusicService.GetArtistById(Convert.ToInt32(Variables.NowPlaying.ArtistId));
                NowPlayingArtistImageView.Image = await Functions.LoadImage(NPArtist.ImageUrl);
            }
            ChannelTitleLabel.Text = Variables.CurrentChannel.Title.ToUpper();
            NowPlayingTitleLabel.Text = Variables.NowPlaying.SongTitle;
            NowPlayingArtistLabel.Text = Variables.NowPlaying.ArtistName;
            NowPlayingImageView.Image = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
            RecentsImageView.Image = await Functions.LoadImage(Variables.NowPlaying.AlbumImage);
            await SR.Disconnect();
            await SR.ConnectAsync();
            await UpdateLastPlayedList();
        }

        async Task AudioControl()
        {
            PlayCount++;
            if (PlayCount > 1)
            {
                try
                {
                    AppDelegate.MediaPlayer.StopPlayback();
                    PlayCount = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error:{ex.Message}");
                }
                try
                {
                    StartStopButton.SetTitle("Play", UIControlState.Normal);
                    StartStopButton.SetImage(UIImage.FromBundle("play-fill.png"), UIControlState.Normal);
                    Console.WriteLine("clicked");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    await StreamService.GetStreamUrlAsync();
                    await AppDelegate.MediaPlayer.Initialize();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error:{ex.Message}");
                }
                try
                {
                    StartStopButton.SetTitle("", UIControlState.Normal);
                    StartStopButton.SetImage(null, UIControlState.Normal);
                    var AI = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White);
                    StartStopButton.AddSubview(AI);
                    AI.Center = new CGPoint(StartStopButton.Frame.Size.Width / 2, StartStopButton.Frame.Size.Height / 2);
                    AI.StartAnimating();
                    await Task.Delay(3000);
                    AI.StopAnimating();
                    StartStopButton.SetTitle("Stop", UIControlState.Normal);
                    StartStopButton.SetImage(UIImage.FromBundle("stop-fill.png"), UIControlState.Normal);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}




