using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace HangmanProject
{
    internal class Media
    {
        // Property signatures:
        public MediaPlayer LoseSound { get; private set; }
        public MediaPlayer WinCheers { get; private set; }

        // Ctor
        public Media() =>
            CreateMedia();
        
        // Methods
        private void CreateMedia()
        {
            LoseSound = new MediaPlayer() { AutoPlay = false, Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Media/loseSound.wav")), Volume = 0.5, IsLoopingEnabled = false };
            WinCheers = new MediaPlayer() { AutoPlay = false, Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Media/maleVoiceCheer.wav")), Volume = 1.0, IsLoopingEnabled = false };
        }
    }
}
