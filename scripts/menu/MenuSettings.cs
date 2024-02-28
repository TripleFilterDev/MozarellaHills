using Godot;
using System;

namespace MozarellaHills
{
    public class MenuSettings : Node2D
    {
        private Button buttonReturn;

        private Label labelChangedMusic;
        private Button buttonIncreaseMusic;
        private Button buttonReduceMusic;

        private Label labelChangedSound;
        private Button buttonIncreaseSound;
        private Button buttonReduceSound;

        private Button buttonFullscreenToggle;
        private Button buttonVSyncToggle;
        private Button buttonLanguage;

        public override void _Ready()
        {
            labelChangedMusic = (Label) GetNode("CanvasMenu/ControlMenu/ControlMusic/LabelMusic");
            buttonReduceMusic = (Button) GetNode("CanvasMenu/ControlMenu/ControlMusic/ButtonReduce");
            buttonIncreaseMusic = (Button) GetNode("CanvasMenu/ControlMenu/ControlMusic/ButtonIncrease");

            labelChangedSound = (Label) GetNode("CanvasMenu/ControlMenu/ControlSounds/LabelSounds");
            buttonReduceSound = (Button) GetNode("CanvasMenu/ControlMenu/ControlSounds/ButtonReduce");
            buttonIncreaseSound = (Button) GetNode("CanvasMenu/ControlMenu/ControlSounds/ButtonIncrease");

            buttonFullscreenToggle = (Button) GetNode("CanvasMenu/ControlMenu/ControlFullscreen/ButtonToggle");
            buttonVSyncToggle = (Button) GetNode("CanvasMenu/ControlMenu/ControlVSync/ButtonToggle");
            buttonLanguage = (Button) GetNode("CanvasMenu/ControlMenu/ControlLocalization/ButtonToggle");

            buttonReturn = (Button) GetNode("CanvasMenu/ControlMenu/ButtonReturn");

            buttonReduceMusic.Connect("pressed", this, "OnButtonReduceMusicPressed");
            buttonIncreaseMusic.Connect("pressed", this, "OnButtonIncreaseMusicPressed");

            buttonReduceSound.Connect("pressed", this, "OnButtonReduceSoundPressed");
            buttonIncreaseSound.Connect("pressed", this, "OnButtonIncreaseSoundPressed");

            buttonFullscreenToggle.Connect("pressed", this, "OnButtonFullscreenTogglePressed");
            buttonVSyncToggle.Connect("pressed", this, "OnButtonVSyncTogglePressed");
            buttonLanguage.Connect("pressed", this, "OnButtonLanguageTogglePressed");

            buttonReturn.Connect("pressed", this, "OnButtonReturnPressed");

            buttonReturn.GrabFocus();

            UpdateSettingsOnStart();
        }

        private void OnButtonReturnPressed()
        {
            GlobalTransition.Instance.ChangeScene("res://scenes/menu/main.mn.tscn");
        }

        private void UpdateSettingsOnStart()
        {
            labelChangedMusic.Text = GlobalSettings.MusicVolume + "%";
            labelChangedSound.Text = GlobalSettings.SoundVolume + "%";
            buttonFullscreenToggle.Text = GlobalSettings.FullscreenEnabled ? Tr("MN_ENABLED") : Tr("MN_DISABLED");
            buttonVSyncToggle.Text = GlobalSettings.VSyncEnabled ? Tr("MN_ENABLED") : Tr("MN_DISABLED");
            buttonLanguage.Text = GlobalSettings.Localization == "en" ? Tr("LO_ENGLISH") : Tr("LO_RUSSIAN");
        }

        private void OnButtonReduceMusicPressed()
        {
            GlobalSettings.MusicVolume -= 10;
            if (GlobalSettings.MusicVolume < 0)
            {
                GlobalSettings.MusicVolume = 0;
            }

            labelChangedMusic.Text = GlobalSettings.MusicVolume + "%";

            GlobalSettings.Instance.ApplySettings();
        }

        private void OnButtonIncreaseMusicPressed()
        {
            GlobalSettings.MusicVolume += 10;
            if (GlobalSettings.MusicVolume > 100)
            {
                GlobalSettings.MusicVolume = 100;
            }

            labelChangedMusic.Text = GlobalSettings.MusicVolume + "%";

            GlobalSettings.Instance.ApplySettings();
        }

        private void OnButtonReduceSoundPressed()
        {
            GlobalSettings.SoundVolume -= 10;
            if (GlobalSettings.SoundVolume < 0)
            {
                GlobalSettings.SoundVolume = 0;
            }

            labelChangedSound.Text = GlobalSettings.SoundVolume + "%";

            GlobalSettings.Instance.ApplySettings();
        }

        private void OnButtonIncreaseSoundPressed()
        {
            GlobalSettings.SoundVolume += 10;
            if (GlobalSettings.SoundVolume > 100)
            {
                GlobalSettings.SoundVolume = 100;
            }

            labelChangedSound.Text = GlobalSettings.SoundVolume + "%";

            GlobalSettings.Instance.ApplySettings();
        }

        private void OnButtonFullscreenTogglePressed()
        {
            GlobalSettings.FullscreenEnabled = !GlobalSettings.FullscreenEnabled;
            buttonFullscreenToggle.Text = GlobalSettings.FullscreenEnabled ? Tr("MN_ENABLED") : Tr("MN_DISABLED");

            GlobalSettings.Instance.ApplySettings();
        }

        private void OnButtonVSyncTogglePressed()
        {
            GlobalSettings.VSyncEnabled = !GlobalSettings.VSyncEnabled;
            buttonVSyncToggle.Text = GlobalSettings.VSyncEnabled ? Tr("MN_ENABLED") : Tr("MN_DISABLED");

            GlobalSettings.Instance.ApplySettings();
        }

        private void OnButtonLanguageTogglePressed()
        {
            GlobalSettings.Localization = GlobalSettings.Localization == "en" ? "ru" : "en";
            buttonLanguage.Text = GlobalSettings.Localization == "en" ? Tr("LO_ENGLISH") : Tr("LO_RUSSIAN");

            GlobalSettings.Instance.ApplySettings();

            GlobalTransition.Instance.ChangeScene("res://scenes/menu/settings.mn.tscn");
        }
    }
}