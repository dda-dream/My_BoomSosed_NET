using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_BoomSosed_NET
{
    class SoundPlayer
    {
        FormController formController;
        public float soundVolume { get; set; }
        public bool soundPlaying { get; set; }
        CheckBox ctrl_RandomVolume;
        WaveOutEvent outputDevice;
        AudioFileReader audioFile;

        public SoundPlayer(FormController formController, CheckBox ctrl_RandomVolume)
        {
            this.formController = formController;
            this.ctrl_RandomVolume = ctrl_RandomVolume;
            outputDevice = new WaveOutEvent();
        }
        public void PlaySound(string filePath)
        {
            var audioFilePath = Path.GetDirectoryName(Application.ExecutablePath) + filePath;
            if (!File.Exists(audioFilePath))
            {
                formController.LoggerAdd($"PlayMp3: File not exist {filePath}");
                return;
            }

            soundVolume = ctrl_RandomVolume.Checked ? (float)Random.Shared.NextDouble() : 1;
            
            audioFile = new AudioFileReader(audioFilePath);
            
            formController.LoggerAdd($"Boom! {filePath} vol: {(int)(soundVolume * 100)} sec: {(int)audioFile.TotalTime.TotalSeconds}");
            if (outputDevice.PlaybackState != PlaybackState.Playing)
            {
                soundPlaying = true;
                outputDevice.Init(audioFile);
                outputDevice.Play();
                outputDevice.Volume = soundVolume;
                outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
            } 
        }
        public void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            outputDevice.PlaybackStopped -= OutputDevice_PlaybackStopped;
            soundPlaying = false;
            formController.LoggerAdd($"Playback Stopped.");
            audioFile.Dispose();
        }

    }
}
