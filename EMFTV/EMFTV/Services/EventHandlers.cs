using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace EMF.IOS.Services
{
    public class EventHandlers
    {
        public delegate void RecentSongsCloseEventHandler(object sender, EventArgs e);

        public delegate void StatusChangedEventHandler(object sender, EventArgs e);

        public delegate void SongChangedEventHandler(object sender, EventArgs e);

        public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);

        public delegate void ChangeChannelEventhandler(object sender, EventArgs e);

        public class ConnectivityChangedEventArgs : EventArgs
        {
            public bool IsConnected { get; set; }


        }

    }
}