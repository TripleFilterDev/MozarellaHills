using Godot;
using System;
using System.ComponentModel;

namespace MozarellaHills
{
    public class GlobalSettings : Node2D
    {
        public static GlobalSettings Instance = null;

        public static int MusicVolume = 50;
        public static int SoundVolume = 60;
        public static bool VSyncEnabled = true;
        public static bool FullscreenEnabled = false;
        public static string Localization = "en";

        public override void _Ready()
        {
            Instance = this;

            GlobalSettings.Instance.ApplySettings();

            OS.SetThreadName("MozarellaHills");
        }

        public void ApplySettings()
        {
            OS.WindowFullscreen = FullscreenEnabled;
            OS.VsyncEnabled = VSyncEnabled;

            AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), MusicVolume > 0 ? -50 + MusicVolume * 0.5f : -1000f);
            AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sounds"), SoundVolume > 0 ? -75 + SoundVolume * 0.75f : -1000f);

            TranslationServer.SetLocale(Localization);
        }
    }

}
