using Godot;
using System;

namespace MozarellaHills
{
    public class TriggerSave : Area2D
    {
        private PlatformCharacter character = null;

        [Export]
        private Vector2 savePosition = Vector2.Zero;

        private bool entered = false;

        public override void _Ready()
        {
            this.Connect("body_entered", this, "OnBodyEntered");
            this.Connect("body_exited", this, "OnBodyExited");
        }

        public override void _Process(float delta)
        {
            
        }

        private void OnBodyEntered(Node body)
        {
            if (body is PlatformCharacter)
            {
                character = (PlatformCharacter) body;
                
                //GlobalAlert.Show(false, "Game saved!");

                //GameState.SavedCharacterPosition = savePosition;
                //GameState.SavedCharacterScene = GetTree().CurrentScene.Filename;

                //GameState.SavedPosition = savePosition;
                //GameState.
                //GameState.Cheeses = character.CheeseCount;
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