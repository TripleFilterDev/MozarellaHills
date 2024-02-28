using Godot;
using System;

namespace MozarellaHills
{
    public class ButtonSounded : Button
    {
        public override void _Ready()
        {
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