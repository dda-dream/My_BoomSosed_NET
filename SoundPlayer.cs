using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace My_BoomSosed_NET
{
    class SoundPlayer
    {
        FormController formController;
        float volumeAmplifier = 1;
        public bool soundPlaying { get; set; }
        CheckBox ctrl_RandomVolume;
        WaveOutEvent outputDevice;
        AudioFileReader? audioFile;
        TextBox ctrl_volumeAmplifier;
        private int currentRepeat = 0;
        private int totalRepeats = 0;
        private string? currentFilePath;


        public SoundPlayer(FormController formController, CheckBox ctrl_RandomVolume, TextBox ctrl_volumeAmplifier)
        {
            if (formController == null)
                throw new ArgumentNullException("FormController is null");

            this.formController = formController;
            this.ctrl_RandomVolume = ctrl_RandomVolume;
            outputDevice = new WaveOutEvent();
            this.ctrl_volumeAmplifier = ctrl_volumeAmplifier;
        }

        public void PlaySound(string filePath, int repeatCount = 1)
        {
            var audioFilePath = Path.GetDirectoryName(Application.ExecutablePath) + filePath;
            if (!File.Exists(audioFilePath))
            {
                formController.LoggerAdd($"PlayMp3: File not exist {filePath}");
                return;
            }

            float soundVolume = ctrl_RandomVolume.Checked ? (float)Random.Shared.NextDouble() : 1;

            audioFile = new AudioFileReader(audioFilePath);
            var volumeProvider = new VolumeSampleProvider(audioFile);

            if (float.TryParse(ctrl_volumeAmplifier.Text, out volumeAmplifier))
                volumeAmplifier = volumeAmplifier / 100;
            else
                volumeAmplifier = 1;

            volumeProvider.Volume = volumeAmplifier;

            double rounded = Math.Round(this.GetSoundLength(audioFilePath), 1);
            formController.LoggerAdd($"Boom! {filePath} vol: {(int)(soundVolume * 100)} volAmpl: {(int)(volumeAmplifier * 100)} sec: {rounded:0.0}");

            if (currentRepeat == 0)
            {
                currentRepeat = 1;
                totalRepeats = repeatCount;
                currentFilePath = filePath;
            }

            soundPlaying = true;
            outputDevice.Init(volumeProvider);
            outputDevice.Play();
            outputDevice.Volume = soundVolume;
            outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
        }

        public void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            outputDevice.PlaybackStopped -= OutputDevice_PlaybackStopped;
            audioFile?.Dispose();

            if (currentRepeat < totalRepeats)
            {
                currentRepeat++;
                //formController.LoggerAdd($"Repeat {currentRepeat}/{totalRepeats}");
                PlaySound(currentFilePath, totalRepeats);
                return;
            }

            soundPlaying = false;
            formController.LoggerAdd("Playback Stopped.");
            currentRepeat = 0;
            totalRepeats = 0;
            currentFilePath = null;
        }

        public double GetSoundLength(string audioFilePath)
        {
            AudioFileReader audioFile = new AudioFileReader(audioFilePath);

            return audioFile.TotalTime.TotalSeconds;
        }
    }
}
