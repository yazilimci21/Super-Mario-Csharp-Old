using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EKSuper_Mario.Core
{
    public class MarioSoundManager
    {
        [Flags]
        public enum SoundFlags
        {
            /// <summary>play synchronously (default)</summary>
            SND_SYNC = 0x0000,
            /// <summary>play asynchronously</summary>
            SND_ASYNC = 0x0001,
            /// <summary>silence (!default) if sound not found</summary>
            SND_NODEFAULT = 0x0002,
            /// <summary>pszSound points to a memory file</summary>
            SND_MEMORY = 0x0004,
            /// <summary>loop the sound until next sndPlaySound</summary>
            SND_LOOP = 0x0008,
            /// <summary>don't stop any currently playing sound</summary>
            SND_NOSTOP = 0x0010,
            /// <summary>Stop Playing Wave</summary>
            SND_PURGE = 0x40,
            /// <summary>The pszSound parameter is an application-specific alias in the registry. You can combine this flag with the SND_ALIAS or SND_ALIAS_ID flag to specify an application-defined sound alias.</summary>
            SND_APPLICATION = 0x80,
            /// <summary>don't wait if the driver is busy</summary>
            SND_NOWAIT = 0x00002000,
            /// <summary>name is a registry alias</summary>
            SND_ALIAS = 0x00010000,
            /// <summary>alias is a predefined id</summary>
            SND_ALIAS_ID = 0x00110000,
            /// <summary>name is file name</summary>
            SND_FILENAME = 0x00020000,
            /// <summary>name is resource name or atom</summary>
            SND_RESOURCE = 0x00040004
        }

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(byte[] pszSound, IntPtr hmod, SoundFlags fdwSound);

        private string hurt1, hurt2, yahoo1, yahoo2, bump, kick, coin, jump, pause, itemSprout, bonusPoints, healthUp, healthDown;

        public MarioSoundManager()
        {
            hurt1 = Application.StartupPath + "\\sounds\\mario_ooh.wav";
            hurt2 = Application.StartupPath + "\\sounds\\mario_oh.wav";
            yahoo1 = Application.StartupPath + "\\sounds\\mario_waha.wav";
            yahoo2 = Application.StartupPath + "\\sounds\\mario_woohoo.wav";
            bump = Application.StartupPath + "\\sounds\\bump.wav";
            kick = Application.StartupPath + "\\sounds\\kick.wav";
            coin = Application.StartupPath + "\\sounds\\coin.wav";
            jump = Application.StartupPath + "\\sounds\\jump.wav";
            pause = Application.StartupPath + "\\sounds\\pause.wav";
            itemSprout = Application.StartupPath + "\\sounds\\item_sprout.wav";
            bonusPoints = Application.StartupPath + "\\sounds\\veggie_throw.wav";
            healthUp = Application.StartupPath + "\\sounds\\power_up.wav";
            healthDown = Application.StartupPath + "\\sounds\\power_down.wav";
        }

        private void play(string file)
        {
            PlaySound(file, UIntPtr.Zero, (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC));
        }

        public void Play(byte[] waveData)
        {
            PlaySound(waveData, IntPtr.Zero, SoundFlags.SND_ASYNC | SoundFlags.SND_MEMORY);
        }

        public void playHealthUp()
        {
            play(healthUp);
        }

        public void playHealthDown()
        {
            play(healthDown);
        }

        public void playBonusPoints()
        {
            play(bonusPoints);
        }

        public void playItemSprout()
        {
            play(itemSprout);
        }

        public void playCoin()
        {
            play(coin);
        }

        public void playKick()
        {
            play(kick);
        }

        public void playBump()
        {
            play(bump);
        }

        public void playJump()
        {
            play(jump);
        }

        public void playPause()
        {
            play(pause);
        }

        public void playHurt()
        {
            Random r = new Random();
            int rNum = r.Next(2);
            if (rNum == 0)
            {
                play(hurt1);
            }
            else
            {
                play(hurt2);
            }
        }

        public void playCelebrate()
        {
            Random r = new Random();
            int rNum = r.Next(2);
            if (rNum == 0)
            {
                play(yahoo1);
            }
            else
            {
                play(yahoo2);
            }
        }
    }
}
