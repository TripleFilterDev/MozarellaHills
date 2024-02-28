using Godot;
using System;

namespace MozarellaHills
{
    public class FuncCameraBorders : Camera2D
    {
        public static FuncCameraBorders Instance = null;

        public override void _Ready()
        {
            Instance = this;
        }
    }
}