using System.Collections.Generic;

namespace EMFTV.Models
{
    public class ApplicationConfig
    {   

        public string VOTDURL { get; set; }

        public NetworkType Network { get; set; }

        public int GoogleAdMaxCount { get; set; }

        public string GoogleAdUnitId { get; set; }

        public string DefaultStreamUrl { get; set; }

        public string GlobalStreamUrl { get; set; }

        public long AppLocationUpdateInterval { get; set; }

        public bool ChannelNavigationEnabled { get; set; }

        public List<Channel> Channels { get; set; }

        public int SelectedNetworkId { get; set; }

        public Channel SelectedChannel { get; set; }

        public bool VerseNotificationShowVerse { get; set; }

        public bool LyricsEnabled { get; set; }

        public string StreamServiceApi { get; set; }

        public string HookDirectory { get; set; }

        public string NetworkFromEmail { get; set; }
    }
}
