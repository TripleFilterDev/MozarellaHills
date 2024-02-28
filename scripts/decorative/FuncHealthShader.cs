using Godot;
using System;

namespace MozarellaHills
{
    public class FuncHealthShader : Node2D
    {
        public override void _Ready()
        {
            ((TextureRect) GetNode("CanvasLayer/TextureRect")).Visible = true;
        }

    }
}