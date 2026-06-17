using System;
using System.Media;
namespace HYMAPSOPIR
{
    public class AudioTerkirim : IObserver
    {
        public void Update(string suara)
        {
            try
            {
                string audioPath = "cash-register-sound-fx.wav";

                using (SoundPlayer player = new SoundPlayer(audioPath))
                {
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                Console.Beep(1000, 500);
            }
        }
    }
}