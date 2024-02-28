using Godot;
using System;

namespace MozarellaHills
{
    public class TriggerChangeScene : Area2D
    {
        [Export]
        private AllDirection direction = AllDirection.None;

        [Export(PropertyHint.File)]
        private string newScene = "";

        [Export]
        private Color transitionColor = Color.Color8(255, 236, 214);

        [Export]
        private Vector2 newCharacterPos = Vector2.Zero;

        private PlatformCharacter character = null;

        private bool entered = false;

        private bool transit = false;

        public override void _Ready()
        {
            this.Connect("body_entered", this, "OnBodyEntered");
            this.Connect("body_exited", this, "OnBodyExited");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (character != null && entered)
            {
                if (!character.WithStone || (newScene == "res://scenes/t-shirt.lvl.tscn" && character.WearJacket) || (newScene == "res://scenes/dialogs/king.dlg.tscn" && character.WearCrown))
                {
                    GlobalAlert.Show(true, "AL_CANTGO");

                    switch (direction)
                    {
                        case AllDirection.Left: character.ApplyImpulse(Vector2.Left * 20); break;
                        case AllDirection.Right: character.ApplyImpulse(Vector2.Right * 20); break;
                        case AllDirection.Up: character.ApplyImpulse(Vector2.Up * 20); break;
                        case AllDirection.Down: character.ApplyImpulse(Vector2.Down * 20); break;
                    }
                }
                else if (!transit)
                {
                    GameState.SavedPosition = newCharacterPos;
                    GameState.SavedScene = newScene;
                    //GameState.IsRevive = false;

                    GlobalTransition.Instance.ChangeScene(GameState.SavedScene, transitionColor);

                    GameState.Save();
                    GlobalAlert.Show(true, "AL_SAVE");

                    transit = true;
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