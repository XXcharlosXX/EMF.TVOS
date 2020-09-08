using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMFTV.Models;
using EMFTV.Services;

namespace EMFTV.Global
{
    public static class Variables
    {
        static List<SongModel> PP;
        public static List<SongModel> PreviouslyPlayed
        {
            get
            {
                if (PP == null)
                {
                    Task.Run(MusicService.GetAndStoreRecentSongsAsync);
                }
                return PP;
            }
            set
            {
                PP = value;
            }
        }
        static Channel selectedChannel;
        public static Channel CurrentChannel
        {
            get
            {
                return selectedChannel;
            }
            set
            {
                selectedChannel = value;
            }
        }
        private static SongModel NP;
        public static SongModel NowPlaying
        {
            get
            {
                if (NP == null)
                {
                    Task.Run(MusicService.GetAndStoreRecentSongsAsync);
                }
                return NP;
            }
            set
            {
                NP = value;
            }
        }

        public static string StreamUrl { get; set; }

        public static Dictionary<int, string> Networks = new Dictionary<int, string>
        {
            { 1, "k-love" },
            { 100, "k-love-christmas" },
            { 300, "k-love-classics" },
            { 2, "air1" },
            { 101, "air1-christmas"},
        };
        public static nfloat ScreenWidth { get; set; }
        public static nfloat ScreenHeight { get; set; }
    }
}
