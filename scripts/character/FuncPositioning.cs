using Godot;
using System;

namespace MozarellaHills
{
    public class FuncPositioning : Node2D
    {
        public static FuncPositioning Instance = null;

        public Vector2 NewCharacterPos = Vector2.Zero;

        public override void _Ready()
        {
            //NewCharacterPos = GameState.IsRevive ? GameState.SavedCharacterPosition : GameState.NewCharacterPosition;
            NewCharacterPos = GameState.SavedPosition;

            GameState.Load();

            Instance = this;
        }
    }
}