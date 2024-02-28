using Godot;
using System;

namespace MozarellaHills
{
    public class ObstacleBush : Area2D
    {
        public override void _Ready()
        {
            Connect("body_entered", this, "OnBodyEntered");
        }

        private void OnBodyEntered(Node body)
        {
            if (body is PlatformCharacter)
            {
                ((PlatformCharacter) body).Kill();
            }

            if (body is MobJackal)
            {
                ((MobJackal) body).Kill();
            }
        }
    }
}