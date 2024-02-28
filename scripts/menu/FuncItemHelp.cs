using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

namespace MozarellaHills
{
    public class FuncItemHelp : Node2D
    {
        public static FuncItemHelp Instance = null;

        private RichTextLabel richHelpText;
        private CanvasLayer canvasHelp;
        private Button buttonContinue;

        public override void _Ready()
        {
            Instance = this;

            richHelpText = (RichTextLabel) GetNode("CanvasHelp/ControlHelp/PanelText/RichHelpText");
            canvasHelp = (CanvasLayer) GetNode("CanvasHelp");
            buttonContinue = (Button) GetNode("CanvasHelp/ControlHelp/ButtonContinue");

            buttonContinue.Connect("pressed", this, "OnButtonContinuePressed");

            richHelpText.BbcodeEnabled = true;

            canvasHelp.Hide();
        }

        public void ShowHelp(bool isTranslated, string text)
        {
            canvasHelp.Show();
            //GD.Print("show!");
            richHelpText.BbcodeText = isTranslated ? Tr(text) : text;
            buttonContinue.GrabFocus();

            if (PlatformCharacter.Instance != null) PlatformCharacter.Instance.CanControl = false;
        }

        private void OnButtonContinuePressed()
        {
            canvasHelp.Hide();

            PlatformCharacter.Instance.CanControl = true;
        }
    }
}