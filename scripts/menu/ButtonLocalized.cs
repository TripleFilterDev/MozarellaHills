using Godot;
using System;

namespace MozarellaHills
{
    public class ButtonLocalized : Button
    {
        [Export]
        private string locale = "";

        public override void _Ready()
        {
            Text = Tr(locale);

            Connect("mouse_entered", this, "OnButtonHovered");
            Connect("pressed", this, "OnButtonPressed");
        }

        private void OnButtonHovered()
        {
            GetNode<AudioStreamPlayer>("AudioHover").Play();
        }

        private void OnButtonPressed()
        {
            GetNode<AudioStreamPlayer>("AudioClick").Play();
        }
    }
}