using EMFTV.Global;
using EMFTV.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMFTV.Services
{
    public class SignalRService
    {
        HttpClient client;

        public delegate void SongChangeReceivedHandler(object sender, SongModel song);
        public event SongChangeReceivedHandler SongChanged;

        public delegate void ConnectionHandler(object sender, bool successful, string message);
        public event ConnectionHandler Connected;
        public event ConnectionHandler ConnectionFailed;
        public bool IsConnected { get; private set; }
        public bool IsBusy { get; private set; }

        HubConnection connection;

        SongModel previousSong { get; set; }

        public SignalRService()
        {
            client = new HttpClient();
        }

        public async Task ConnectAsync()
        {
            try
            {
                IsBusy = true;
                //https://api.corpemf.com/music-player/v1/NowPlaying/negotiate?Hub=KloveLive

                var response = await client.PostAsync($"{Application.CData.SignalRBaseUrl}{Variables.CurrentChannel.HubName}", null);
                string negotiateJson = await response.Content.ReadAsStringAsync();
                NegotiateInfo negotiate = JsonConvert.DeserializeObject<NegotiateInfo>(negotiateJson);

                connection = new HubConnectionBuilder()
                    .WithAutomaticReconnect()
                    .AddNewtonsoftJsonProtocol()
                    .WithUrl(negotiate.Url, options =>
                    {
                        options.AccessTokenProvider = async () => negotiate.AccessToken;
                    })
                    .Build();

                connection.Closed += Connection_Closed;
                connection.On<JObject>("songChanged", AddNewMessage);
                await connection.StartAsync();

                IsConnected = true;
                IsBusy = false;
                Console.WriteLine("Signal R connection started.");
                Connected?.Invoke(this, true, "Connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Signal R connection failed.");
                ConnectionFailed?.Invoke(this, false, ex.Message);
                IsConnected = false;
                IsBusy = false;
            }
        }

        Task Connection_Closed(Exception arg)
        {
            Console.WriteLine("Signal R connection closed.");
            ConnectionFailed?.Invoke(this, false, arg.Message);
            IsConnected = false;
            IsBusy = false;
            return Task.CompletedTask;
        }
        void AddNewMessage(JObject message)
        {
            try
            {
                if (Variables.NowPlaying != null)
                {
                    previousSong = Variables.NowPlaying;
                }
                string id = message.GetValue("SongId").ToString();
                string title = message.GetValue("SongTitle").ToString();
                string artist = message.GetValue("ArtistName").ToString();
                string album = message.GetValue("AlbumTitle").ToString();
                string length = message.GetValue("SongLength").ToString();
                string songUrl = message.GetValue("SongUrl").ToString();
                string artistId = message.GetValue("ArtistId").ToString();
                string twitter = message.GetValue("ArtistTwitterUrl").ToString();
                string facebook = message.GetValue("ArtistFacebookUrl").ToString();
                string albumImage = message.GetValue("AlbumImage").ToString();
                Variables.NowPlaying = new SongModel
                {
                    Id = id,
                    SongTitle = title,
                    ArtistName = artist,
                    AlbumTitle = album,
                    SongLength = length,
                    SongUrl = songUrl,
                    ArtistId = artistId,
                    ArtistTwitterUrl = twitter,
                    ArtistFacebookUrl = facebook,
                    AlbumImage = albumImage,
                };

                if (previousSong != null)
                {
                    if (previousSong != Variables.NowPlaying)
                    {
                        Variables.PreviouslyPlayed.Insert(0, Variables.NowPlaying);
                        Variables.PreviouslyPlayed.RemoveAt(Variables.PreviouslyPlayed.Count - 1);
                    }
                }
                //App.STVM.CurrentSong = Variables.CurrentSong;
                SongChanged?.Invoke(this, Variables.NowPlaying);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sig R Obj Creation error" + e.Message);
            }
            Console.WriteLine(Variables.NowPlaying.SongTitle); 

        }
        public async Task Disconnect()
        {
            try
            {
                await connection.DisposeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error disposing sig r connection");
            }
        }
    }
}

//ASP.NET Signal R Example - tvos
//https://github.com/microsoft/uwp-experiences/tree/master/apps/music