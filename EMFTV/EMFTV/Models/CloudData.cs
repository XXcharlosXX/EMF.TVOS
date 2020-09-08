using System;

namespace EMFTV.Models
{
    public class CloudData
    {
        public CloudData()
        {
        }
        /*----------------------------------ACD----------------------------------*/
        private string KRAPR;
        public string KReportAProblemRecipient
        {
            get
            {
                return KRAPR;
            }
            set
            {
                KRAPR = value;
            }
        }
        private string ARAPR;
        public string AReportAProblemRecipient
        {
            get
            {
                return ARAPR;
            }
            set
            {
                ARAPR = value;
            }
        }
        
        //ArtistService.cs
        private string ASbaseUrl;
        public string ArtistServiceBaseUrl
        {
            get
            {
                return ASbaseUrl;
            }
            set
            {
                ASbaseUrl = value;
            }
        }
        private string ASSearchEndpoint;
        public string ArtistServiceSearchEndpoint
        {
            get
            {
                return ASSearchEndpoint;
            }
            set
            {
                ASSearchEndpoint = value;
            }
        }
        private string ASDetailEndpoint;
        public string ArtistServiceDetailEndpoint
        {
            get
            {
                return ASDetailEndpoint;
            }
            set
            {
                ASDetailEndpoint = value;
            }
        }
        //BroadcastService
        private string BSbaseUrl;
        public string BroadcastServiceBaseUrl
        {
            get
            {
                return BSbaseUrl;
            }
            set
            {
                BSbaseUrl = value;
            }
        }
        private string BSRecentSongsEndpoint;
        public string BroadcastServiceRecentSongsEndpoint
        {
            get
            {
                return BSRecentSongsEndpoint;
            }
            set
            {
                BSRecentSongsEndpoint = value;
            }
        }
        private string BSSongInfoEndpoint;
        public string BroadcastServiceSongInfoEndpoint
        {
            get
            {
                return BSSongInfoEndpoint;
            }
            set
            {
                BSSongInfoEndpoint = value;
            }
        }
        private string BSSongIdEndpoint;
        public string BroadcastServiceSongIdEndpoint
        {
            get
            {
                return BSSongIdEndpoint;
            }
            set
            {
                BSSongIdEndpoint = value;
            }
        }
        //Privacy Policy
        private string PSBaseUrl;
        public string PrivacyServiceBaseUrl
        {
            get
            {
                return PSBaseUrl;
            }
            set
            {
                PSBaseUrl = value;
            }
        }
        private string KPSContentId;
        public string KPrivacyServiceContentId
        {
            get
            {
                return KPSContentId;
            }
            set
            {
                KPSContentId = value;
            }
        }
        private string APSContentId;
        public string APrivacyServiceContentId
        {
            get
            {
                return APSContentId;
            }
            set
            {
                APSContentId = value;
            }
        }
        //StationService
        private string SSBaseUrl;
        public string StationServiceBaseUrl
        {
            get
            {
                return SSBaseUrl;
            }
            set
            {
                SSBaseUrl = value;
            }
        }
        private string SSGetSignalsLatLongEndpoint;
        public string StationServiceGetSignalsLatLongEndpoint
        {
            get
            {
                return SSGetSignalsLatLongEndpoint;
            }
            set
            {
                SSGetSignalsLatLongEndpoint = value;
            }
        }
        private string SSHV;
        public string SSHeaderValue
        {
            get
            {
                return SSHV;
            }
            set
            {
                SSHV = value;
            }
        }
        private string SSHK;
        public string SSHeaderKey
        {
            get
            {
                return SSHK;
            }
            set
            {
                SSHK = value;
            }
        }
        //VerseService
        private string VSBaseUrl;
        public string VerseServiceBaseUrl
        {
            get
            {
                return VSBaseUrl;
            }
            set
            {
                VSBaseUrl = value;
            }
        }
        private string VSGetVerseEndpoint;
        public string VerseServiceGetVerseEndpoint
        {
            get
            {
                return VSGetVerseEndpoint;
            }
            set
            {
                VSGetVerseEndpoint = value;
            }
        }
        private string VSGetVerseListEndpoint;
        public string VerseServiceGetVerseListEndpoint
        {
            get
            {
                return VSGetVerseListEndpoint;
            }
            set
            {
                VSGetVerseListEndpoint = value;
            }
        }
        private string VSHV;
        public string VSHeaderValue
        {
            get
            {
                return VSHV;
            }
            set
            {
                VSHV = value;
            }
        }
        private string VSHK;
        public string VSHeaderKey
        {
            get
            {
                return VSHK;
            }
            set
            {
                VSHK = value;
            }
        }
        private string SSU;
        public string SubscriptionServiceUrl
        {
            get
            {
                return SSU;
            }
            set
            {
                SSU = value;
            }
        }
        private string SuSHV;
        public string SubscriptionServiceHeaderValue
        {
            get
            {
                return SuSHV;
            }
            set
            {
                SuSHV = value;
            }
        }
        private string SuSHK;
        public string SubscriptionServiceHeaderKey
        {
            get
            {
                return SuSHK;
            }
            set
            {
                SuSHK = value;
            }
        }
        /*--------- URLs outside of More Page ---------*/
        private string SITUrl;
        public string SongImageThumbnailUrl
        {
            get
            {
                return SITUrl;
            }
            set
            {
                SITUrl = value;
            }
        }
        private string SAIUrl;
        public string SongAlbumImageUrl
        {
            get
            {
                return SAIUrl;
            }
            set
            {
                SAIUrl = value;
            }
        }
        private string VS;
        public string VersionString
        {
            get
            {
                return VS;
            }
            set
            {
                VS = value;
            }
        }
        private string UD;
        public string UpdateDescription
        {
            get
            {
                return UD;
            }
            set
            {
                UD = value;
            }
        }
        private string KPU;
        public string KPrivacyUrl
        {
            get
            {
                return KPU;
            }
            set
            {
                KPU = value;
            }
        }
        private string APU;
        public string APrivacyUrl
        {
            get
            {
                return APU;
            }
            set
            {
                APU = value;
            }
        }
        private string LSU;
        public string LocationServiceUrl
        {
            get
            {
                return LSU;
            }
            set
            {
                LSU = value;
            }
        }
        private string LSHV;
        public string LSHeaderValue
        {
            get
            {
                return LSHV;
            }
            set
            {
                LSHV = value;
            }
        }
        private string AH;
        public string AuthorizationHeader
        {
            get
            {
                return AH;
            }
            set
            {
                AH = value;
            }
        }
        private string DK;
        public string DebugKey
        {
            get
            {
                return DK;
            }
            set
            {
                DK = value;
            }
        }
        private int SCTO;
        public int StreamCacheTimeOut
        {
            get
            {
                return SCTO;
            }
            set
            {
                SCTO = value;
            }
        }

