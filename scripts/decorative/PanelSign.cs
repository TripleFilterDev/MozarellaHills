using Godot;
using System;

namespace MozarellaHills
{
    public class PanelSign : Panel
    {
        [Export]
        private string text = "";

        [Export]
        private bool isLocalized = true;

        public override void _Ready()
        {
            GetNode<RichTextLabel>("Label").BbcodeText = isLocalized ? Tr(text) : text;
        }
    }
}
