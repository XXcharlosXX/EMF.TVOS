using System;
using System.Threading.Tasks;
using AVFoundation;
using EMFTV.Global;
using EMFTV.Models;
using Foundation;
using static EMF.IOS.Services.EventHandlers;
namespace EMFTV.Services
{
    public enum PlayerOption
    {
        Stream = 0,
        StreamAndSave
    }
    public class AudioService : NSObject
    {
        #region PUBLIC PROPERTIES

        public StatusType Status { get; set; }
        public SongModel CurrentSong { get; set; }

        public string AudioUrl { get; set; }
        public PlayerOption PlayerOption { get; private set; }
        AVAudioSession avSession;

        #endregion PUBLIC PROPERTIES


        #region PRIVATE PROPERTIES
        private NSObject _audioRouteNotification { get; set; }
        private NSObject _audioInterruptionNotification { get; set; }
        private ApplicationConfig _appConfig
        {
            get
            {
                return Application.Config;
            }
        }
        private EmfStreamPlayer _streamingPlayer { get; set; }

        #endregion PRIVATE PROPERTIES

        public AudioService()
        {
            AppDelegate.MediaPlayer = this;
        }
        private static AudioService _instance = new AudioService();

        //Static method which allows the instance creation  
        public static AudioService Instance()
        {
            //All you need to do it is just return the  
            //already initialized which is thread safe  
            return _instance;
        }

        public event StatusChangedEventHandler StatusChanged;

        #region EVENT

        protected virtual void OnStatusChanged(EventArgs e)
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, e);
            }
        }

        #endregion EVENT HANDLERS

        public enum StatusType
        {
            Stopped = 0,
            Buffering = 1,
            Playing = 2
        }

        //private void _streamingPlayer_StatusChanged(object sender, EventArgs e)
        //{
        //    Status = (StatusType)_streamingPlayer.PlayerStatus;
        //    OnStatusChanged(e);
        //}

        public async Task StartPlayback()
        {
            try
            {
                var url = new NSUrl(Variables.StreamUrl);
                _streamingPlayer.ReplaceCurrentItem(url);
                _streamingPlayer.ActionAtItemEnd = AVPlayerActionAtItemEnd.None;
                //await YouboraService.PostStartData(_appConfig.Network, Variables.CurrentChannel, url.ToString());
            }
            catch (Exception ex)
            {
            }
        }



        public async Task Initialize()
        {
            //await YouboraService.GetYouboraAPIParametersAsync();
            StopPlayback();
            if(avSession == null)
            {
                avSession = AVAudioSession.SharedInstance();
                avSession.SetActive(true);
                avSession.SetCategory(AVAudioSessionCategory.Playback);

                NSError activationError = null;
                avSession.SetActive(true, out activationError);
                if (activationError != null)
                Console.WriteLine("Could not activate audio session {0}", activationError.LocalizedDescription);
            }
            _streamingPlayer = new EmfStreamPlayer();
            //_streamingPlayer.StatusChanged += _streamingPlayer_StatusChanged;
            await StartPlayback();
        }

        public void StopPlayback()
        {
            try
            {
                if(_streamingPlayer != null)
                {
                    _streamingPlayer?.Pause();
                    Status = StatusType.Stopped;
                    OnStatusChanged(EventArgs.Empty);
                    _streamingPlayer?.Dispose();
                }
            }
            catch(ObjectDisposedException ODEx)
            {
                Console.WriteLine($"streamingPlayer - is already disposed. {ODEx.Message}");
            }
            catch (Exception ex)
            {
            }

        }
    }
}