        private int LCTO;
        public int LocationCacheTimeOut
        {
            get
            {
                return LCTO;
            }
            set
            {
                LCTO = value;
            }
        }
        private string SOSBU;
        public string SearchOnSpotifyBaseUrl
        {
            get
            {
                return SOSBU;
            }
            set
            {
                SOSBU = value;
            }
        }
        private string LSBU;
        public string LocationServiceBaseUrl
        {
            get
            {
                return LSBU;
            }
            set
            {
                LSBU = value;
            }
        }
        private string LSGLE;
        public string LocationServiceGetLocationEndpoint
        {
            get
            {
                return LSGLE;
            }
            set
            {
                LSGLE = value;
            }
        }
        private string LSGIE;
        public string LocationServiceGetIpEndpoint
        {
            get
            {
                return LSGIE;
            }
            set
            {
                LSGIE = value;
            }
        }
        private string AAAV;
        public string AllAccessAuthorizationValue
        {
            get
            {
                return AAAV;
            }
            set
            {
                AAAV = value;
            }
        }
        private string SRBU;
        public string SignalRBaseUrl
        {
            get
            {
                return SRBU;
            }
            set
            {
                SRBU = value;
            }
        }
        
        private string MSBU;
        public string MusicServiceBaseUrl
        {
            get
            {
                return MSBU;
            }
            set
            {
                MSBU = value;
            }
        }
        private string LPE;
        public string LastPlayedEndpoint
        {
            get
            {
                return LPE;
            }
            set
            {
                LPE = value;
            }
        }
        private string AE;
        public string ArtistsEndpoint
        {
            get
            {
                return AE;
            }
            set
            {
                AE = value;
            }
        }
        private string SE;
        public string SongsEndpoint
        {
            get
            {
                return SE;
            }
            set
            {
                SE = value;
            }
        }
    }
}
