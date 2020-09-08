using System;
using System.Collections.Generic;
using AVFoundation;
using Foundation;
using static EMF.IOS.Services.EventHandlers;

namespace EMFTV.Services
{
    public class EmfStreamPlayer : AVPlayer
    {
        int PlayCount = 0;
        public AVPlayerItem streamingItem;
        public event StatusChangedEventHandler StatusChanged;
        public int PlayerStatus { get; set; }

        public EmfStreamPlayer()
        {
            AddObserver(this, new NSString("status"), 0, new IntPtr(0));
            AddObserver(this, new NSString("rate"), 0, new IntPtr(0));
        }

        public EmfStreamPlayer(NSUrl url) : base(url)
        {
            AddObserver(this, new NSString("status"), 0, new IntPtr(0));
            AddObserver(this, new NSString("rate"), 0, new IntPtr(0));
        }

        public void ReplaceCurrentItem(NSUrl url)
        {
            if (CurrentItem != null)
            {
                CurrentItem.RemoveObserver(this, new NSString("status"));
                CurrentItem.RemoveObserver(this, new NSString("rate"));
            }
            streamingItem = AVPlayerItem.FromUrl(url);
            streamingItem.AddObserver(this, new NSString("rate"), 0, new IntPtr(0));
            streamingItem.AddObserver(this, new NSString("status"), 0, new IntPtr(0));
            ReplaceCurrentItemWithPlayerItem(streamingItem);
        }

        protected virtual void OnStatusChanged(EventArgs e, int status)
        {
            PlayerStatus = status;
            //if (App.SM.PlayerState != status)
            //{
            //    App.SM.PlayerState = status;
            //    StatusChanged?.Invoke(this, e);
            //}
        }

        public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {

            try
            {
                if (keyPath == "status")
                {
                    if (Status == AVPlayerStatus.ReadyToPlay)
                    {
                        PlayCount++;
                        if (PlayCount >= 2)
                        {
                            return;
                        }
                        Console.WriteLine("Initializing Stream...");
                        Play();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (keyPath == "rate")
                {
                    Console.WriteLine($"playback rate: {Rate}");
                    if(Rate == 0)
                    {
                        PlayCount = 0;
                    }
                    OnStatusChanged(EventArgs.Empty, (int)Rate);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}