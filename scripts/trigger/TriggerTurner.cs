using Godot;
using System;

namespace MozarellaHills
{
    public class TriggerTurner : Area2D
    {
        [Export]
        private HorizontalDirection turnDirection = HorizontalDirection.None;

        public override void _Ready()
        {
            this.Connect("body_entered", this, "OnBodyEntered");   
        }

        private void OnBodyEntered(Node body)
        {
            if (body is MobBase)
            {
                ((MobBase) body).Turn(turnDirection);
            }
        }
    }
}