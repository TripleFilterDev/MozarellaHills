using Godot;
using System;

namespace MozarellaHills
{
    public class TriggerPush : Area2D
    {
        [Export]
        private AllDirection direction = AllDirection.None;

        private PlatformCharacter character = null;

        private bool entered = false;

        public override void _Ready()
        {
            this.Connect("body_entered", this, "OnBodyEntered");
            this.Connect("body_exited", this, "OnBodyExited");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (character != null && entered)
            {
                switch (direction)
                {
                    case AllDirection.Left: character.ApplyImpulse(Vector2.Left * 20); break;
                    case AllDirection.Right: character.ApplyImpulse(Vector2.Right * 20); break;
                    case AllDirection.Up: character.ApplyImpulse(Vector2.Up * 20); break;
                    case AllDirection.Down: character.ApplyImpulse(Vector2.Down * 20); break;
                }
            }
        }

        public override void _Process(float delta)
        {
            
        }

        private void OnBodyEntered(Node body)
        {
            if (body is PlatformCharacter)
            {
                character = (PlatformCharacter) body;
                entered = true;
            }
            
        }

        private void OnBodyExited(Node body)
        {
            if (body is PlatformCharacter)
            {
                character = null;
                entered = false;
            }
        }
    }
}