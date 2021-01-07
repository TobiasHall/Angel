using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Angel
{
    static class MediaHelper
    {
        public static readonly MediaPlayer backgroundPlayer = new MediaPlayer();
        public static readonly MediaPlayer soundEffectPlayer = new MediaPlayer();
        public static double volume = 1;
        public static bool Muted = false;




        public static void PlayMedia(MediaPlayer mediaPlayer, Uri uri)
        {
            if (mediaPlayer == backgroundPlayer)
            {
                backgroundPlayer.Open(uri);
                //backgroundPlayer.IsMuted = Muted;
                backgroundPlayer.Play();
            }
            else
            {
                soundEffectPlayer.Open(uri);
                //soundEffectPlayer.IsMuted = Muted;
                soundEffectPlayer.Play();
            }
        }
    }
}
