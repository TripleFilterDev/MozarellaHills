using Godot;
using System;

namespace MozarellaHills
{
    public class FuncPlayMusic : Node2D
    {
        [Export]
        private GlobalMusic.Tracks track = GlobalMusic.Tracks.None;

        public override void _Ready()
        {
            GlobalMusic.Instance.Play(track);
        }
    }
}