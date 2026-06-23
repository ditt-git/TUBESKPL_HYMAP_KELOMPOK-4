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
                string audioPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cash-register-sound-fx.wav");

                using (SoundPlayer player = new SoundPlayer(audioPath))
                {
                    player.Play();
                }
            }
            catch (Exception)
            {
                Console.Beep(1000, 500);
            }
        }
    }
}