using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EMFTV.Global;
using EMFTV.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMFTV.Services
{
    public class MusicService
    {
        private static readonly HttpClient client = new HttpClient();

        public async static Task GetAndStoreRecentSongsAsync()
        {
            var url = $"{Application.CData.MusicServiceBaseUrl}{Application.CData.LastPlayedEndpoint}" + $"{Variables.CurrentChannel.SourceId}?limit=5";
            client.DefaultRequestHeaders.Add(Application.CData.AuthorizationHeader, Application.CData.AllAccessAuthorizationValue);
            try
            {
                var y = await client.GetStringAsync(url);

                if (y != null)
                {

                    var x = JsonConvert.DeserializeObject<List<SongModel>>(y.ToString());
                    Variables.NowPlaying = x[0];
                    Variables.PreviouslyPlayed = x;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static async Task<ArtistModel> GetArtistById(int id)
        {
            var url = $"{Application.CData.MusicServiceBaseUrl}{Application.CData.ArtistsEndpoint}" + $"{id}";
            client.DefaultRequestHeaders.Add(Application.CData.AuthorizationHeader, Application.CData.AllAccessAuthorizationValue);
            try
            {
                var y = await client.GetStringAsync(url);

                if (y != null)
                {

                    return JsonConvert.DeserializeObject<ArtistModel>(y.ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task GetSongById(int id)
        {
            var url = $"{Application.CData.MusicServiceBaseUrl}{Application.CData.SongsEndpoint}" + $"{id}";
            client.DefaultRequestHeaders.Add(Application.CData.AuthorizationHeader, Application.CData.AllAccessAuthorizationValue);
            try
            {
                var y = await client.GetStringAsync(url);

                if (y != null)
                {

                    var x = JsonConvert.DeserializeObject<SongModel>(y.ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static async Task<List<ArtistSong>> GetArtistSongsById(int id)
        {
            var url = $"{Application.CData.MusicServiceBaseUrl}{Application.CData.ArtistsEndpoint}{id}/{Application.CData.SongsEndpoint}";
            client.DefaultRequestHeaders.Add(Application.CData.AuthorizationHeader, Application.CData.AllAccessAuthorizationValue);
            try
            {
                var y = await client.GetStringAsync(url);

                if (y != null)
                {

                    var z = JsonConvert.DeserializeObject<List<ArtistSong>>(y.ToString());
                    return z;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<string> GetLyricsBySongId(string id)
        {
            var url = $"{Application.CData.MusicServiceBaseUrl}{Application.CData.SongsEndpoint}{id}";
            client.DefaultRequestHeaders.Add(Application.CData.AuthorizationHeader, Application.CData.AllAccessAuthorizationValue);
            string LFID = null;
            try
            {
                var y = await client.GetStringAsync(url);

                if (y != null)
                {
                    var x = JsonConvert.DeserializeObject<ArtistSong>(y.ToString());
                    LFID = x.LyricFindId;
                }
            }
            catch (Exception ex)
            {
                LFID = null;
            }
            client.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrEmpty(LFID))
            {
                string LFUrl = $"https://api.lyricfind.com/lyric.do?apikey=9310e5ed49928d25aa06b16f4ab73895&territory=US&reqtype=default&trackid=lfid:{LFID}&output=json";
                try
                {
                    var y = await client.GetStringAsync(LFUrl);

                    if (y != null)
                    {

                        var x = JsonConvert.DeserializeObject<LyricFindResponse>(y.ToString());
                        if (x.Response.Code == 101 || x.Response.Code == 111)
                        {
                            return x.Track.Lyrics;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    LFID = null;
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
